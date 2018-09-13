using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using Telerik.Web.UI;
using System.Configuration;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Transactions;

public partial class admin_products_Products : BasePage
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
            BindCategories();
            BindData();
            grdProducts.DataBind();
        }
    }

    private void BindData()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtProductCategory.SelectedValue))
            {
                int categoryid = int.Parse(txtProductCategory.SelectedValue);
                List<Product> productList = (from p in entities.Products
                                             where p.ProductCategory.CategoryID == categoryid
                                             orderby p.DisplayOrder
                                             select p).ToList();
                grdProducts.DataSource = productList;
            }
            else
                grdProducts.DataSource = null;
            
        }
        catch (Exception ex)
        {
            
        }
    }

    private void BindCategories()
    {
        try
        {
            List<ProductCategory> categories = (from pc in entities.ProductCategories
                                                select pc).ToList();
            txtProductCategory.DataSource = categories;
            txtProductCategory.DataBind();
            if (categories != null)
            {
                txtProductCategory.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindData();
    }
    protected void grdProducts_ItemCommand(object source, GridCommandEventArgs e)
    {
        txtError.Text = string.Empty;
        if (e.CommandName == RadGrid.InitInsertCommandName)
        {
            Response.Redirect("AddProduct.aspx");
        }
        else if(e.CommandName == RadGrid.EditCommandName)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int id = (int)item.GetDataKeyValue("ProductID");
            Response.Redirect("AddProduct.aspx?id=" + id);
        }
        else if (e.CommandName == RadGrid.DeleteCommandName)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int id = (int)item.GetDataKeyValue("ProductID");
            Product pp = (from p in entities.Products
                          where p.ProductID == id
                          select p).FirstOrDefault();
            if (pp != null)
            {
                if (pp.ProductsInBranches.Count == 0)
                {
                    try
                    {
                        using (TransactionScope transaction = new TransactionScope())
                        {
                            entities.Products.Remove(pp);
                            entities.SaveChanges();
                            transaction.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    }
                }
                else
                    txtError.Text = "Cannot remove this product because this product is linked with branch(es)";
            }
        }

    }
    protected void grdProducts_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            
            GridEditableItem item = e.Item as GridEditableItem;
            Image img = item.FindControl("Image1") as Image;
            int id = (int)item.GetDataKeyValue("ProductID");
            Product product = (from p in entities.Products
                                where p.ProductID == id
                                select p).FirstOrDefault();
            if (!product.IsSpecial)
            {
                item.Cells[0].Text = string.Empty;
            }
            else if (product.IsSpecial)
            {
                if (img != null)
                    img.Visible = false;
            }
        }
    }

    public bool GetValue(bool val)
    {
        return val;
    }
    protected void grdProducts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridNestedViewItem)
        {
            GridNestedViewItem nestedItem = e.Item as GridNestedViewItem;
            Product product = nestedItem.DataItem as Product;

            if (!product.IsSpecial)
            {
                string name = nestedItem.ParentItem.OwnerTableView.Columns[0].DataTypeName;
            }
            else
            {
                RadGrid grid = nestedItem.FindControl("grdChildProducts") as RadGrid;

                /*if (grid != null)
                {                    
                    grid.DataSource = product.Products1;
                    grid.DataBind();
                    
                }*/
            }
        }
    }
    protected void grdProducts_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        string s = "";
    }
    protected void txtProductCategory_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindData(); grdProducts.DataBind();
    }
}
