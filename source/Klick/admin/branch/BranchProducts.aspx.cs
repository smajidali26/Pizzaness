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

public partial class admin_branch_BranchProducts : BasePage
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
            BindBranchProducts();
            grdProducts.DataBind();
        }
    }

    
    protected void grdProducts_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindBranchProducts();
    }

    protected void grdProducts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        
    }

    protected void grdProducts_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            
            long id = (long)item.GetDataKeyValue("BranchProductID");
            ProductsInBranch pb = (from p in entities.ProductsInBranches
                                   where p.BranchProductID == id
                                   select p).FirstOrDefault();
            Label label = item.FindControl("Label1") as Label;
            Label label2 = item.FindControl("Label2") as Label;
            if (pb != null)
            {
                Product pp = (from p in entities.Products
                              where p.ProductID == pb.ProductID
                              select p).FirstOrDefault();
                if (pp != null)
                {
                    label.Text = pp.Name;
                }

                Branch bb = (from b in entities.Branches
                             where b.BranchID == pb.BranchID
                             select b).FirstOrDefault();
                if (bb != null)
                {
                    label2.Text = bb.Title;
                }

            }
        }
    }

    protected void grdProducts_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.InitInsertCommandName)
        {
            Response.Redirect("AddProductInBranch.aspx");
        }
        else if (e.CommandName == RadGrid.DeleteCommandName)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem != null)
            {
                Telerik.Web.UI.DataKey key = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex];
                long id = (long)key["BranchProductID"];
                using (TransactionScope transaction = new TransactionScope())
                {
                    List<ProductAdon> productAdonList = (from productAdon in entities.ProductAdons
                                                         join productTypeAdon in entities.AdOnTypeInProducts on productAdon.ProductAdonTypeID equals productTypeAdon.ProductsAdOnTypeId
                                                         where productTypeAdon.BrachProductID == id
                                                         select productAdon).ToList();
                    List<AdOnTypeInProduct> productAdonTypeList = (from productTypeAdon in entities.AdOnTypeInProducts
                                                                   where productTypeAdon.BrachProductID == id
                                                                   select productTypeAdon).ToList();
                    if (productAdonList.Count > 0)
                    {
                        foreach (ProductAdon productAdon in productAdonList)
                        {
                            entities.ProductAdons.Remove(productAdon);
                        }
                    }

                    if (productAdonTypeList.Count > 0)
                    {
                        foreach (AdOnTypeInProduct productAdonType in productAdonTypeList)
                        {
                            entities.AdOnTypeInProducts.Remove(productAdonType);
                        }
                    }

                    List<ProductOptionsInProduct> productOptionList = (from productOption in entities.ProductOptionsInProducts
                                                                       join productOptionType in entities.OptionTypesInProducts on productOption.ProductsOptionTypeId equals productOptionType.ProductsOptionTypeId
                                                                       where productOptionType.BranchProductID == id
                                                                       select productOption).ToList();
                    List<OptionTypesInProduct> productOptionTypeList = (from productOptionType in entities.OptionTypesInProducts
                                                                        where productOptionType.BranchProductID == id
                                                                        select productOptionType).ToList();
                    if (productOptionList.Count > 0)
                    {
                        foreach (ProductOptionsInProduct productAdon in productOptionList)
                        {
                            entities.ProductOptionsInProducts.Remove(productAdon);
                        }
                    }

                    if (productOptionTypeList.Count > 0)
                    {
                        foreach (OptionTypesInProduct productAdonType in productOptionTypeList)
                        {
                            entities.OptionTypesInProducts.Remove(productAdonType);
                        }
                    }
                    ProductsInBranch productInBranch = (from productBranch in entities.ProductsInBranches where productBranch.BranchProductID == id select productBranch).FirstOrDefault();
                    entities.ProductsInBranches.Remove(productInBranch);
                    entities.SaveChanges();
                    transaction.Complete();
                }

            }
        }
    }

    protected void txtProductCategory_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindBranchProducts(); 
        grdProducts.DataBind();
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

    private void BindBranchProducts()
    {
        try
        {
            short categoryId = Convert.ToInt16(txtProductCategory.SelectedValue);
            List<ProductsInBranch> list = (from pb in entities.ProductsInBranches join p in entities.Products on pb.ProductID equals p.ProductID
                                           where p.CategoryID == categoryId
                                           select pb).ToList();
            grdProducts.DataSource = list;
        }
        catch (Exception ex)
        {
        }
    }
}