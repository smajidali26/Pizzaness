using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessObjects;
using BusinessService;

public partial class ProductPage : BasePage
{
    #region Private Members

    private KlickEntities entities = new KlickEntities();

    private ProductCategoryManager productCategoryManager = new ProductCategoryManager();

    private int PreviousRandomNumber { get; set; }

    private int TotalResults { get; set; }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Int16 categoryId = 0;
        if (Int16.TryParse(QueryStringParamID, out categoryId))
        {
            ProductCategories productCategory = productCategoryManager.GetProductCategoryById(categoryId);
            if(productCategory!=null)
            {
                ltCategoryName.Text = productCategory.Name;
            }
            BindProducts(categoryId);
        }
    }

    #region private methods

    private void BindProducts(short categoryid)
    {
        //txtError.Text = string.Empty;
        try
        {

            var storeTime = GetStoreTime();
            var results = (from pc in entities.ProductCategories
                           join p in entities.Products on pc.CategoryID equals p.CategoryID
                           join bp in entities.ProductsInBranches on p.ProductID equals bp.ProductID
                           join b in entities.Branches on bp.BranchID equals b.BranchID
                           where b.BranchID == BranchId && pc.CategoryID == categoryid && bp.Enable
                           select new { p.Name, bp.Price, bp.BranchProductID, p.ProductID, p.Description, p.IsSpecial, p.ImagePath, p.Image, p.DisplayOrder }).OrderBy(p => p.DisplayOrder).ToList().Distinct();
            TotalResults = results.Count();

            rptMenu.DataSource = results;
            rptMenu.DataBind();
        }
        catch (Exception ex)
        {
            
        }
    }
    
    public String StartDiv(object index)
    {
        if ((Int32)index % 2 == 0 || (Int32)index == 0)
            return "<div class='span9 offset-bottom'>";
        return "";
    }
    public String EndDiv(object index)
    {
        if ((Int32)index % 2 == 1 || (Int32)index == TotalResults - 1)
            return "</div>";
        return "";
    }
    #endregion
}