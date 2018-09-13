using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessService;

public partial class OrderHistory : BasePage
{
    #region Private Members
    private OrderManager orderManager = new OrderManager();
    #endregion

    #region Events

    protected override void OnInit(EventArgs e)
    {
        if (SessionUserId == 0)
        {
            Response.Redirect("Login.aspx?url=OrderHistory.aspx");
        }
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMyOrders();
        }
    }
        
    #endregion

    #region Methods

    private void GetMyOrders()
    {
        rptOrderHistory.DataSource = orderManager.GetOrderByContactInfoId(BranchId, SessionUser.ContactInfoId, 1, 10);
        rptOrderHistory.DataBind();
    }

    public String ReturnOrderPaymentStatus(object isPaid)
    {
        if ((bool)isPaid)
            return "Paid";
        return "Not Paid";
    }

    public String Display(object isPaid,PaymentType paymentType)
    {
        if ((bool)isPaid || paymentType != PaymentType.OnlinePayment)
            return "none";
        return "block";
    }
    #endregion
    
}