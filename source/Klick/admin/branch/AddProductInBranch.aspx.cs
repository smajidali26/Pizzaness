using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using System.Configuration;
using System.Transactions;
using System.Data;
using System.Data.Objects;
using BusinessService;
using BusinessEntities;

public partial class admin_branch_AddProductInBranch : BasePage
{
    private BranchManager _branchManager = new BranchManager();

    private ProductManager _productManager = new ProductManager();

    private ProductInBranchManager _prodictInBranchManager = new ProductInBranchManager();


    private ProductsInBranches ProductInBranchObj { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int32 branchProductId = 0;
            if (Int32.TryParse(QueryStringParamID, out branchProductId))
            {
                ProductInBranchObj = _prodictInBranchManager.GetProductInBranchById(branchProductId);
                if (ProductInBranchObj != null)
                {
                    BindBranches();
                    BindProducts();
                    txtPrice.Value = ProductInBranchObj.Price;
                    txtActive.Enabled = ProductInBranchObj.Enable;
                    ViewStateID = QueryStringParamID.ToString();
                }
            }
            else
            {
                BindBranches();
                BindProducts();
            }
        }
    }

    private void BindBranches()
    {
        try
        {
            txtBranch.DataSource = _branchManager.GetAllBranch();
            txtBranch.DataBind();
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void BindProducts()
    {
        try
        {
            if (txtBranch.Items.Count > 0)
            {
                int branchid = int.Parse(txtBranch.SelectedValue);
                string queryString = String.Empty;
                if (QueryStringParamID == null)
                {
                    txtProduct.DataSource =  _productManager.GetProductsNotInBranch(branchid);
                    txtProduct.DataBind();

                }
                else
                {
                     Products product = _productManager.GetProductById(ProductInBranchObj.ProductID);
                     if (product != null)
                     {
                         txtProduct.Items.Add(new RadComboBoxItem(product.Name, product.ProductID.ToString()));
                     }
                }
            }
            else
                txtError.Text = "You must add branch before adding products for branch.";
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }
    protected void txtBranch_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindProducts();
    }
    protected void SaveBtn_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtBranch.SelectedValue) && !string.IsNullOrEmpty(txtProduct.SelectedValue) && txtPrice.Value != null)
            {
                ProductsInBranches productInBranch = new ProductsInBranches();
                int result = 0;
                productInBranch.BranchID = Int32.Parse(txtBranch.SelectedValue);
                productInBranch.ProductID = Int32.Parse(txtProduct.SelectedValue);
                productInBranch.Price = txtPrice.Value.Value;
                productInBranch.Enable = txtActive.Checked;

                result = _prodictInBranchManager.AddUpdateProductInBranch(productInBranch);
                if (result > 0)
                {
                    if (ViewStateID != null)
                    {
                        ShowMessage(txtProduct.SelectedItem.Text + " has been updated with default price of $" + txtPrice.Value, MessageType.Success);
                        
                    }
                    else
                    {
                        ShowMessage(txtProduct.SelectedItem.Text + " added into branch(" + txtBranch.SelectedItem.Text + ") with default price of $" + txtPrice.Value, MessageType.Success);
                        
                        BindProducts();
                    }
                    txtError.ForeColor = System.Drawing.Color.Green;
                    txtError.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    
                }
            }
            else
                ShowMessage("There should be all values to make new entry.", MessageType.Error);
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }
    protected void UpdateBtn_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void CancelBtn_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Branches.aspx");
    }
}