using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessService;

public partial class Cart : BasePage
{
    #region Private Members
    private BranchManager branchManager = new BranchManager();
    #endregion

    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowCart();
            
        }
    }

    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hdSelectedDeletedItems.Value))
        {
            String[] ids = hdSelectedDeletedItems.Value.Split(',');
            foreach(String id in ids)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    int orderDetailId = Convert.ToInt32(id);
                    for (int i = 0; i < SessionUserOrder.OrderDetailsList.Count; i++)
                    {
                        if (SessionUserOrder.OrderDetailsList.ElementAt(i).OrderDetailID == orderDetailId)
                        {
                            OrderDetails orderDetail = SessionUserOrder.OrderDetailsList.ElementAt(i);
                            SessionOrderAdonList.Remove(SessionOrderAdonList.ElementAt(i));
                            SessionOrderDetailOptionList.Remove(SessionOrderDetailOptionList.ElementAt(i));
                            SessionUserOrder.OrderTotal -= orderDetail.Quantity * orderDetail.Price;
                            SessionUserOrder.OrderDetailsList.Remove(orderDetail);
                            break;
                        }
                    }
                    
                }
            }
            SessionUserOrderTotal = SessionUserOrder.OrderTotal;
            if (!string.IsNullOrEmpty(hdSelectedDeletedItems.Value.TrimEnd(',')))
            {
                string sessionID = HttpContext.Current.Session.SessionID;
                string message = Common.RemoveFromCartLogMessage(hdSelectedDeletedItems.Value, SessionUserFullName);
                
                LogManager log = new LogManager();
                log.SaveLogData(sessionID, LogLevel.INFO.ToString(), Logger.RemoveFromCart.ToString(), message, null);
            }
            ShowCart();
        }
    }

    protected void rptCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        OrderDetails orderDetail = e.Item.DataItem as OrderDetails;
        if (orderDetail != null)
        {
            if (orderDetail.IsGroupProduct)
            {
                 //Repeater rptSubProduct = (Repeater)e.Item.FindControl("rptSubProduct");
                 //rptSubProduct.DataSource = orderDetail.OrderDetailSubProducts;
                 //rptSubProduct.DataBind();
            }
        }
    }
    #endregion
    
    #region Private Methods

    private void ShowCart()
    {
        if (SessionUserOrder != null)
        {
            rptCart.DataSource = SessionUserOrder.OrderDetailsList;
            SessionUserOrder.OrderDetailsList.Select(o=> new {o.ItemTotal }).ToList();
        }
        else
            rptCart.DataSource = null;
        rptCart.DataBind();
        if (SessionUserOrder != null && SessionUserOrder.OrderDetailsList.Count > 0)
        {
            BusinessEntities.Branches branch = branchManager.GetBranchById(BranchId);
            hdVAT.Value = Math.Round(branch.TaxPercentage, 2).ToString("G");
            if(SessionPreOrderPromo != null)
            {
                txtSubTotal.Text = SessionUserOrder.OrderTotal.ToString("G");
                txtDiscount.Text = SessionPreOrderPromo.PreOrderPromoValue.ToString("G");
                deduction.Visible = true;
                txtVATAmount.Text = Math.Round(((SessionUserOrder.OrderTotal - SessionPreOrderPromo.PreOrderPromoValue) * Convert.ToDouble(branch.TaxPercentage)) / 100, 2).ToString("G");
                txtTotal.Text = Math.Round(Convert.ToDouble(txtVATAmount.Text) + (SessionUserOrder.OrderTotal - SessionPreOrderPromo.PreOrderPromoValue), 2).ToString("G");
            }
            else
            {
                txtSubTotal.Text = SessionUserOrder.OrderTotal.ToString();
                txtVATAmount.Text = Math.Round((SessionUserOrder.OrderTotal * Convert.ToDouble(branch.TaxPercentage)) / 100, 2).ToString("G");
                txtTotal.Text = Math.Round(Convert.ToDouble(txtVATAmount.Text) + Convert.ToDouble(txtSubTotal.Text), 2).ToString("G");
            }
            
        }
        else
        {
            txtSubTotal.Text = "";
            hdVAT.Value = "";
            txtVATAmount.Text ="";
            txtTotal.Text = "";
        }
    }

    #endregion

}