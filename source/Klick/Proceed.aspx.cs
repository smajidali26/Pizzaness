using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using System.Transactions;
using System.Data;
using Telerik.Web.UI;
using BusinessService;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using interfax;
using Core;
using System.Globalization;
using System.Threading;
using BusinessEntities;

public partial class Proceed : BasePage
{
    #region Private Members
    
    private KlickEntities entities = new KlickEntities();

    private OrderManager orderManager = new OrderManager();

    private UsersManager userManager = new UsersManager();

    private PromotionCodeManager promoManager = new PromotionCodeManager();

    private MailManager mailManager = new MailManager();
    #endregion

    #region Page Events

    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected override void OnInit(EventArgs e)
    {
        if (SessionUser == null)
        {
            Response.Redirect("Login.aspx?url=Proceed.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            if(SessionPreOrderPromo != null)
            {
                txtPromotionCode.Text = SessionPreOrderPromo.PreOrderPromoCode;
            }
        }
    }

    
    #endregion

    #region Radion Button Events
    
    protected void txtDeliveryMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtDeliveryMethod.SelectedValue == "1")
        {
            Branch branch = entities.Branches.Where(b => b.BranchID == BranchId).FirstOrDefault();
            if (branch != null)
            {
                if (SessionUserOrder != null)
                {
                    BusinessEntities.PromotionCodes promotionCode = null;
                    Double deduction = 0,Tip = 0;
                    if (!String.IsNullOrEmpty(txtPromotionCode.Text))
                    {
                        promotionCode = promoManager.GetPromotionCodeByCode(txtPromotionCode.Text.Trim());
                        if (promotionCode != null)
                        {
                            if ((promotionCode.StartDate == null ||
                                 (promotionCode.StartDate != null &&
                                  promotionCode.StartDate.Value.Subtract(DateTime.Now).Days >= 0))
                                &&
                                (promotionCode.EndDate == null ||
                                 (promotionCode.EndDate != null &&
                                  promotionCode.EndDate.Value.Subtract(DateTime.Now).Days <= 0)))
                            {
                                if (promotionCode.TypeOfPromotion == BusinessEntities.PromotionType.Money)
                                {
                                    deduction = promotionCode.PromotionValue;
                                }
                                else
                                {
                                    deduction = (SessionUserOrder.OrderTotal*promotionCode.PromotionValue)/100;
                                }
                            }
                        }
                    }
                    txtPrice.Text =  Math.Round(SessionUserOrder.OrderTotal-deduction, 2).ToString();
                    txtTax.Text = "" + Math.Round((decimal)branch.TaxPercentage, 0);
                    txtDeliveryCharges.Text = Math.Round((decimal)branch.DeliveryCharges, 2).ToString();
                    hdTotalChargesBeforeTax.Value = Convert.ToDecimal(Math.Round(SessionUserOrder.OrderTotal - deduction, 2)).ToString();
                    decimal tax = ((Convert.ToDecimal(SessionUserOrder.OrderTotal - deduction) + Math.Round((decimal)branch.DeliveryCharges, 2)) * Math.Round((decimal)branch.TaxPercentage, 0)) / 100;
                    txtTaxAmount.Text = Math.Round(tax, 2).ToString();
                    Double.TryParse(txtTip.Text, out Tip);
                    txtTotal.Text = "" + Math.Round((Math.Round(Convert.ToDecimal(SessionUserOrder.OrderTotal - deduction + Tip), 2) + Math.Round(tax, 2) + Math.Round((decimal)branch.DeliveryCharges, 2)), 2);
                }
            }
        }
        else
        {
            Branch branch = entities.Branches.Where(b => b.BranchID == BranchId).FirstOrDefault();
            if (branch != null)
            {
                if (SessionUserOrder != null)
                {
                    BusinessEntities.PromotionCodes promotionCode = null;
                    Double deduction = 0, Tip = 0;
                    if (!String.IsNullOrEmpty(txtPromotionCode.Text))
                    {
                        promotionCode = promoManager.GetPromotionCodeByCode(txtPromotionCode.Text.Trim());
                        if (promotionCode != null)
                        {
                            if (promotionCode.TypeOfPromotion == BusinessEntities.PromotionType.Money)
                            {
                                deduction = promotionCode.PromotionValue;
                            }
                            else
                            {
                                deduction = (SessionUserOrder.OrderTotal *promotionCode.PromotionValue)/100;
                            }
                        }
                    }
                    txtPrice.Text = Math.Round(SessionUserOrder.OrderTotal - deduction, 2).ToString();
                    txtDeliveryCharges.Text = "0.00";
                    decimal tax = (Convert.ToDecimal(SessionUserOrder.OrderTotal - deduction) * Math.Round((decimal)branch.TaxPercentage, 0)) / 100;
                    txtTax.Text = "" + Math.Round((decimal)branch.TaxPercentage, 0);
                    txtTaxAmount.Text = Math.Round(tax, 2).ToString();
                    Double.TryParse(txtTip.Text, out Tip);
                    txtTotal.Text = "" + Math.Round((Convert.ToDecimal(SessionUserOrder.OrderTotal - deduction + Tip) + tax), 2);
                }
            }
        }
    }

    #endregion

    #region Grid Events

    protected void grdOrderDerail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = e.Item as GridDataItem;
            OrderDetail orderDeail = item.DataItem as OrderDetail;
            if (orderDeail != null)
            {
                string productname = entities.Products.Where(p => p.ProductID == orderDeail.ProductID).Select(p => p.Name).FirstOrDefault();
                item.Cells[2].Text = productname;
            }
        }
    }

    #endregion
    
    #region Button Events

    protected void OrderNow_Click(object sender, EventArgs e)
    {
        if (SessionUserOrder != null)
        {
            
            Double deduction = 0, Tip = 0;
            BusinessEntities.PromotionCodes promotionCode = null;
            BusinessEntities.Orders order = SessionUserOrder;


            if (!String.IsNullOrEmpty(txtPromotionCode.Text))
            {
                if (SessionPreOrderPromo != null)
                    deduction += SessionPreOrderPromo.PreOrderPromoValue;

                promotionCode = promoManager.GetPromotionCodeByCode(txtPromotionCode.Text.Trim());
                if (promotionCode != null)
                {
                    if (promotionCode.TypeOfPromotion == BusinessEntities.PromotionType.Money)
                    {
                        deduction = promotionCode.PromotionValue;
                    }
                    else if (promotionCode.TypeOfPromotion == BusinessEntities.PromotionType.EGiftCard) // in case of E-Gift, keep track of PromotionValueUsed...
                    {
                        if ((promotionCode.PromotionValue - promotionCode.PromoValueUsed) > 0)
                        {
                            if (order.OrderTotal < (promotionCode.PromotionValue - promotionCode.PromoValueUsed))
                            {
                                promotionCode.PromoValueUsed += deduction = order.OrderTotal;
                            }
                            else
                            {
                                promotionCode.PromoValueUsed +=
                                    deduction = (promotionCode.PromotionValue - promotionCode.PromoValueUsed);
                            }
                        }
                    }
                    else
                    {
                        deduction = (SessionUserOrder.OrderTotal*promotionCode.PromotionValue)/100;
                    }

                    order.PromotionCodeId = promotionCode.PromotionCodeId;

                    order.PromotionValueUsed = (promotionCode.PromoValueUsed == 0.0)
                                                   ? (double) 0.0
                                                   : promotionCode.PromoValueUsed;
                }
            }

            if (order.ContactInfoId == 0)
                order.ContactInfoId = SessionUserContactInfoId;
            order.BranchID = Convert.ToInt32(ConfigurationManager.AppSettings["BranchId"]);
            order.OrderTypeID = (BusinessEntities.OrderType)Convert.ToInt16(txtDeliveryMethod.SelectedValue);
            order.PaymentMethod = (BusinessEntities.PaymentType)Convert.ToInt16(txtPaymentMethod.SelectedValue);
            order.TaxPercentage = Convert.ToDouble(txtTax.Text);
            if(!String.IsNullOrEmpty(txtTip.Text) && Double.TryParse(txtTip.Text,out Tip))
            {
                order.LineTip = Double.Parse(txtTip.Text);
            }
            if (order.OrderTypeID == BusinessEntities.OrderType.Deliver)
            {
                order.OrderTypeID = BusinessEntities.OrderType.Deliver;
                order.DeliveryCharges = Convert.ToDouble(txtDeliveryCharges.Text);
                order.DeliveryAddress = txtAddress.Text + " <br /> " + txtCity.Text + " " + txtZipCode.Text + "<br /> " + txtState.Text;
                order.OrderTotal = (order.OrderTotal + order.DeliveryCharges) + order.LineTip + (((order.OrderTotal + order.DeliveryCharges) * order.TaxPercentage) / 100);
                order.OrderTotal = order.OrderTotal - deduction;
            }
            else
            {
                order.OrderTypeID = BusinessEntities.OrderType.SelfPickup;
                order.OrderTotal = order.OrderTotal + order.LineTip + ((order.OrderTotal * order.TaxPercentage) / 100);
                order.OrderTotal = order.OrderTotal - deduction;
            }
            Int64 result = orderManager.AddOrder(order, SessionOrderDetailOptionList, SessionOrderAdonList);

            string sessionID = HttpContext.Current.Session.SessionID;
            string categoryName = string.Empty;
            string sizes = string.Empty;
            string adons = string.Empty;
            double price = order.OrderTotal;
            string recepientName = order.CustomerName;
            string userName = SessionUserFullName;

            foreach (var currentOrder in order.OrderDetailsList)
            {
                if (!categoryName.Contains(currentOrder.CategoryName))
                    categoryName += currentOrder.CategoryName;
                if (currentOrder.OrderDetailSubProducts != null)
                {
                    foreach (var product in currentOrder.OrderDetailSubProducts)
                    {
                        sizes += product.ProductName + ": ";
                        foreach (var p in product.OrderDetailSubProductOptions)
                        {
                            sizes += p.ProductOptionName + ",";
                        }
                        foreach (var adon in product.OrderDetailSubProductAdons)
                        {
                            adons += adon.AdOnId + ",";
                        }
                    }
                }
            }

            sizes = sizes.Trim(',');
            adons = adons.TrimEnd(',');

            sizes = sizes == "" ? null : sizes;
            adons = adons == "" ? null : adons;
            // get log message to pass to db
            string message = Common.ConfirmOrderLogMessage(categoryName, price,
                                                           adons, sizes, recepientName, SessionUserFullName);

            // Log order details
            LogManager log = new LogManager();
            log.SaveLogData(sessionID, LogLevel.INFO.ToString(), Logger.ConfirmOrder.ToString(), message, null);


            if (result > 0)
            {
                order.OrderID = result;
                SendMail(order);
                SendFax(order);
                if(Tip > 0)
                {
                    message = Common.LineTipLogMessage(Tip.ToString(), result.ToString());
                    log.SaveLogData(sessionID, LogLevel.INFO.ToString(), Logger.ConfirmOrder.ToString(), message, null);
                }
                if (order.OrderTypeID == BusinessEntities.OrderType.Deliver)
                {
                    SessionMessage = "Thank you for ordering at pizzaness. Your order has been successfully placed. We will process and dispatch your order to you as soon as possible. An order detail email has been sent at your registered email.";
                }
                else
                {
                    SessionMessage = "Thank you for ordering at pizzaness. Your order has been successfully placed. An order detail email has been sent at your registered email.";
                }
                if (order.PaymentMethod == BusinessEntities.PaymentType.CashPayment)
                {
                    SessionUserOrder = null;
                    SessionPreOrderPromo = null;
                    SessionUserOrderTotal = 0;
                    Response.Redirect("~/Default.aspx");
                }
                SessionOrderId = result;
                SessionUserOrderTotal = order.OrderTotal;

                Response.Redirect("~/ProcessPayment.aspx");
            }
        }
    }

    #endregion

    #region Textbox Event

    protected void txtTip_TextChanged(object sender, EventArgs e)
    {
        decimal tip = 0;
        decimal.TryParse(txtTip.Text, out tip);
        if (tip > 0)
        {
            SessionUserOrderTotal = SessionUserOrder.OrderTotal;
            if (txtPaymentMethod.SelectedValue == "1")
            {
                if (txtDeliveryMethod.SelectedValue == "1")
                {
                    Branch branch = entities.Branches.Where(b => b.BranchID == BranchId).FirstOrDefault();
                    if (branch != null)
                    {
                        if (SessionUserOrder != null)
                        {

                            txtPrice.Text = Math.Round(SessionUserOrder.OrderTotal, 2).ToString();
                            txtTax.Text = "" + Math.Round((decimal)branch.TaxPercentage, 0);
                            txtDeliveryCharges.Text = Math.Round((decimal)branch.DeliveryCharges, 2).ToString();
                            decimal tax = ((Convert.ToDecimal(SessionUserOrder.OrderTotal) + Math.Round((decimal)branch.DeliveryCharges, 2)) * Math.Round((decimal)branch.TaxPercentage, 0)) / 100;
                            txtTotal.Text = "" + Math.Round((Math.Round(Convert.ToDecimal(SessionUserOrder.OrderTotal), 2) + tip + Math.Round(tax, 2) + Math.Round((decimal)branch.DeliveryCharges, 2)), 2);
                        }
                    }
                }
                else
                {
                    Branch branch = entities.Branches.Where(b => b.BranchID == BranchId).FirstOrDefault();
                    if (branch != null)
                    {
                        if (SessionUserOrder != null)
                        {
                            txtPrice.Text = Math.Round(SessionUserOrder.OrderTotal, 2).ToString();
                            txtDeliveryCharges.Text = "0.00";
                            decimal tax = (Convert.ToDecimal(SessionUserOrder.OrderTotal) * Math.Round((decimal)branch.TaxPercentage, 0)) / 100;
                            txtTotal.Text = "" + Math.Round((Convert.ToDecimal(SessionUserOrder.OrderTotal) + tax + tip), 2);
                        }
                    }
                }
            }
        }
        else
        {
            if (SessionUserOrderTotal != 0 && SessionUserOrder.OrderTotal != SessionUserOrderTotal)
                SessionUserOrder.OrderTotal = SessionUserOrderTotal;
            if (txtDeliveryMethod.SelectedValue == "1")
            {
                Branch branch = entities.Branches.Where(b => b.BranchID == BranchId).FirstOrDefault();
                if (branch != null)
                {
                    if (SessionUserOrder != null)
                    {

                        txtPrice.Text = Math.Round(SessionUserOrder.OrderTotal, 2).ToString();
                        txtTax.Text = "" + Math.Round((decimal)branch.TaxPercentage, 0);
                        txtDeliveryCharges.Text = Math.Round((decimal)branch.DeliveryCharges, 2).ToString();
                        decimal tax = ((Convert.ToDecimal(SessionUserOrder.OrderTotal) + Math.Round((decimal)branch.DeliveryCharges, 2)) * Math.Round((decimal)branch.TaxPercentage, 0)) / 100;
                        txtTotal.Text = "" + Math.Round((Math.Round(Convert.ToDecimal(SessionUserOrder.OrderTotal), 2) + Math.Round(tax, 2) + Math.Round((decimal)branch.DeliveryCharges, 2)), 2);
                    }
                }
            }
            else
            {
                Branch branch = entities.Branches.Where(b => b.BranchID == BranchId).FirstOrDefault();
                if (branch != null)
                {
                    if (SessionUserOrder != null)
                    {
                        txtPrice.Text = Math.Round(SessionUserOrder.OrderTotal, 2).ToString();
                        txtDeliveryCharges.Text = "0.00";
                        decimal tax = (Convert.ToDecimal(SessionUserOrder.OrderTotal) * Math.Round((decimal)branch.TaxPercentage, 0)) / 100;
                        txtTotal.Text = "" + Math.Round((Convert.ToDecimal(SessionUserOrder.OrderTotal) + tax), 2);
                    }
                }
            }
        }
    }

    #endregion

    #region Private Methods

    private void BindData()
    {
        /*ContactAddress address = entities.ContactAddresses.Where(a => a.AddressType.AddressTypeName.Equals("billing") && a.ContactInfoId == SessionUser.ContactInfoId).FirstOrDefault();
        if (address != null)
        {
            txtBillingAddress.Text = address.Address + " ," + address.City + " <br />" + address.Zip + " " + address.State;
        }
        else
            txtCurrentAddress.Checked = false;*/
        if (SessionUserOrder != null)
        {
            BusinessEntities.ContactAddresses address = userManager.GetUserAddressByUserId(SessionUserId);
            if (address != null)
            {
                txtFirstName.Text = address.FirstName;
                txtLastName.Text = address.LastName;
                txtTelephone.Text = address.Telephone;
                txtMobile.Text = address.Mobile;
                txtAddress.Text = address.Address;
                txtCity.Text = address.City;
                txtZipCode.Text = address.Zip;
                txtState.Text = address.State;

            }
           
            rptCart.DataSource = SessionUserOrder.OrderDetailsList;
            rptCart.DataBind();

            string categoryName = string.Empty;
            double price = 0.0;
            string adons = string.Empty;
            string productDetail = string.Empty;
            string userName = SessionUserFullName;
            
            foreach (var item in SessionUserOrder.OrderDetailsList)
            {
                price += item.Price;
                categoryName = item.CategoryName;
                userName = SessionUserFullName;
                if (item.OrderDetailSubProducts != null)
                {
                    foreach (var subItem in item.OrderDetailSubProducts)
                    {
                        productDetail += subItem.ProductName + ": ";
                        foreach (var product in subItem.OrderDetailSubProductOptions)
                        {
                            productDetail += product.ProductOptionName + ",";
                        }

                        foreach (var adon in subItem.OrderDetailSubProductAdons)
                        {
                            adons += adon.AdOnId + ",";
                        }
                    }
                }
            }
            productDetail = productDetail.Trim(',');
            adons = adons.TrimEnd(',');

            productDetail = productDetail == "" ? null : productDetail;
            adons = adons == "" ? null : adons;

            string message = Common.CheckoutLogMessage(categoryName, price, adons, productDetail, userName);

            string sessionID = HttpContext.Current.Session.SessionID;

            LogManager log = new LogManager();
            log.SaveLogData(sessionID, LogLevel.INFO.ToString(), Logger.CheckOut.ToString(), message, null);
        }
    }

    private void SendMail(BusinessEntities.Orders order)
    {
        StringBuilder sb = new StringBuilder();
        sb = sb.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "NewOrder.html"));

        sb.Replace("{URL}", BaseSiteUrl);
        sb.Replace("{ORDERNUMBER}", order.OrderID.ToString());
        sb.Replace("{ORDERDATE}", GetStoreTime().ToString("MM/dd/yyyy HH:mm"));
        sb.Replace("{FULLNAME}", txtFirstName.Text + " " + txtLastName.Text);
        sb.Replace("{ADDRESS}", txtAddress.Text);
        sb.Replace("{CITY}", txtCity.Text);
        sb.Replace("{STATE}", txtState.Text);
        sb.Replace("{ZIPCODE}", txtZipCode.Text );
        sb.Replace("{PHONE}", txtTelephone.Text);
        if (!String.IsNullOrEmpty(txtMobile.Text))
            sb.Replace("{MOBILE}", !String.IsNullOrEmpty(txtTelephone.Text) ? " - " + txtMobile.Text : txtMobile.Text);
        else
            sb.Replace("{MOBILE}", "");
        if (order.OrderTypeID == BusinessEntities.OrderType.Deliver)
        {
            sb.Replace("{ORDERDELIVERY}", "Pizzaness Delivery");
        }
        else
        {
            sb.Replace("{ORDERDELIVERY}", "Self Pickup");
        }

        if (order.PaymentMethod == BusinessEntities.PaymentType.OnlinePayment)
        {
            sb.Replace("{PAYMENTMETHOD}", "Online Payment");
        }
        else
        {
            sb.Replace("{PAYMENTMETHOD}", "Cash Payment");
        }
        StringBuilder finalTemplate = new StringBuilder();
        for (int i = 0; i < order.OrderDetailsList.Count; i++)
        {
            BusinessEntities.OrderDetails orderDetails = order.OrderDetailsList.ElementAt(i);
            StringBuilder newTemplate = new StringBuilder();
            newTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "OrderDetailTemplate.html"));
            newTemplate.Replace("{ITEMQUANTITY}", orderDetails.Quantity.ToString());
            newTemplate.Replace("{PRODUCTNAME}", orderDetails.ProductName);
            newTemplate.Replace("{PRODUCTTYPE}", !String.IsNullOrEmpty(orderDetails.CategoryName) ? orderDetails.CategoryName : "");
            newTemplate.Replace("{PRICE}", orderDetails.Price.ToString());
            newTemplate.Replace("{RECIPENTNAME}", orderDetails.RecipientName);
            newTemplate.Replace("{INSTRUCTION}", orderDetails.Comments);

            StringBuilder optionTemplate = new StringBuilder();
            
            foreach (BusinessEntities.OrderDetailOptions obj in SessionOrderDetailOptionList.ElementAt(i))
            {
                //optionTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "OptionTemplate.html"));
                //optionTemplate.Replace("{OPTIONSTYPE}", obj.ProductOptionTypeName);
                optionTemplate.Append(obj.ProductOptionName + ",");
            }
            
            StringBuilder toppings = new StringBuilder();
            String previousAddonTypeName = String.Empty;
            StringBuilder toppingTemplate = new StringBuilder();
            if (orderDetails.IsGroupProduct)
            {
                List<BusinessEntities.OrderDetailSubProduct> subProducts = orderDetails.OrderDetailSubProducts;
                StringBuilder finalSubProductTemplate = new StringBuilder();
                foreach (BusinessEntities.OrderDetailSubProduct subProduct in subProducts)
                {
                    StringBuilder subProductTemplate = new StringBuilder();
                    subProductTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "SubProductTemplate.html"));
                    subProductTemplate.Replace("{PRODUCTNAME}", subProduct.ProductName);
                    subProductTemplate.Replace("{RECIPENTNAME}", subProduct.RecipientName);
                    subProductTemplate.Replace("{INSTRUCTION}", subProduct.Comments);

                    StringBuilder subProductOptionTemplate = new StringBuilder();
                    
                    if (subProduct.OrderDetailSubProductOptions != null)
                    {
                        foreach (BusinessEntities.OrderDetailSubProductOption subProductOption in subProduct.OrderDetailSubProductOptions)
                        {
                            //subProductOptionTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "OptionTemplate.html"));
                            //subProductOptionTemplate.Replace("{OPTIONSTYPE}", subProductOption.ProductOptionTypeName);
                            subProductOptionTemplate.Append(subProductOption.ProductOptionName + ",");
                           
                        }
                    }
                    toppings = new StringBuilder();
                    previousAddonTypeName = String.Empty;
                    toppingTemplate = new StringBuilder();
                    foreach (BusinessEntities.OrderDetailSubProductAdon obj in subProduct.OrderDetailSubProductAdons)
                    {
                        if (obj.SelectedAdonOption != 0)
                        {
                            if (!obj.AdonTypeName.Equals(previousAddonTypeName))
                            {
                                toppings.Append(" <b>" + obj.AdonTypeName + ":</b>");
                                previousAddonTypeName = obj.AdonTypeName;
                            }
                            toppings.Append(Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->",
                                                                 "<!-- End Topping-->", false));
                            if (obj.IsDoubleSelected)
                                toppings.Replace("{DOUBLE}", "Two times ");
                            else
                                toppings.Replace("{DOUBLE}", "");
                            toppings.Replace("{NAME}", obj.AdonName);
                            toppings.Replace("{OPTION}",
                                             ((BusinessEntities.SelectedOption) obj.SelectedAdonOption).ToString());
                        }
                    }

                    if (!String.IsNullOrEmpty(toppings.ToString()))
                    {
                        String subString = Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->", "<!-- End Topping-->", true);
                        toppingTemplate.Replace(subString, toppings.ToString());
                    }
                    subProductTemplate.Replace("<!-- Options -->", subProductOptionTemplate.ToString().TrimEnd(new char[] { ',' }));
                    subProductTemplate.Replace("<!-- Topping -->", toppingTemplate.ToString());
                    finalSubProductTemplate.Append(subProductTemplate.ToString());
                }
                newTemplate.Replace("<!--SubProducts-->", finalSubProductTemplate.ToString());
                toppings = new StringBuilder();
                toppingTemplate = new StringBuilder();
            }
            foreach (BusinessEntities.OrderDetailAdOns obj in SessionOrderAdonList.ElementAt(i))
            {
                if (obj.SelectedAdonOption != 0)
                {
                    if (!obj.AdonTypeName.Equals(previousAddonTypeName))
                    {
                        toppings.Append(" <b>" + obj.AdonTypeName + ":</b>");                        
                        previousAddonTypeName = obj.AdonTypeName;
                    }
                    
                    
                    toppings.Append(obj.AdonName);
                    if (obj.SelectedAdonOption == SelectedOption.FirstHalf)
                    {
                        if (obj.IsDoubleSelected)
                            toppings.Append("(Left 2 Times),");
                        else
                            toppings.Append("(Left),");

                    }
                    else if(obj.SelectedAdonOption == SelectedOption.SecondHalf)
                    {
                        if (obj.IsDoubleSelected)
                            toppings.Append("(Right 2 Times),");
                        else
                            toppings.Append("(Right),");

                    }
                    else
                    { toppings.Append(","); }
                    
                    
                }
            }
            if (!String.IsNullOrEmpty(toppings.ToString()))
            {
                //String subString = Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->", "<!-- End Topping-->", true);
                toppingTemplate.Append(toppings.ToString());
            }
            newTemplate.Replace("<!--OrderDetailOptions-->", optionTemplate.ToString().TrimEnd(new char[] { ',' }));
            newTemplate.Replace("<!--OrderDetailToppings-->", toppingTemplate.ToString().TrimEnd(new char[] { ',' }));
            finalTemplate.Append(newTemplate.ToString());
            //finalTemplate.Append(optionTemplate.ToString());
            //finalTemplate.Append(toppingTemplate.ToString());
        }
        String DetailTemplate = Utility.GetSubString(sb.ToString(), "<!--DETAIL TEMPLATE-->", "<!--END DETAIL TEMPLATE-->", true);
        DetailTemplate = DetailTemplate.Replace("<!--DETAILTEMPLATE-->", finalTemplate.ToString());

        sb.Replace(Utility.GetSubString(sb.ToString(), "<!--DETAIL TEMPLATE-->", "<!--END DETAIL TEMPLATE-->", true), DetailTemplate.ToString());
        sb.Replace("{TIP}", order.LineTip.ToString("C2"));
        sb.Replace("{TOTALPRICE}", order.OrderTotal.ToString("C2"));
        try
        {
            var mail = new Mail();
            mail.Subject = "New Order";
            mail.Sender = ConfigurationManager.AppSettings["DonotReplyEmail"];
            mail.Receiver = SessionUser.Username;
            mail.MailCc = string.Empty;
            mail.MailBcc = ConfigurationManager.AppSettings["OrderReceiveEmail"];
            mail.MailBody = sb.ToString();
            mail.IsHtml = true;
            mail.ReferenceId = order.OrderID;
            mail.ReferenceType = "Order";
            mailManager.AddMail(mail);
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SendFax(BusinessEntities.Orders order)
    {
        StringBuilder Fax = new StringBuilder();
        
        Fax = Fax.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "Fax.html"));
        Fax.Replace("{URL}", BaseSiteUrl);
        Fax.Replace("{ORDERNUMBER}", order.OrderID.ToString());
        Fax.Replace("{ORDERDATE}", GetStoreTime().ToString("MM/dd/yyyy HH:mm"));
        Fax.Replace("{FULLNAME}", txtFirstName.Text +" "+txtLastName.Text);
        Fax.Replace("{ADDRESS}", txtAddress.Text + " " + txtCity.Text + ", " + txtZipCode.Text + ", " + txtState.Text);
        Fax.Replace("{PHONE}", txtTelephone.Text);
        if (!String.IsNullOrEmpty(txtMobile.Text))
            Fax.Replace("{MOBILE}", !String.IsNullOrEmpty(txtTelephone.Text) ? " - " + txtMobile.Text : txtMobile.Text);
        else
            Fax.Replace("{MOBILE}", "");
        if (order.OrderTypeID == BusinessEntities.OrderType.Deliver)
        {
            Fax.Replace("{ORDERDELIVERY}", "Pizzaness Delivery");
        }
        else
        {
            Fax.Replace("{ORDERDELIVERY}", "Self Pickup");
        }

        if (order.PaymentMethod == BusinessEntities.PaymentType.OnlinePayment)
        {
            Fax.Replace("{PAYMENTMETHOD}", "Online Payment");
        }
        else
        {
            Fax.Replace("{PAYMENTMETHOD}", "Cash Payment");
        }
        StringBuilder finalTemplate = new StringBuilder();
        for (int i = 0; i < order.OrderDetailsList.Count; i++)
        {
            BusinessEntities.OrderDetails orderDetail = order.OrderDetailsList.ElementAt(i);
            StringBuilder newTemplate = new StringBuilder();
            newTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "FaxOrderDetailTemplate.html"));
            newTemplate.Replace("{ITEMNUMBER}", (i + 1).ToString());
            newTemplate.Replace("{PRODUCTTYPE}", !String.IsNullOrEmpty(orderDetail.CategoryName) ? orderDetail.CategoryName : "");
            newTemplate.Replace("{PRODUCTNAME}", orderDetail.ProductName);
            newTemplate.Replace("{ITEMQUANTITY}", orderDetail.Quantity.ToString());
            newTemplate.Replace("{ITEMPRICE}", orderDetail.ItemTotal.ToString("C2"));
            newTemplate.Replace("{RECIPENTNAME}", orderDetail.RecipientName);
            newTemplate.Replace("{INSTRUCTION}", orderDetail.Comments);

            StringBuilder optionTemplate = new StringBuilder();
            
            foreach (BusinessEntities.OrderDetailOptions obj in SessionOrderDetailOptionList.ElementAt(i))
            {
                optionTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "OptionTemplate.html"));
                optionTemplate.Replace("{OPTIONSTYPE}", obj.ProductOptionTypeName);
                optionTemplate.Replace("{OPTIONS}", obj.ProductOptionName);
            }
            StringBuilder toppings = new StringBuilder();
            String previousAddonTypeName = String.Empty;
            StringBuilder toppingTemplate = new StringBuilder();
            if (orderDetail.IsGroupProduct)
            {
                List<BusinessEntities.OrderDetailSubProduct> subProducts = orderDetail.OrderDetailSubProducts;
                StringBuilder finalSubProductTemplate = new StringBuilder();
                foreach (BusinessEntities.OrderDetailSubProduct subProduct in subProducts)
                {
                    StringBuilder subProductTemplate = new StringBuilder();
                    subProductTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "SubProductTemplate.html"));
                    subProductTemplate.Replace("{PRODUCTNAME}", subProduct.ProductName);
                    subProductTemplate.Replace("{RECIPENTNAME}", subProduct.RecipientName);
                    subProductTemplate.Replace("{INSTRUCTION}", subProduct.Comments);

                    StringBuilder subProductOptionTemplate = new StringBuilder();
                    
                    if (subProduct.OrderDetailSubProductOptions != null)
                    {
                        foreach (BusinessEntities.OrderDetailSubProductOption subProductOption in subProduct.OrderDetailSubProductOptions)
                        {
                            subProductOptionTemplate.Append(Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "OptionTemplate.html"));
                            subProductOptionTemplate.Replace("{OPTIONSTYPE}", subProductOption.ProductOptionTypeName);
                            subProductOptionTemplate.Replace("{OPTIONS}", subProductOption.ProductOptionName);
                        }
                    }
                    toppings = new StringBuilder();
                    previousAddonTypeName = String.Empty;
                    toppingTemplate = new StringBuilder();
                    int j = 1;
                    foreach (BusinessEntities.OrderDetailSubProductAdon obj in subProduct.OrderDetailSubProductAdons)
                    {
                        if (obj.SelectedAdonOption != 0)
                        {
                            if (!obj.AdonTypeName.Equals(previousAddonTypeName))
                            {
                                if (!String.IsNullOrEmpty(toppings.ToString()))
                                {
                                    String subString = Utility.GetSubString(toppingTemplate.ToString(),
                                                                            "<!-- Toppings-->", "<!-- End Topping-->",
                                                                            true);
                                    toppingTemplate.Replace(subString, toppings.ToString());
                                }
                                toppings = new StringBuilder();
                                toppingTemplate.Append(
                                    Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "ToppingTemplate.html"));
                                toppingTemplate.Replace("{TOPPINGTYPE}", obj.AdonTypeName);
                                previousAddonTypeName = obj.AdonTypeName;
                            }
                            toppings.Append(Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->",
                                                                 "<!-- End Topping-->", false));
                            if (obj.IsDoubleSelected)
                                toppings.Replace("{DOUBLE}", "Two times ");
                            else
                                toppings.Replace("{DOUBLE}", "");
                            toppings.Replace("{NAME}", obj.AdonName);
                            toppings.Replace("{OPTION}",
                                             ((BusinessEntities.SelectedOption) obj.SelectedAdonOption).ToString());
                        }
                    }
            
                    if (!String.IsNullOrEmpty(toppings.ToString()))
                    {
                        String subString = Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->", "<!-- End Topping-->", true);
                        toppingTemplate.Replace(subString, toppings.ToString());
                    }
                    //if(!String.IsNullOrEmpty(subProduct.CrustType))
                    //    subProductTemplate.Replace("<!-- CrustType -->", subProduct.CrustType);
                    subProductTemplate.Replace("<!-- Options -->", subProductOptionTemplate.ToString());
                    subProductTemplate.Replace("<!-- Topping -->", toppingTemplate.ToString());
                    finalSubProductTemplate.Append(subProductTemplate.ToString());
                    if (j != subProduct.OrderDetailSubProductAdons.Count)
                    {
                        finalSubProductTemplate.Append("<tr><td colspan='2' style='border-top:1px solid black;'></td></tr>");
                    }
                }
                newTemplate.Replace("<!--SubProducts-->", finalSubProductTemplate.ToString());
                toppings = new StringBuilder();
                toppingTemplate = new StringBuilder();
            }
            foreach (BusinessEntities.OrderDetailAdOns obj in SessionOrderAdonList.ElementAt(i))
            {
                if (obj.SelectedAdonOption != 0)
                {
                    if (!obj.AdonTypeName.Equals(previousAddonTypeName))
                    {
                        if (!String.IsNullOrEmpty(toppings.ToString()))
                        {
                            String subString = Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->",
                                                                    "<!-- End Topping-->", true);
                            toppingTemplate.Replace(subString, toppings.ToString());
                        }
                        toppings = new StringBuilder();
                        toppingTemplate.Append(
                            Core.Utility.ReadFile(PhysicalApplicationPath + HtmlFolder + "FaxToppingTemplate.html"));
                        toppingTemplate.Replace("{TOPPINGTYPE}", obj.AdonTypeName);
                        previousAddonTypeName = obj.AdonTypeName;
                    }
                    toppings.Append(Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->",
                                                         "<!-- End Topping-->", false));

                    if (obj.IsDoubleSelected)
                        toppings.Replace("{DOUBLE}", "Two times ");
                    else
                        toppings.Replace("{DOUBLE}", "");
                    toppings.Replace("{NAME}", obj.AdonName);
                    toppings.Replace("{OPTION}", ((BusinessEntities.SelectedOption) obj.SelectedAdonOption).ToString());
                }
            }
            if (!String.IsNullOrEmpty(toppings.ToString()))
            {
                String subString = Utility.GetSubString(toppingTemplate.ToString(), "<!-- Toppings-->", "<!-- End Topping-->", true);
                toppingTemplate.Replace(subString, toppings.ToString());
            }
            
            newTemplate.Replace("<!--OrderDetailOptions-->", optionTemplate.ToString());
            newTemplate.Replace("<!--OrderDetailToppings-->", toppingTemplate.ToString());

            finalTemplate.Append(newTemplate.ToString());
            //finalTemplate.Append(optionTemplate.ToString());
            //finalTemplate.Append(toppingTemplate.ToString());
        }
        String DetailTemplate = Utility.GetSubString(Fax.ToString(), "<!--DETAIL TEMPLATE-->", "<!--END DETAIL TEMPLATE-->", true);
        DetailTemplate = DetailTemplate.Replace("<!--DETAILTEMPLATE-->", finalTemplate.ToString());

        Fax.Replace(Utility.GetSubString(Fax.ToString(), "<!--DETAIL TEMPLATE-->", "<!--END DETAIL TEMPLATE-->", true), DetailTemplate.ToString());
        Fax.Replace("{TIP}", order.LineTip.ToString("C2"));
        Fax.Replace("{TOTALPRICE}", order.OrderTotal.ToString("C2"));
        if (order.PaymentMethod == BusinessEntities.PaymentType.CashPayment)
        {
            interfax.InterFax client = new InterFax();
            if (ConfigurationManager.AppSettings["AllowFax"].Equals("true"))
                client.Sendfax(ConfigurationManager.AppSettings["FaxUsername"],
                               ConfigurationManager.AppSettings["FaxPassword"],
                               ConfigurationManager.AppSettings["FaxNumber"],
                               System.Text.Encoding.UTF8.GetBytes(Fax.ToString()), "HTML");
            else
            {
                try
                {
                    Core.Utility.SendEmail(ConfigurationManager.AppSettings["DonotReplyEmail"], SessionUser.Username, String.Empty, ConfigurationManager.AppSettings["OrderReceiveEmail"], "New Order Fax", Fax.ToString(), true);
                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            SessionFax = Fax.ToString();
        }
    }

    #endregion

    
}