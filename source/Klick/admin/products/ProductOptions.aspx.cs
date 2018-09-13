using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Telerik.Web.UI;
using System.Configuration;
using System.Transactions;
using System.Data;
using System.Data.Objects;

public partial class admin_products_ProductOptions : BasePage
{
    private KlickEntities entities = new KlickEntities();

    public override void Dispose()
    {
        entities.Dispose(); 
        base.Dispose();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            grdProductOptions.DataBind();
        }
    }

    private void BindData()
    {
        try
        {
            List<ProductOption> producOptions = (from po in entities.ProductOptions
                                                  select po).ToList();
            grdProductOptions.DataSource = producOptions;
        }
        catch (Exception ex)
        {
            txtError.Text = "Error occured while binding Options data. Error Detail -> " + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
        }
    }

    protected void grdProductOptions_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }
    protected void grdProductOptions_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.InitInsertCommandName)
        {
            Response.Redirect("AddProductOptions.aspx");
        }
        else if (e.CommandName == RadGrid.EditCommandName)
        {
            GridEditableItem item =  e.Item as GridEditableItem;
            int id = (int)item.GetDataKeyValue("ProductOptionID");
            Response.Redirect("AddProductOptions.aspx?id="+id);
        }

    }
    protected void grdProductOptions_ItemDataBound(object sender, GridItemEventArgs e)
    {
        
    }
    protected void grdProductOptions_ItemCreated(object sender, GridItemEventArgs e)
    {
        
    }
   
}