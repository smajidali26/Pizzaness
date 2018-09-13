using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessService;

public partial class controls_Menu : BaseUserControl
{
    #region Private Members
    private ProductCategoryManager productCategoryManager = new ProductCategoryManager();
    #endregion

    #region Public Properties

    public bool IsHomePageMenu { get; set; }

    #endregion
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        GetProductCategories();
    }

    #endregion

    #region private methods
    private void GetProductCategories()
    {
        if (IsHomePageMenu)
            rptMenu.DataSource = productCategoryManager.GetHomePageProductCategories(BranchId);
        else
        {
            rptMenu.DataSource = productCategoryManager.GetAllProductCategories(BranchId,true);
        }
        rptMenu.DataBind();
    }
    #endregion
}