using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessEntities;
using BusinessService;
using System.Configuration;
using Telerik.Web.UI;

public partial class templates_main : BaseMasterPage
{
    #region Private Members
    
    private KlickEntities entities = new KlickEntities();
    private OrderManager orderManager = new OrderManager();
    private BranchManager _branchManager = new BranchManager();
    
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        string s = string.Empty;
        ManageMenu();
        if (SessionUserId != 0)
        {
            login.Visible = about.Visible = false;
            myaccount.Visible = logout.Visible = true;

            //txtUserFullName.Text = SessionUserFullName;
            //logout.Visible = true;
            //welcome.Visible = false;
        }
        
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsClosed"]) && (IsStoreClosed()))
        {
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "StoreClosed", "<script>StoreClosed=true;</script>");
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (SessionUserOrder != null)
        {
            NoOfItems.Text = SessionUserOrder.OrderDetailsList.Count.ToString();
            CartIconPanel.Visible = true;
        }
        base.Render(writer);
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (SessionUserOrder != null)
        {

            orderManager.AddOrder(SessionUserOrder,SessionOrderDetailOptionList,SessionOrderAdonList);
        }
    }

    private void ManageMenu()
    {
       /* if (SessionUserId != 0)
        {
            register.Visible = false;
            login.Visible = false;
            signout.Visible = true;
            if (SessionUserRole.LoginTypeId == 3)
            {
                account.Visible = true;
            }
            else if (SessionUserRole.LoginTypeId == 1 || SessionUserRole.LoginTypeId == 2)
            {
                administration.Visible = true;
            }
        }*/
    }
}
