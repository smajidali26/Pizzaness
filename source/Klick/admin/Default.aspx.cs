using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessService;
using Telerik.Web.UI;

public partial class admin_Default : BasePage
{
    private OrderManager orderManager = new OrderManager();

    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //FromDate.MaxDate = DateTime.Now;
           // ToDate.MaxDate = DateTime.Now;
            BindDeliveryStatus();
            grdOrder.PageSize = GridPageSize;
            BindData();
            grdOrder.DataBind();
        }
    }

    protected void grdOrder_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }

    protected void grdOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
        grdOrder.DataBind();
    }

    protected void ddlOrderStatus_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        grdOrder.VirtualItemCount = 0;
        grdOrder.CurrentPageIndex = 0;
        BindData();
        grdOrder.DataBind();
    }
    #endregion

    #region Private Methods
    private void BindData()
    {
        ICollection<Orders> list = orderManager.GetOrder(BranchId,(OrderStatus)Convert.ToInt32(ddlOrderStatus.SelectedValue),grdOrder.CurrentPageIndex+1,grdOrder.PageSize);
        grdOrder.DataSource = list;
        if (list.Count > 0)
            grdOrder.VirtualItemCount = list.ElementAt(0).TotalRows;
    }

    private void BindDeliveryStatus()
    {
        ddlOrderStatus.Items.Add(new RadComboBoxItem("New Order",((short)OrderStatus.NewOrder).ToString()));
        ddlOrderStatus.Items.Add(new RadComboBoxItem(OrderStatus.Delivered.ToString(), ((short)OrderStatus.Delivered).ToString()));
    }
    #endregion

   
}