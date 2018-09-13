using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessService;
using Core;

public partial class Ajax_AjaxData : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static bool UpdateOrderDetailItem(int orderDetailId, int quantity)
    {
        var previousPrice = SessionUserOrder.OrderDetailsList.Where(od => od.OrderDetailID == orderDetailId).ElementAt(0).Price;
        var previousQuantity = SessionUserOrder.OrderDetailsList.Where(od => od.OrderDetailID == orderDetailId).ElementAt(0).Quantity;
        SessionUserOrder.OrderDetailsList.Where(od => od.OrderDetailID == orderDetailId).ElementAt(0).Quantity = quantity;
        SessionUserOrder.OrderDetailsList.Where(od => od.OrderDetailID == orderDetailId).ElementAt(0).ItemTotal = quantity * previousPrice;
        SessionUserOrder.OrderTotal -= (previousPrice * previousQuantity);
        SessionUserOrder.OrderTotal += (previousPrice * quantity);
        
        return true;
    }

    [WebMethod]
    public static String GetOrderDetails()
    {
        String html = String.Empty;
        StringBuilder finalTemplate = new StringBuilder();
        StringBuilder shortCarttemplate = new StringBuilder();
        Double subTotal = 0;
        Double tax = 0;

        #region Detail Items
        if (SessionUserOrder != null)
        {
            for (int i = 0; i < SessionUserOrder.OrderDetailsList.Count; i++)
            {
                StringBuilder template = new StringBuilder();
                template = template.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "ShortOrderDetail.html"));

                template.Replace("{PRODUCTNAME}", SessionUserOrder.OrderDetailsList.ElementAt(i).ProductName);
                template.Replace("{IMAGEPATH}", "/Products/" + SessionUserOrder.OrderDetailsList.ElementAt(i).ProductImage);
                template.Replace("{QUANTITY}", SessionUserOrder.OrderDetailsList.ElementAt(i).Quantity.ToString());
                template.Replace("{PRICE}", SessionUserOrder.OrderDetailsList.ElementAt(i).ItemTotal.ToString());
                finalTemplate.Append(template.ToString());
                subTotal += SessionUserOrder.OrderDetailsList.ElementAt(i).ItemTotal;
            }
        }
        #endregion

        #region Short Cart
        shortCarttemplate = shortCarttemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "ShortCart.html"));
        if (SessionUserOrder != null && SessionUserOrder.OrderDetailsList.Count > 0)
        {
            BranchManager branchManager = new BranchManager();
            Branches branch = branchManager.GetBranchById(BranchId);
            shortCarttemplate = shortCarttemplate.Replace("{DETAIL}", finalTemplate.ToString());
            shortCarttemplate = shortCarttemplate.Replace("{ITEMS}", SessionUserOrder.OrderDetailsList.Count.ToString());
            shortCarttemplate = shortCarttemplate.Replace("{SUBTOTAL}", subTotal.ToString());
            shortCarttemplate = shortCarttemplate.Replace("{TAX}", Math.Round(branch.TaxPercentage, 2).ToString("G"));
            tax = Math.Round((SessionUserOrder.OrderTotal * Convert.ToDouble(branch.TaxPercentage)) / 100, 2);
            shortCarttemplate = shortCarttemplate.Replace("{TAXPRICE}", tax.ToString("G"));
            shortCarttemplate = shortCarttemplate.Replace("{TOTALPRICE}", (tax + SessionUserOrder.OrderTotal).ToString("G"));
        }
        else
        {
            shortCarttemplate = shortCarttemplate.Replace("{DETAIL}", "No item is added in cart.");
            shortCarttemplate = shortCarttemplate.Replace("{ITEMS}", "0");
            shortCarttemplate = shortCarttemplate.Replace("{SUBTOTAL}", "0");
            shortCarttemplate = shortCarttemplate.Replace("{TAX}", "0");
            shortCarttemplate = shortCarttemplate.Replace("{TAXPRICE}", "0");
            shortCarttemplate = shortCarttemplate.Replace("{TOTALPRICE}", "0");
        }

        #endregion

        return shortCarttemplate.ToString();
    }

    [WebMethod]
    public static Double AddLineTip(Double currentValue,  Double taxAmount)
    {
        SessionUserOrder.LineTip = currentValue;        
        return SessionUserOrder.OrderTotal + currentValue + taxAmount;
    }

    [WebMethod]
    public static object AddPromotionCode(String promotionCode,Double lineTip,Double deliveryCharges)
    {
        #region Variables

        string json = @"{{'SubTotal':{0},'Tax': {1}, 'OrderTotal': {2}, 'InvalidCode': {3} }}";
        
        Double deduction = 0,NewTotal = 0,Tax = 0;
        PromotionCodeManager promoManager = new PromotionCodeManager();
        BranchManager branchManager = new BranchManager();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        #endregion

        PromotionCodes code = promoManager.GetPromotionCodeByCode(promotionCode);
        Branches branch = branchManager.GetBranchById(BranchId);
        if (code != null)
        {
            DateTime currentDate = DateTime.Now;

            NewTotal = SessionUserOrder.OrderTotal;
            Tax = ((NewTotal + deliveryCharges) * Convert.ToDouble(branch.TaxPercentage)) / 100;

            double preOrderPromoValue = 0.0d;

            if (SessionPreOrderPromo != null)
            {
                preOrderPromoValue = SessionPreOrderPromo.PreOrderPromoValue;

                if (SessionPreOrderPromo.PreOrderPromoCode.ToLower().Equals(promotionCode.ToLower()))

                    json = string.Format(json, SessionUserOrder.OrderTotal, Tax,
                                         (SessionUserOrder.OrderTotal + deliveryCharges + Tax + lineTip),
                                         Resources.ErrorMessages.PromoCodeInUse);

                return serializer.Deserialize<Dictionary<string, string>>(json);
            }

            if (currentDate > code.EndDate || currentDate < code.StartDate)
            {
                json = string.Format(json, SessionUserOrder.OrderTotal, Tax, (SessionUserOrder.OrderTotal + deliveryCharges + Tax + lineTip), Resources.ErrorMessages.PromoOutOfDate);

                return serializer.Deserialize<Dictionary<string, string>>(json);
            }

            if (code.TypeOfPromotion == PromotionType.EGiftCard && code.CodeUsageCounter != null && code.CodeUsageCounter == 0)    //  in db it's incrementing 
            {
                json = string.Format(json, SessionUserOrder.OrderTotal, Tax, (SessionUserOrder.OrderTotal + Tax + lineTip), Resources.ErrorMessages.PromoCounterZero);

                return serializer.Deserialize<Dictionary<string, string>>(json);
            }

            if (code.TypeOfPromotion == PromotionType.Money)
            {
                deduction = code.PromotionValue;
            }
            else if (code.TypeOfPromotion == PromotionType.EGiftCard)
            {
                if ((code.PromotionValue - code.PromoValueUsed) > 0)
                {
                    if (SessionUserOrder.OrderTotal < (code.PromotionValue - code.PromoValueUsed))
                        code.PromoValueUsed += deduction = SessionUserOrder.OrderTotal;
                    else
                        code.PromoValueUsed += deduction = (code.PromotionValue - code.PromoValueUsed);
                }
                else
                {       // promo value used completely
                    json = string.Format(json, NewTotal, Tax, (NewTotal + deliveryCharges + Tax + lineTip), Resources.ErrorMessages.PromoCodeBalanceEnded);
                    return serializer.Deserialize<Dictionary<string, string>>(json);
                }
            }
            else
            {
                deduction = (SessionUserOrder.OrderTotal*code.PromotionValue)/100;
            }

            NewTotal = SessionUserOrder.OrderTotal;
            NewTotal = NewTotal <= 0 ? 0 : NewTotal;
            Tax = ((NewTotal + deliveryCharges) * Convert.ToDouble(branch.TaxPercentage)) / 100;

            json = string.Format(json, NewTotal, Tax, (NewTotal + deliveryCharges + Tax + lineTip) - deduction, 0);

            var dict = serializer.Deserialize<Dictionary<string, string>>(json);
            
            return dict;
        }
        else
        {
            SessionPreOrderPromo = null;
            Tax = (SessionUserOrder.OrderTotal + deliveryCharges) * (Convert.ToDouble(branch.TaxPercentage)) / 100;
            json = string.Format(json, SessionUserOrder.OrderTotal, Tax, SessionUserOrder.OrderTotal + deliveryCharges + Tax + lineTip, Resources.ErrorMessages.PromoCodeInvalid);


            var dict = serializer.Deserialize<Dictionary<string, string>>(json);
            return dict;
        }
    }


    // IT WAS NEVER CALLED SO CODE IS NOT TESTED
    //[WebMethod]
    //public static object AddPreOrderPromo(string promoCode)
    //{
    //    double promoCodeValue = 0.0d;
    //    DateTime currentDate = DateTime.Now;

    //    string json = @"{{'Message':{0} }}";
    //    JavaScriptSerializer serializer = new JavaScriptSerializer();

    //    PromotionCodeManager promoManager = new PromotionCodeManager();

    //    BusinessEntities.PreOrderPromo sessionPreOrderPromo = SessionPreOrderPromo;
    //    if (sessionPreOrderPromo == null)
    //    {
    //        sessionPreOrderPromo = new BusinessEntities.PreOrderPromo();
    //    }

    //    PromotionCodes code = promoManager.GetPromotionCodeByCode(sessionPreOrderPromo.PreOrderPromoCode = promoCode);

    //    if (code != null)
    //    {
    //        if (currentDate > code.EndDate || currentDate < code.StartDate)
    //        {
    //            json = string.Format(json, Resources.ErrorMessages.PromoOutOfDate);

    //            return serializer.Deserialize<Dictionary<string, string>>(json);
    //        }

    //        if (code.CodeUsageCounter != null && code.CodeUsageCounter == 0)
    //        {
    //            json = string.Format(json, Resources.ErrorMessages.PromoCounterZero);

    //            return serializer.Deserialize<Dictionary<string, string>>(json);
    //        }

    //        sessionPreOrderPromo.PreOrderPromoValue = code.PromotionValue - code.PromoValueUsed;
    //        sessionPreOrderPromo.PreOrderPromoCode = code.PromotionCode;
    //    }

    //    SessionPreOrderPromo = sessionPreOrderPromo;

    //    if (sessionPreOrderPromo.PreOrderPromoValue > 0)
    //    {
    //        json = string.Format(json, Resources.InfoMessages.PromoCodeAdded);

    //        return serializer.Deserialize<Dictionary<string, string>>(json);
    //    }
    //    else
    //    {
    //        json = string.Format(json, Resources.ErrorMessages.PromoNotAdded);

    //        return serializer.Deserialize<Dictionary<string, string>>(json);
    //    }
    //}

    [WebMethod]
    public static ICollection<Object> SaleReport(Int16 reportType,String fromDate,String toDate)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        DateTimeFormatInfo format = new DateTimeFormatInfo();
        format.ShortDatePattern = "MM/dd/yyyy";
        format.LongDatePattern = "MM/dd/yyyy HH:mm";
        cultureInfo.DateTimeFormat = format;

        Thread.CurrentThread.CurrentCulture = cultureInfo;

        DateTime? FromDate = null, ToDate = null;
        if (!String.IsNullOrEmpty(fromDate))
        {
            FromDate = DateTime.Parse(fromDate);
        }
        if (!String.IsNullOrEmpty(toDate))
        {
            ToDate = DateTime.Parse(toDate);
        }

        ICollection<Object> list = new List<Object>();
        OrderManager orderManager = new OrderManager();
        DataSet dataset = orderManager.SaleReport(reportType, FromDate, ToDate);
        if (dataset.Tables[0] != null)
        {
            list.Add(Utility.ConvertTo<Report>(dataset.Tables[0]));
        }
        if (dataset.Tables[1] != null)
        {
            list.Add(Utility.ConvertTo<Report>(dataset.Tables[1]));
        }
        return list;
    }
}