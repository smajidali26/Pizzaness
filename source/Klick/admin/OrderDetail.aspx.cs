using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessService;
using Telerik.Web.UI;
using System.IO;

public partial class admin_Order_OrderDetail : BasePage
{
    private OrderManager orderManager = new OrderManager();

    private MailManager mailManager = new MailManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            long orderid = Convert.ToInt64(Request.QueryString["OID"]);
            var path = Server.MapPath("~/OrderDetailHtml/") + orderid + ".html";
            if(File.Exists(path))
            {
                Response.Redirect("~/OrderDetailHtml/" + orderid + ".html");
            }
            else
            {
                
                var mail = mailManager.GetMailByReferenceId(orderid, "Order");
                if (mail != null)
                {
                    File.WriteAllText(path, mail.MailBody);
                    Response.Redirect("~/OrderDetailHtml/" + orderid + ".html");
                }
            }
            //GetOrderDetails();
            //grdOrderDetail.DataBind();
        }
    }


    #region Private Methods
    private void GetOrderDetails()
    {
        long orderid = Convert.ToInt64(Request.QueryString["OID"]);

        Orders order = orderManager.GetOrderById(orderid);
        if (order.IsPaid)
            Paid.Visible = false;
        if (order.OrderStatusID == OrderStatus.Delivered)
            Delivered.Visible = false;
        ICollection<OrderDetails> orderDetails = orderManager.GetOrderDetail(orderid);
        grdOrderDetail.DataSource = orderDetails;
    }
    #endregion

    protected void grdOrderDetail_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        GetOrderDetails();
    }
    protected void grdOrderDetail_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        if (e.DetailTableView.Name.Equals("OrderDetailAdon"))
        {
            GridDataItem parentItem = e.DetailTableView.ParentItem as GridDataItem;
            long orderDetailId = (long)parentItem.GetDataKeyValue("OrderDetailID");
            ICollection<OrderDetailAdOns> orderDetailAdons = new List<OrderDetailAdOns>();
            ICollection<OrderDetails> orderDetails = (ICollection<OrderDetails>)e.DetailTableView.DataSource;
            OrderDetails orderDetail = orderDetails.Where(od => od.OrderDetailID == orderDetailId).First();
            if (!String.IsNullOrEmpty(orderDetail.OrderDetailAdOnsXml))
                orderDetailAdons = Core.Utility.XmlToObjectList<OrderDetailAdOns>(orderDetail.OrderDetailAdOnsXml, "//OrderDetailAdOns/OrderDetailAdOn");

            e.DetailTableView.DataSource = orderDetailAdons;
            
        }
        else if (e.DetailTableView.Name.Equals("OrderDetailOption"))
        {
            GridDataItem parentItem = e.DetailTableView.ParentItem as GridDataItem;
            long orderDetailId = (long)parentItem.GetDataKeyValue("OrderDetailID");
            ICollection<OrderDetailOptions> orderDetailOptions = new List<OrderDetailOptions>();
            ICollection<OrderDetails> orderDetails = (ICollection<OrderDetails>)e.DetailTableView.DataSource;
            OrderDetails orderDetail = orderDetails.Where(od => od.OrderDetailID == orderDetailId).First();
            if (!String.IsNullOrEmpty(orderDetail.OrderDetailOptionXml))
                orderDetailOptions = Core.Utility.XmlToObjectList<OrderDetailOptions>(orderDetail.OrderDetailOptionXml, "//OrderDetailOptions/OrderDetailOption");
            e.DetailTableView.DataSource = orderDetailOptions;          
        }
    }

    protected void grdOrderDetail_ItemDataBound(object sender, GridItemEventArgs e)
    {

    }

    protected void grdOrderDetail_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }
    protected void Paid_Click(object sender, EventArgs e)
    {
        long orderid = Convert.ToInt64(Request.QueryString["OID"]);
        int result = orderManager.OrderPaid(orderid);
        if (result > 0)
        {
            txtMessage.Text = "Order payment status has been changed to Paid.";
        }
    }
    protected void Delete_Click(object sender, EventArgs e)
    {
        long orderid = Convert.ToInt64(Request.QueryString["OID"]);
        int result = orderManager.DeleteOrder(orderid);
        if (result > 0)
        {
            txtMessage.Text = "Order has been deleted.";
        }
    }

    protected void Delivered_Click(object sender, EventArgs e)
    {
        long orderid = Convert.ToInt64(Request.QueryString["OID"]);
        int result = orderManager.OrderDelivered(orderid);
        if (result > 0)
        {
            txtMessage.Text = "Order delivery status has been changed to Delivered.";
        }
    }
}