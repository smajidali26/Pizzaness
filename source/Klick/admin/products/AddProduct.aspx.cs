using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using Telerik.Web.UI;
using System.Configuration;
using System.Transactions;
using System.Data;
using BusinessService;
using System.Drawing;
using Core;


public partial class admin_products_AddProduct : BasePage
{
    #region Private Members

    private ProductManager productManager = new ProductManager();
    private Products parentProduct = null;
    private bool inEditMode = false;
    private ProductCategoryManager productCategoryManager = new ProductCategoryManager();
    
    #endregion

    #region Properties

    /// <summary>
    /// Products that are added as a deal product
    /// </summary>
    public ICollection<ProductsChildRelationship> DealProducts { get; set; }

    private ICollection<ProductOptions> DealExistingOptions { get; set; }

    private ICollection<ProductAdon> DealExistingAdons { get; set; }
    #endregion

    #region Page Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetProductCategories();
            GetProductById();
        }
    }    

    #endregion

    #region Button Events

    protected void SaveProductBtn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtProductName.Text) && !string.IsNullOrEmpty(txtProductCategory.SelectedValue))
        {
            SaveProduct();
        }
        else
            ShowMessage("You must enter all values.", MessageType.Error);
    }

    protected void SaveChildProductBtn_Click(object sender, EventArgs e)
    {
        AddChildProducts();
    }

    protected void ButtonSaveOptions_Click(object sender, EventArgs e)
    {
        SaveProductChildProductOptions();
        //SaveProductChildAdons();
    }

    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Products.aspx");
    }

    #endregion

    #region Grid Evetns

    protected void grdProducts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            Products product = e.Item.DataItem as Products;

            if (DealProducts != null)
            {
                ProductsChildRelationship dealProduct = DealProducts.Where(dp => dp.ChildProductId == product.ProductID).FirstOrDefault();
                if (dealProduct != null)
                {
                    CheckBox chk = item.FindControl("OptionCheckBox") as CheckBox;
                    RadNumericTextBox quantity = item.FindControl("Quantity") as RadNumericTextBox;
                    RadNumericTextBox unitPrice = item.FindControl("UnitPrice") as RadNumericTextBox;
                    RadNumericTextBox FreeTopping = item.FindControl("FreeTopping") as RadNumericTextBox;
                    CheckBox isCustomizable = item.FindControl("AllowCustomization") as CheckBox;

                    chk.Checked = true;
                    quantity.Text = dealProduct.Quantity.ToString();
                    unitPrice.Text = dealProduct.UnitPrice.ToString();
                    FreeTopping.Text = dealProduct.NumberOfFreeTopping.ToString();
                    isCustomizable.Checked = dealProduct.IsCustomizable;

                }
            }
        }
    }

    protected void grdProducts_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        GetProductsForDeal();
    }
    
    protected void ComboOptions_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {

    }

    protected void ComboOptions_ItemDataBound(object sender, GridItemEventArgs e)
    {
        
        if (e.Item is GridDataItem)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            DataRowView option = e.Item.DataItem as DataRowView;
            if (DealExistingOptions != null)
            {
                ProductOptions options = DealExistingOptions.Where(op => op.OptionID == (Int32)option["OptionID"]).FirstOrDefault();
                if (options != null)
                {
                    CheckBox chk = item.FindControl("OptionCheckBox") as CheckBox;
                    RadNumericTextBox OptionPrice = item.FindControl("OptionPrice") as RadNumericTextBox;
                    chk.Checked = true;
                    OptionPrice.Text = options.Price.ToString();

                }
            }
        }
        

    }

    #endregion

    #region Other Events
    
    protected void txtProductCategory_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetProductsForDeal(); 
        grdProducts.DataBind();
    }

    #endregion

    #region Methods

    private void GetProductById()
    {
        int id = 0;
        if (!String.IsNullOrEmpty(QueryStringParamID) && Int32.TryParse(QueryStringParamID,out id))
        {
            Products product = productManager.GetProductById(id);
            if (product != null)
            {
                txtProductName.Text = product.Name;
                txtDescription.Text = product.Description;
                if (product.DisplayOrder != 0)
                    txtDisplayOrder.Text = product.DisplayOrder.ToString();
                txtProductCategory.SelectedValue = product.CategoryID.ToString();
                if (!product.IsSpecial)
                {
                    txtNo.Checked = true;
                }
                else
                {
                    product.ProductActivationObject = Utility.XmlToObject<ProductActivation>(product.ProductActivationObj);
                    parentProduct = product;
                    txtYes.Checked = true;
                    if (!product.ProductActivationObject.DisplayOnFullDay)
                    {
                        rblTimeSpan.SelectedValue = "0";
                        rtpStartTime.SelectedTime = TimeSpan.Parse(product.ProductActivationObject.StartTime);
                        rtpEndTime.SelectedTime = TimeSpan.Parse(product.ProductActivationObject.EndTime);
                    }
                    else
                    {
                        rblTimeSpan.SelectedValue = "1";
                    }
                    
                    foreach (ListItem item in chkListDays.Items)
                    {
                        if (product.ProductActivationObject.Days.Contains( item.Value))
                        {
                            item.Selected = true;
                        }
                    }
                    RadTabStrip1.Tabs[0].Visible = true;
                    RadTabStrip1.Tabs[1].Visible = true;
                    RadTabStrip1.Tabs[2].Visible = true;
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadMultiPage1.PageViews[0].Selected = true;
                    inEditMode = true;
                    if (product.IsSpecial)
                    {
                        GetExistingDealProducts(id);
                        GetDealOptionsByProductId(id);
                        //GetDealAdonsByProductId(id);
                        GetProductsForDeal();
                        GetOptionsForDeal();
                        grdProducts.DataBind();
                    }
                    //GetAdonsForDeal();
                    //ComboAdons.DataBind();
                }
                ViewStateID = id;
            }
        }
    }

    private void GetExistingDealProducts(Int32 productId)
    {
        DealProducts = productManager.GetDealProductsByProductId(productId);
    }

    private void SaveProduct()
    {
        try
        {
            Products product = new Products();
            product.ProductActivationObject = new ProductActivation();
            product.Name = txtProductName.Text;
            product.IsSpecial = (txtYes.Checked) ? true : false;
            product.IsActive = true;
            product.Description = txtDescription.Text;
            product.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            product.CategoryID = short.Parse(txtProductCategory.SelectedValue);
            product.ImagePath = "~/Products/";
            if (product.IsSpecial)
            {                 
                String days = String.Empty;
                foreach (ListItem item in chkListDays.Items)
                {
                    if (item.Selected)
                    {
                        days += item.Value + ",";
                    }
                }
                product.ProductActivationObject.Days = days = days.TrimEnd(new char[] { ',' });
                product.ProductActivationObject.DisplayOnFullDay = (rblTimeSpan.SelectedValue.Equals("1")) ? true : false;
                if (!product.ProductActivationObject.DisplayOnFullDay)
                {
                    product.ProductActivationObject.StartTime = rtpStartTime.SelectedTime.Value.ToString();
                    product.ProductActivationObject.EndTime = rtpEndTime.SelectedTime.Value.ToString();
                }
                else
                {
                    product.ProductActivationObject.StartTime = null;
                    product.ProductActivationObject.EndTime = null;
                }
            }
            if (ViewStateID != null)
                product.ProductID = (Int32)ViewStateID;
            string filename = string.Empty;
            if (txtProductImage.UploadedFiles.Count > 0)
                filename = Guid.NewGuid().ToString() + txtProductImage.UploadedFiles[0].GetExtension();
            
            product.Image = filename;
                
            int result = 0;
            if (ViewStateID == null)
            {
                result = productManager.AddProduct(product);
                product.ProductID = result;
            }
            else
                result = productManager.UpdateProduct(product);
            if (result > 0)
            {
                if (txtProductImage.UploadedFiles.Count > 0 && txtProductImage.UploadedFiles[0].ContentLength > 0)
                {
                    String path = Server.MapPath("~/Products/");
                    txtProductImage.UploadedFiles[0].SaveAs(path + filename);
                    ThumbImage(path + filename, path + "L_" + filename, 180, 180);
                    ThumbImage(path + filename, path + "S_" + filename, 60, 60);
                }
                if (txtNo.Checked)
                {
                    SessionMessage = product.Name + " has been saved successfully.";
                    Response.Redirect("Products.aspx");
                }
                else if (txtYes.Checked)
                {
                    RadTabStrip1.Tabs[0].Visible = true;
                    RadTabStrip1.Tabs[1].Visible = true;
                    RadTabStrip1.Tabs[1].Selected = true;
                    RadMultiPage1.PageViews[1].Selected = true;
                    ShowMessage(product.Name + " has been saved successfully.", MessageType.Success);
                    GetExistingDealProducts(product.ProductID);
                    GetProductsForDeal();
                    grdProducts.DataBind();
                    ViewStateID = product.ProductID;
                }


            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error occured. Error detail -> " + ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message), MessageType.Error);
        }
    }

    private void AddChildProducts()
    {
        GridItemCollection collection = grdProducts.Items;
        int MainProductID = 0;
        bool validate = true;
        if (!string.IsNullOrEmpty(QueryStringParamID))
        {
            MainProductID = int.Parse(QueryStringParamID);
        }
        else if (ViewStateID != null)
        {
            MainProductID = (int)ViewStateID;
        }


        ICollection<ProductsChildRelationship> comboItems = new List<ProductsChildRelationship>();
        foreach (GridItem item in collection)
        {
            GridEditableItem editableItem = item as GridEditableItem;
            int id = (int)editableItem.GetDataKeyValue("ProductID");
            CheckBox chk = item.FindControl("OptionCheckBox") as CheckBox;
            RadNumericTextBox quantity = item.FindControl("Quantity") as RadNumericTextBox;
            RadNumericTextBox unitPrice = item.FindControl("UnitPrice") as RadNumericTextBox;
            RadNumericTextBox FreeTopping = item.FindControl("FreeTopping") as RadNumericTextBox;
            CheckBox isCustomizable = item.FindControl("AllowCustomization") as CheckBox;

            if (chk.Checked)
            {
                if (!String.IsNullOrEmpty(quantity.Text) && !String.IsNullOrEmpty(unitPrice.Text))
                {
                    ProductsChildRelationship childProduct = new ProductsChildRelationship();
                    childProduct.ParentProductsId = MainProductID;
                    childProduct.ChildProductId = id;
                    childProduct.Quantity = Convert.ToInt16(quantity.Text);
                    childProduct.UnitPrice = Convert.ToDouble(unitPrice.Text);
                    childProduct.IsCustomizable = isCustomizable.Checked;
                    if (!String.IsNullOrEmpty(FreeTopping.Text))
                    {
                        childProduct.NumberOfFreeTopping = Convert.ToInt16(FreeTopping.Text);
                    }
                    comboItems.Add(childProduct);
                }
                else
                {
                    validate = false;
                    break;
                }
            }

        }

        if (validate)
        {
            if (comboItems.Count > 0)
            {
                int result = productManager.AddComboDealProducts(comboItems);
                ShowMessage("Combo items are saved successfully.", MessageType.Success);
                GetDealOptionsByProductId(MainProductID);
                GetOptionsForDeal();
                ComboOptions.DataBind();
                //GetAdonsForDeal();
                //ComboAdons.DataBind();
                RadTabStrip1.Tabs[2].Visible = true;
                RadTabStrip1.Tabs[2].Selected = true;
                RadMultiPage1.PageViews[2].Selected = true;

            }
            else
            {
                ShowMessage("You have selected no option for combo deal.", MessageType.Error);
            }
        }
        else
        {
            ShowMessage("Error! Missing values. Please fill out Quantity and Unit price when adding as combo deal.", MessageType.Error);
        }

    }

    private void SaveProductChildProductOptions()
    {
        GridItemCollection collection = ComboOptions.Items;
        ICollection<ProductChildProductOption> optionItems = new List<ProductChildProductOption>();
        bool validate = true;
        foreach (GridItem item in collection)
        {
            GridEditableItem editableItem = item as GridEditableItem;
            
            int OptionID = (int)editableItem.GetDataKeyValue("OptionID");
            CheckBox OptionCheckBox = item.FindControl("OptionCheckBox") as CheckBox;
            HiddenField ComboId = item.FindControl("ComboId") as HiddenField;
            RadNumericTextBox OptionPrice = item.FindControl("OptionPrice") as RadNumericTextBox;
            if (OptionCheckBox.Checked)
            {
                if (!String.IsNullOrEmpty(OptionPrice.Text))
                {
                    ProductChildProductOption obj = new ProductChildProductOption();
                    obj.ComboId = Convert.ToInt32(ComboId.Value);
                    obj.OptionId = OptionID;
                    obj.Price = Convert.ToDouble(OptionPrice.Text);
                    optionItems.Add(obj);
                }
                else
                {
                    ShowMessage( "You have selected to add option but not entered price of option.",MessageType.Error);
                    validate = false;
                    break;
                }
            }
            
        }

        if (validate)
        {
            if (optionItems.Count > 0)
            {
                if (productManager.AddProductChildProductOption(optionItems) > 0)
                {
                    Response.Redirect("Products.aspx");
                }
            }
            else
            {
                ShowMessage("You have selected no option",MessageType.Error);
            }
        }
    }
    
    private void GetProductCategories()
    {
        ICollection<ProductCategories> list = productCategoryManager.GetProductCategories();

        txtProductCategory.DataSource = list;
        txtProductCategory.DataBind();
    }

    private void GetProductsForDeal()
    {
        try
        {
            ICollection<Products> productsList = productManager.GetProductsForDeal();
            grdProducts.DataSource = productsList;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GetOptionsForDeal()
    {
        Int32 MainProductID = 0;
        if (!string.IsNullOrEmpty(QueryStringParamID))
        {
            MainProductID = int.Parse(QueryStringParamID);
        }
        else if (ViewStateID != null)
        {
            MainProductID = (int)ViewStateID;
        }

        ComboOptions.DataSource = productManager.GetOptionsForComboProducts(MainProductID);
    }

    private void GetDealOptionsByProductId(Int32 productId)
    {
        DealExistingOptions = productManager.GetDealOptionsByProductId(productId);
    }

    private void GetDealAdonsByProductId(Int32 productId)
    {
        DealExistingAdons = productManager.GetDealAdonsByProductId(productId);
    }

    #endregion

}
