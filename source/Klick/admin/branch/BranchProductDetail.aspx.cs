using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessService;
using BusinessEntities;
using System.Data;

public partial class admin_branch_BranchProductDetail : BasePage
{
    #region Private Members
    private ProductManager _productManager = new ProductManager();
    #endregion

    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void buttonDelete_Click(object sender, EventArgs e)
    {
        string id = hiddenId.Value;
        string deleteType = hiddenDeleteType.Value;
        if (deleteType.Equals("0"))
        {
            _productManager.DeleteBranchProductOptionType(Convert.ToInt64(id));
        }
        else
        {
            _productManager.DeleteBranchProductAdonType(Convert.ToInt64(id));
        }
        BindData();
    }
    #endregion

    #region Methods

    private void BindData()
    {
        DataSet dataSet = _productManager.GetProductDetail(Convert.ToInt64(QueryStringParamID));
        if (dataSet.Tables.Count > 0 && dataSet.Tables.Count == 2)
        {
            rptProductOptions.DataSource = dataSet.Tables[0];
            rptProductOptions.DataBind();

            rptProductAdon.DataSource = dataSet.Tables[1];
            rptProductAdon.DataBind();
        }
    }

    #endregion
}