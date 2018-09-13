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
using BusinessService;
using Core;

public partial class admin_branch_AddOptionInBranchProduct : BasePage
{
    private KlickEntities entities = new KlickEntities();
    private OptionTypesInProduct otpObj = null;
    private ProductOptionManager productOptionManager = new ProductOptionManager();
    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBranches();
            BindCategories();
            BindProducts();
            BindOptionType();
            CheckAvailbilityOfOptionsofProducts();
            Set_OptionTypeInProducts_LocalObject();
            BindOptions();            
        }
    }

    #region DataBindMethods

    private void BindBranches()
    {
        try
        {
            txtError.Text = string.Empty;
            List<Branch> branches = (from b in entities.Branches
                                     select b).ToList();
            txtCbBranch.DataSource = branches;
            txtCbBranch.DataBind();
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
            txtError.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtCbBranch.SelectedValue))
            {
                int branchid = int.Parse(txtCbBranch.SelectedValue);
                int categoryId = int.Parse(txtCbCategory.SelectedValue);
                List<Product> products = (from p in entities.Products
                                          join bp in entities.ProductsInBranches on p.ProductID equals bp.ProductID
                                          where bp.BranchID == branchid && bp.Product.CategoryID == categoryId && p.IsSpecial == false
                                          select p).ToList();
                txtCbProduct.DataSource = products.OrderBy(p => p.Name);
                txtCbProduct.DataBind();
            }
            else
                ShowMessage("There is no branch to display. Add Branch from Branch Managment.", MessageType.Error); 
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void BindOptionType()
    {
        try
        {
            txtError.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtCbProduct.SelectedValue))
            {
                int productid = int.Parse(txtCbProduct.SelectedValue);
                Product product = (from p in entities.Products
                                          where p.ProductID==productid                                          
                                          select p).FirstOrDefault();
                if (product != null)
                {
                    if (!product.IsSpecial)
                    {
                        int branchid = int.Parse(txtCbBranch.SelectedValue);                       
                        string query = @"SELECT OptionType.*
                        FROM         OptionType 
                        WHERE     NOT (OptionType.OptionTypeID IN (Select OT.OptionTypeID from OptionType as OT 
                        INNER JOIN
                      OptionTypesInProduct ON OptionType.OptionTypeID = OptionTypesInProduct.OptionTypeID INNER JOIN
                      ProductsInBranches ON OptionTypesInProduct.BranchProductID = ProductsInBranches.BranchProductID
                      WHERE (ProductsInBranches.ProductID = " + productid + " And ProductsInBranches.BranchID = " + branchid + ")))";

                        var optiontype = (from ot in entities.OptionTypes
                                          select ot).ToList();
                        txtCbOptionType.DataSource = optiontype.OrderBy(ot => ot.OptionTypeName);
                        txtCbOptionType.DataBind();                        
                    }
                    else
                    {                        
                        var countproduct = (from p in entities.Products
                                            join bp in entities.ProductsInBranches on p.ProductID equals bp.ProductID
                                            select p.ProductID).ToList();
                        if (countproduct.Count != product.ProductsInBranches.Count)
                        {
                            ShowMessage("You can not set price for this product because its sub child are not existing in selected branch.", MessageType.Error); 
                        }
                    }
                }
            }
            else
                ShowMessage("No Product found in branch. Add Product in branch from Branch Managment.", MessageType.Error); 
            
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void BindOptions()
    {
        txtError.Text = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtCbOptionType.SelectedValue))
            {
                short optionid = short.Parse(txtCbOptionType.SelectedValue);
                List<ProductOption> options = (from o in entities.ProductOptions
                                               where o.OptionTypeId == optionid
                                               select o).ToList();
                grdOptions.DataSource = options;
            }
            else
                grdOptions.DataSource = null;
            grdOptions.DataBind();
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void BindCategories()
    {
        try
        {
            txtError.Text = string.Empty;
            
            List<ProductCategory> products = (from pc in entities.ProductCategories
                                                where pc.IsActive == true
                                                select pc).ToList();
            txtCbCategory.DataSource = products.OrderBy(p => p.Name);
            txtCbCategory.DataBind();
            
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    #endregion
       
    #region ComboEvents

    protected void txtCbBranch_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindProducts();
        BindOptionType(); 
        CheckAvailbilityOfOptionsofProducts(); 
        Set_OptionTypeInProducts_LocalObject(); 
        BindOptions();
    }

    protected void txtCbCategory_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindProducts();
        BindOptionType();
        CheckAvailbilityOfOptionsofProducts();
        Set_OptionTypeInProducts_LocalObject();
        BindOptions();
    }

    protected void txtCbProduct_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindOptionType(); 
        CheckAvailbilityOfOptionsofProducts(); 
        Set_OptionTypeInProducts_LocalObject(); 
        BindOptions();
    }

    protected void txtCbOptionType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Set_OptionTypeInProducts_LocalObject(); BindOptions();
    }

    #endregion    

    #region Yes No Events

    protected void txtNo_CheckedChanged(object sender, EventArgs e)
    {
        EnableOptionPrice(true);
    }

    protected void txtYes_CheckedChanged(object sender, EventArgs e)
    {
        EnableOptionPrice(false);
    }

    protected void txtAdonYes_CheckedChanged(object sender, EventArgs e)
    {
        EnableToppingPrice(true);
    }

    protected void txtAdonNo_CheckedChanged(object sender, EventArgs e)
    {
        EnableToppingPrice(false);
    }

    #endregion

    #region ButtonEvents

    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        if (checkValues())
        {
            int branchid = int.Parse(txtCbBranch.SelectedValue);
            int productid = int.Parse(txtCbProduct.SelectedValue);
            short optiontypeid = short.Parse(txtCbOptionType.SelectedValue);
            ProductsInBranch productbranch = (from pb in entities.ProductsInBranches
                                              where pb.BranchID == branchid && pb.ProductID == productid
                                              select pb).FirstOrDefault();
            if (productbranch != null)
            {
                BusinessEntities.OptionTypesInProduct optiontypeproduct = new BusinessEntities.OptionTypesInProduct();
                ICollection<BusinessEntities.ProductOptionsInProducts> ProductOptionList = new List<BusinessEntities.ProductOptionsInProducts>();
               
                optiontypeproduct.BranchID = branchid;
                optiontypeproduct.ProductID = productid;
                optiontypeproduct.OptionTypeID = Convert.ToInt16(txtCbOptionType.SelectedValue);

                
                GridItemCollection collection = grdOptions.Items;
                bool flag = true;

                foreach (GridItem item in collection)
                {
                    CheckBox chk = item.FindControl("txtSelected") as CheckBox;
                    GridEditableItem editableItem = item as GridEditableItem;
                    int productoptionid = (int)editableItem.GetDataKeyValue("OptionID");
                    if (chk.Checked)
                    {
                        BusinessEntities.ProductOptionsInProducts productoption = new BusinessEntities.ProductOptionsInProducts();
                        productoption.ProductOptionID = productoptionid;
                        CheckBox enable = item.FindControl("txtEnabled") as CheckBox;
                        productoption.Enabled = enable.Checked;
                        RadNumericTextBox txtDisplayOrder = item.FindControl("txtDisplayOrder") as RadNumericTextBox;
                        productoption.DisplayOrder = Convert.ToInt16( txtDisplayOrder.Value.ToString());

                        if (txtNo.Checked)
                        {
                            RadNumericTextBox price = item.FindControl("txtOptionPrice") as RadNumericTextBox;
                            if (price.Value != null)
                            {
                                productoption.Price = Convert.ToDecimal(price.Value);
                            }
                            else
                            {
                                ShowMessage("You have not entered option price in selected row of Available Options Table.", MessageType.Error);
                                item.Selected = true;
                                flag = false;
                                break;
                            }
                        }
                        else if (txtYes.Checked)
                        {
                            productoption.Price = Convert.ToDecimal(txtPrice.Value);
                        }

                        if (txtAdonYes.Checked)
                        {
                            RadNumericTextBox price1 = item.FindControl("txtAdonPrice") as RadNumericTextBox;
                            if (price1.Value != null)
                            {
                                productoption.ToppingPrice = Convert.ToDecimal(price1.Value);
                            }
                            else
                            {
                                ShowMessage("You have not entered Adon price in selected row of Available Options Table.", MessageType.Error);
                                item.Selected = true;
                                flag = false;
                                break;
                            }
                        }
                        else
                        {
                            productoption.ToppingPrice = new Nullable<decimal>();
                        }
                        ProductOptionList.Add(productoption);
                    }
                }

                if (flag && ProductOptionList.Count > 0)
                {
                    
                        try
                        {
                            if (txtYes.Checked)
                                optiontypeproduct.IsSamePrice = true;
                            else
                                optiontypeproduct.IsSamePrice = false;
                            if (txtMultiYes.Checked)
                                optiontypeproduct.IsMultiSelect = true;
                            else
                                optiontypeproduct.IsMultiSelect = false;
                            if (txtAdonYes.Checked)
                                optiontypeproduct.IsAdonPriceVary = true;
                            else
                                optiontypeproduct.IsAdonPriceVary = false;
                            if (txtPriceChangeYes.Checked)
                            {
                                optiontypeproduct.IsProductPriceChangeType = true;  
                            }
                            else
                                optiontypeproduct.IsProductPriceChangeType = false;
                            optiontypeproduct.ProductOptionsXml = Utility.CollectionXml<BusinessEntities.ProductOptionsInProducts>(ProductOptionList, "ProductOptionsInProductsDataSet", "ProductOptionsInProductsDataTable");
                            int result = productOptionManager.AddProductOption(optiontypeproduct);
                            if (result > 0)
                            {
                                ShowMessage("Options have been saved successfully.", MessageType.Success);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                        }
                    
                }
                else if (flag)
                {
                    ShowMessage("You have selected no option. You must select option to proceed.", MessageType.Error);
                }
            }
        }
    }

    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branches.aspx");
    }

    #endregion

    private void EnableOptionPrice(bool val)
    {
        OptionPrice.Visible = !val;
        GridItemCollection collection = grdOptions.Items;
        foreach (GridItem item in collection)
        {
            RadNumericTextBox price = item.FindControl("txtOptionPrice") as RadNumericTextBox;
            price.Enabled = val;
        }    
    }
    
    private void EnableToppingPrice(bool val)
    {
        GridItemCollection collection = grdOptions.Items;
        foreach (GridItem item in collection)
        {
            RadNumericTextBox price = item.FindControl("txtAdonPrice") as RadNumericTextBox;
            price.Enabled = val;
        }
    }
        
    private bool checkValues()
    {
        if (string.IsNullOrEmpty(txtCbBranch.SelectedValue))
        {
            txtError.Text = "No branch is available. First Add Branch from Branch Managment.";
            return false;
        }
        if (string.IsNullOrEmpty(txtCbProduct.SelectedValue))
        {
            txtError.Text = "No product available for branch. First Add Product in branch from Branch Managment.";
            return false;
        }
        if (string.IsNullOrEmpty(txtCbOptionType.SelectedValue))
        {
            txtError.Text = "No Option Type available. First Add Option type and Options from Product Managment.";
            return false;
        }
        if (txtYes.Checked == false && txtNo.Checked == false)
        {
            txtError.Text = "Will Price remain same for below options? is not selected. ";
            return false;
        }
        if (txtAdonYes.Checked == false && txtAdonNo.Checked == false)
        {
            txtError.Text = "Adon Price will vary for below options? is not selected. ";
            return false;
        }
        return true;
    }

    protected void grdOptions_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            
            if (txtNo.Checked)
            {
                
                RadNumericTextBox price = e.Item.FindControl("txtOptionPrice") as RadNumericTextBox;
                price.Enabled = true;
                
                if (otpObj != null)
                {
                    ProductOption po = e.Item.DataItem as ProductOption;
                    ProductOptionsInProduct popObj = otpObj.ProductOptionsInProducts.Where(pop => pop.ProductOption.OptionID == po.OptionID).FirstOrDefault();
                    if (popObj != null)
                    {
                        price.Value = Convert.ToDouble(popObj.Price);
                        CheckBox chk = e.Item.FindControl("txtSelected") as CheckBox;
                        chk.Checked = true;
                    }
                }
                
            }
            RadNumericTextBox adonprice = e.Item.FindControl("txtAdonPrice") as RadNumericTextBox;
            if (txtAdonYes.Checked)
            {
                adonprice.Enabled = true;
                if (otpObj != null)
                {
                    ProductOption po = e.Item.DataItem as ProductOption;
                    ProductOptionsInProduct popObj = otpObj.ProductOptionsInProducts.Where(pop => pop.ProductOption.OptionID == po.OptionID).FirstOrDefault();
                    if (popObj != null)
                    {
                        adonprice.Value = Convert.ToDouble(popObj.ToppingPrice);
                        CheckBox chk = e.Item.FindControl("txtSelected") as CheckBox;
                        chk.Checked = true;
                    }
                }
            }
            else
                adonprice.Value = null;
            RadNumericTextBox txtDisplayOrder = e.Item.FindControl("txtDisplayOrder") as RadNumericTextBox;
            if (otpObj != null)
            {
                ProductOption po = e.Item.DataItem as ProductOption;
                ProductOptionsInProduct popObj = otpObj.ProductOptionsInProducts.Where(pop => pop.ProductOption.OptionID == po.OptionID).FirstOrDefault();
                if (popObj != null)
                txtDisplayOrder.Value = (popObj.DisplayOrder != null) ? popObj.DisplayOrder.Value : 0;
            }
        }
    }

    private void CheckAvailbilityOfOptionsofProducts()
    {
        txtError.Text = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtCbBranch.SelectedValue) && !string.IsNullOrEmpty(txtCbProduct.SelectedValue) && !string.IsNullOrEmpty(txtCbOptionType.SelectedValue))
            {
                int branchid = int.Parse(txtCbBranch.SelectedValue);
                int productid = int.Parse(txtCbProduct.SelectedValue);
                short optiontypeid = short.Parse(txtCbOptionType.SelectedValue);

                OptionTypesInProduct optiontypeproduct = (from otp in entities.OptionTypesInProducts
                                                          join bp in entities.ProductsInBranches on otp.BranchProductID equals bp.BranchProductID
                                                          where otp.OptionType.OptionTypeID == optiontypeid && bp.BranchID == branchid && bp.ProductID == productid
                                                          select otp).FirstOrDefault();
                if (optiontypeproduct != null)
                {
                    if (optiontypeproduct.IsMultiSelect)
                    {
                        txtMultiYes.Checked = true;
                    }
                    else
                        txtMultiNo.Checked = true;
                    if (optiontypeproduct.IsAdonPriceVary)
                    {
                        txtAdonYes.Checked = true;
                    }
                    else
                        txtAdonNo.Checked = true;
                    if (optiontypeproduct.IsSamePrice)
                    {
                        txtYes.Checked = true;
                        txtPrice.Value = Convert.ToDouble(optiontypeproduct.ProductOptionsInProducts.ToList()[0].Price);
                    }
                    else
                    {
                        txtNo.Checked = true;
                    }
                    if (optiontypeproduct.IsProductPriceChangeType)
                    {
                        txtPriceChangeYes.Checked = true;
                    }
                    else
                        txtPriceChangeNo.Checked = true;
                }
                else
                {
                    txtPriceChangeYes.Checked = false;
                    txtPriceChangeNo.Checked = false;
                    txtNo.Checked = false;
                    txtMultiYes.Checked = false;
                    txtMultiNo.Checked = false;
                    txtYes.Checked = false;
                    txtAdonYes.Checked = false;
                    txtAdonNo.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void Set_OptionTypeInProducts_LocalObject()
    {
        if (!string.IsNullOrEmpty(txtCbBranch.SelectedValue) && !string.IsNullOrEmpty(txtCbProduct.SelectedValue) && !string.IsNullOrEmpty(txtCbOptionType.SelectedValue))
        {
            int branchid = int.Parse(txtCbBranch.SelectedValue);
            int productid = int.Parse(txtCbProduct.SelectedValue);
            short optiontypeid = short.Parse(txtCbOptionType.SelectedValue);

            otpObj = (from otp in entities.OptionTypesInProducts
                                                      join bp in entities.ProductsInBranches on otp.BranchProductID equals bp.BranchProductID
                                                      where otp.OptionType.OptionTypeID == optiontypeid && bp.BranchID == branchid && bp.ProductID == productid
                                                      select otp).FirstOrDefault();
            if (otpObj != null)
            {
                if (otpObj.IsMultiSelect)
                {
                    txtMultiYes.Checked = true;
                }
                else
                    txtMultiNo.Checked = true;
                if (otpObj.IsAdonPriceVary)
                {
                    txtAdonYes.Checked = true;
                }
                else
                    txtAdonNo.Checked = true;
                if (otpObj.IsSamePrice)
                {
                    txtYes.Checked = true;
                    txtPrice.Value = Convert.ToDouble(otpObj.ProductOptionsInProducts.ToList()[0].Price);
                }
                else
                {
                    txtNo.Checked = true;
                }
                if (otpObj.IsProductPriceChangeType)
                {
                    txtPriceChangeYes.Checked = true;
                }
                else
                    txtPriceChangeNo.Checked = true;
            }
        }
    }

    protected void txtPriceChangeYes_CheckedChanged(object sender, EventArgs e)
    {
        txtError.Text = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtCbBranch.SelectedValue) && !string.IsNullOrEmpty(txtCbProduct.SelectedValue))
            {
                int branchid = int.Parse(txtCbBranch.SelectedValue);
                int productid = int.Parse(txtCbProduct.SelectedValue);
                short optiontypeid = short.Parse(txtCbOptionType.SelectedValue);
                OptionTypesInProduct OTP = (from otp in entities.OptionTypesInProducts
                                            join bp in entities.ProductsInBranches on otp.BranchProductID equals bp.BranchProductID
                                            where bp.BranchID == branchid && bp.ProductID == productid && otp.IsProductPriceChangeType == true && otp.OptionTypeID != optiontypeid
                                            select otp).FirstOrDefault();
                if (OTP != null)
                {
                    txtError.Text = "Another option type "+ OTP.OptionType.OptionTypeName+" has property to change Product Price on selection of any Option. Selection of Yes will override it.";
                }
            }
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }
}