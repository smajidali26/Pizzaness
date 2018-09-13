using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using System.Configuration;
using Telerik.Web.UI;

public partial class controls_OrderGrid : BaseUserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionUserOrder != null)
        {
            txtOrderGrd.Visible = true ;    
            UpdateOrderDisplayList();
        }
    }


    public void UpdateOrderDisplayList()
    {
        if (SessionUserOrder != null)
        {

           
                txtOrderGrd.DataSource = SessionUserOrder.OrderDetailsList;
                SessionUserOrderTotal = Math.Round(SessionUserOrder.OrderTotal, 2);
            
        }
        else
            txtOrderGrd.DataSource = null;
        txtOrderGrd.DataBind();
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        UpdateOrderDisplayList();

    }
    protected void txtOrderGrd_ItemDataBound(object sender, GridItemEventArgs e)
    {
        
    }

    protected void txtOrderGrd_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.DeleteCommandName)
        {

            if (SessionUserOrder != null)
            {
                long id = (long)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["OrderDetailID"];
                foreach (OrderDetails detail in SessionUserOrder.OrderDetailsList)
                {
                    if (detail.OrderDetailID == id)
                    {
                        SessionUserOrder.OrderTotal -= detail.Price;
                        SessionUserOrder.OrderDetailsList.Remove(detail);
                        SessionUserOrderTotal = Math.Round(SessionUserOrder.OrderTotal, 2);
                        break;
                    }
                }
                Session["order"] = SessionUserOrder;
                txtOrderGrd.DataSource = SessionUserOrder.OrderDetailsList;
                txtOrderGrd.DataBind();
            }
        }
    }

    protected void DeleteOrder_Click(object sender, ImageClickEventArgs e)
    {
    }    
}