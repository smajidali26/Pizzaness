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

public partial class admin_branch_AddAdonInBranchProduct : BasePage
{
    private KlickEntities entities = new KlickEntities();

    private AdOnTypeInProduct atPObj = null;

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
            BindAdOnType();
            Set_AdonTypeInProducts_LocalObject();
            BindAdons();
            
        }
    }

    #region Combo Events

    protected void txtCbBranch_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindProducts();
        BindAdOnType(); CheckAvailbilityOfOptionsofProducts(); Set_AdonTypeInProducts_LocalObject(); BindAdons();
    }

    protected void txtCbCategory_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindProducts();
        BindAdOnType();
        CheckAvailbilityOfOptionsofProducts();
        Set_AdonTypeInProducts_LocalObject();
        BindAdons();
    }

    protected void txtCbProduct_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindAdOnType(); CheckAvailbilityOfOptionsofProducts(); Set_AdonTypeInProducts_LocalObject(); BindAdons();
    }

    protected void txtCbAdonType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        short id = short.Parse(txtCbAdonType.SelectedValue);
        AdonType adontype = (from at in entities.AdonTypes
                             where at.AdOnTypeId == id
                              select at).FirstOrDefault();
        Set_AdonTypeInProducts_LocalObject();
        if (adontype.IsFreeAdonType)
        {
            txtPrice.Value = null;
            txtPrice.Enabled = false;
        }
        else
        {
            txtPrice.Enabled = true;
            CheckAvailbilityOfOptionsofProducts();
        }
        BindAdons();
    }

    protected void txtCbDisplayFormat_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindAdons();
    }
    #endregion

    #region Button Events

    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        if (checkValues())
        {
            int branchid = int.Parse(txtCbBranch.SelectedValue);
            int productid = int.Parse(txtCbProduct.SelectedValue);
            short optiontypeid = short.Parse(txtCbAdonType.SelectedValue);
            short displayFormat = Convert.ToInt16(txtCbDisplayFormat.SelectedValue);
            ProductsInBranch productbranch = (from pb in entities.ProductsInBranches
                                              where pb.BranchID == branchid && pb.ProductID == productid
                                              select pb).FirstOrDefault();
            if (productbranch != null)
            {
                AdOnTypeInProduct adontypeproduct = null;
                if (atPObj != null)
                    adontypeproduct = atPObj;
                else
                    adontypeproduct = new AdOnTypeInProduct();
                GridItemCollection collection = grdAdons.Items;
                bool flag = true;
                if (atPObj == null)
                {
                    foreach (GridItem item in collection)
                    {
                        CheckBox chk = item.FindControl("txtSelected") as CheckBox;
                        GridEditableItem editableItem = item as GridEditableItem;
                        short adonid = (short)editableItem.GetDataKeyValue("AdOnID");
                        if (chk.Checked)
                        {
                            ProductAdon productadon = new ProductAdon();
                            productadon.AdonID = adonid;
                            CheckBox enable = item.FindControl("txtEnabled") as CheckBox;
                            productadon.Enable = enable.Checked;
                            RadComboBox defaultSeleted = item.FindControl("txtCbDefault") as RadComboBox;
                            productadon.DefaultSelected = short.Parse(defaultSeleted.SelectedValue);
                            adontypeproduct.ProductAdons.Add(productadon);
                        }
                    }
                }
                else
                {
                    foreach (GridItem item in collection)
                    {
                        CheckBox chk = item.FindControl("txtSelected") as CheckBox;
                        GridEditableItem editableItem = item as GridEditableItem;
                        short adonid = (short)editableItem.GetDataKeyValue("AdOnID");

                        if (chk.Checked)
                        {
                            ProductAdon productadon = null;
                            bool isNew = false;
                            productadon = atPObj.ProductAdons.Where(pa => pa.AdonID == adonid).FirstOrDefault();
                            if (productadon == null)
                            {
                                productadon = new ProductAdon();
                                productadon.AdonID = adonid;
                                isNew = true;
                            }
                            CheckBox enable = item.FindControl("txtEnabled") as CheckBox;
                            productadon.Enable = enable.Checked;
                            RadComboBox defaultSeleted = item.FindControl("txtCbDefault") as RadComboBox;
                            productadon.DefaultSelected = short.Parse(defaultSeleted.SelectedValue);
                            if (isNew)
                                adontypeproduct.ProductAdons.Add(productadon);
                            else
                            {
                                adontypeproduct.ProductAdons.Remove(productadon);
                                adontypeproduct.ProductAdons.Add(productadon);
                            }
                        }
                        else
                        {
                            ProductAdon padon = atPObj.ProductAdons.Where(pa => pa.AdonID == adonid).FirstOrDefault();
                            if (padon != null)
                            {
                                atPObj.ProductAdons.Remove(padon);
                                entities.ProductAdons.Remove(padon);
                            }
                        }
                    }
                }

                if ( adontypeproduct.ProductAdons.Count > 0)
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        try
                        {   
                            if (txtPrice.Value != null)
                                adontypeproduct.Price = Convert.ToDecimal(txtPrice.Value);
                            else
                                adontypeproduct.Price = new Nullable<decimal>();
                            adontypeproduct.DisplayFormat = displayFormat;
                            if (atPObj == null)
                            {
                                adontypeproduct.AdonTypeID = optiontypeid;
                                adontypeproduct.BrachProductID = productbranch.BranchProductID;
                                entities.AdOnTypeInProducts.Add(adontypeproduct);
                            }                            
                            entities.SaveChanges();
                            transaction.Complete();
                            ShowMessage("Product adons have been saved successfully. ", MessageType.Error);
                        }
                        catch (Exception ex)
                        {
                            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                        }
                    }
                }
                else if (flag)
                {
                    ShowMessage("You have selected no Adon. You must select Adon to proceed.",MessageType.Error);
                }
            }
        }
    }
   
    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchProducts.aspx");
    }

    #endregion

    protected void grdAdons_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            Adon adon = e.Item.DataItem as Adon;
            if (atPObj != null)
            {
                ProductAdon PAdon = atPObj.ProductAdons.Where(pa => pa.AdonID == adon.AdOnID).FirstOrDefault();
                if (PAdon != null)
                {
                    CheckBox chk = e.Item.FindControl("txtSelected") as CheckBox;
                    chk.Checked = true;
                    RadComboBox combo = e.Item.FindControl("txtCbDefault") as RadComboBox;
                    combo.SelectedValue = PAdon.DefaultSelected.ToString();
                    if (!txtCbDisplayFormat.SelectedValue.Equals("1") && !txtCbDisplayFormat.SelectedValue.Equals("4"))
                    {
                        combo.Enabled = false;
                    }
                }
            }
        }
    }

    #region Private Methods

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
        if (string.IsNullOrEmpty(txtCbAdonType.SelectedValue))
        {
            txtError.Text = "No Adon Type available. First Add Adon type and Adon from Product Managment.";
            return false;
        }
        else
        {
            short id = short.Parse(txtCbAdonType.SelectedValue);
            AdonType adontype = (from at in entities.AdonTypes
                                 where at.AdOnTypeId == id
                                 select at).FirstOrDefault();
            if (!adontype.IsFreeAdonType)
            {
                if (txtPrice.Value == null)
                {
                    txtError.Text = "Enter default adon price.";
                    return false;
                }
            }
           
        }
        return true;
    }

    private void CheckAvailbilityOfOptionsofProducts()
    {
        txtError.Text = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtCbBranch.SelectedValue) && !string.IsNullOrEmpty(txtCbProduct.SelectedValue) && !string.IsNullOrEmpty(txtCbAdonType.SelectedValue))
            {
                int branchid = int.Parse(txtCbBranch.SelectedValue);
                int productid = int.Parse(txtCbProduct.SelectedValue);
                short adontypeid = short.Parse(txtCbAdonType.SelectedValue);

                AdOnTypeInProduct adontypeproduct = (from otp in entities.AdOnTypeInProducts
                                                          join bp in entities.ProductsInBranches on otp.BrachProductID equals bp.BranchProductID
                                                          where otp.AdonType.AdOnTypeId == adontypeid && bp.BranchID == branchid && bp.ProductID == productid
                                                          select otp).FirstOrDefault();
                if (adontypeproduct != null)
                {
                    txtPrice.Value = Convert.ToDouble(adontypeproduct.Price);
                }
            }
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void Set_AdonTypeInProducts_LocalObject()
    {
        if (!string.IsNullOrEmpty(txtCbBranch.SelectedValue) && !string.IsNullOrEmpty(txtCbProduct.SelectedValue) && !string.IsNullOrEmpty(txtCbAdonType.SelectedValue))
        {
            int branchid = int.Parse(txtCbBranch.SelectedValue);
            int productid = int.Parse(txtCbProduct.SelectedValue);
            short adontypeid = short.Parse(txtCbAdonType.SelectedValue);

            atPObj = (from otp in entities.AdOnTypeInProducts
                      join bp in entities.ProductsInBranches on otp.BrachProductID equals bp.BranchProductID
                      where otp.AdonType.AdOnTypeId == adontypeid && bp.BranchID == branchid && bp.ProductID == productid
                      select otp).FirstOrDefault();
            if (atPObj != null)
            {
                if (atPObj.Price != null)
                    txtPrice.Text = atPObj.Price.Value.ToString();
                txtCbDisplayFormat.SelectedValue = atPObj.DisplayFormat.Value.ToString();
            }
        }
    }

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
                txtError.Text = "There is no branch to display. Add Branch from Branch Managment.";
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void BindAdOnType()
    {
        try
        {
            txtError.Text = string.Empty;
            if (!string.IsNullOrEmpty(txtCbProduct.SelectedValue))
            {
                int productid = int.Parse(txtCbProduct.SelectedValue);
                Product product = (from p in entities.Products
                                   where p.ProductID == productid
                                   select p).FirstOrDefault();
                if (product != null)
                {
                    if (!product.IsSpecial)
                    {
                        int branchid = int.Parse(txtCbBranch.SelectedValue);
                        string query = @"SELECT     AdonType.*
                                FROM         AdonType
                                WHERE     NOT(AdonType.AdOnTypeId IN (SELECT     AT.AdOnTypeId
                                FROM         AdonType as AT INNER JOIN
                                AdOnTypeInProduct ON AT.AdOnTypeId = AdOnTypeInProduct.AdonTypeID INNER JOIN
                                ProductsInBranches ON AdOnTypeInProduct.BrachProductID = ProductsInBranches.BranchProductID
                                WHERE (ProductsInBranches.ProductID = " + productid + " And ProductsInBranches.BranchID = " + branchid + ")))";

                        var adontype = (from at in entities.AdonTypes
                                        select at).ToList();
                        txtCbAdonType.DataSource = adontype.OrderBy(at => at.AdOnTypeName);
                        txtCbAdonType.DataBind();

                    }
                    else
                    {
                        ShowHide(false);
                        var countproduct = (from p in entities.Products
                                            join bp in entities.ProductsInBranches on p.ProductID equals bp.ProductID
                                            select p.ProductID).ToList();
                        if (countproduct.Count != product.ProductsInBranches.Count)
                        {
                            txtError.Text = "You can not set price for this product because its sub child are not existing in selected branch.";
                        }
                    }
                }
            }
            else
                txtError.Text = "No Product found in branch. Add Product in branch from Branch Managment.";

        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void BindAdons()
    {
        txtError.Text = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtCbAdonType.SelectedValue))
            {
                short adontypeid = short.Parse(txtCbAdonType.SelectedValue);
                List<Adon> adons = (from o in entities.Adons
                                    where o.AdonType == adontypeid
                                    select o).ToList();
                grdAdons.DataSource = adons;
            }
            else
                grdAdons.DataSource = null;
            grdAdons.DataBind();
        }
        catch (Exception ex)
        {
            txtError.Text = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
        }
    }

    private void ShowHide(bool val)
    {
        txtCbAdonType.Enabled = val;

    }

    #endregion
}