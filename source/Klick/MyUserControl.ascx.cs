using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessObjects;
using BusinessService;
using Core;
using Telerik.Web.UI;
using AdOnTypeInProduct = BusinessEntities.AdOnTypeInProduct;
using Adon = BusinessEntities.Adon;
using OptionTypesInProduct = BusinessEntities.OptionTypesInProduct;
using OrderDetailSubProduct = BusinessEntities.OrderDetailSubProduct;
using OrderDetailSubProductAdon = BusinessEntities.OrderDetailSubProductAdon;
using OrderDetailSubProductOption = BusinessEntities.OrderDetailSubProductOption;

public partial class MyUserControl : BaseUserControl
{
    private KlickEntities entities = new KlickEntities();
    private ProductManager _productManager = new ProductManager();

    #region Delegate
    // Delegate declaration
    public delegate void UpdatePrice(object sender, EventArgs e);

    // Event declaration
    public event UpdatePrice OnUpdatePrice;

    #endregion

    #region Properties

    /// <summary>
    /// Set Product Object
    /// </summary>
    public Products ProductObject { get; set; }

    private string selectedOption
    {
        get { return (string)ViewState["SelectedOption"]; }
        set { ViewState["SelectedOption"] = value; }
    }

    public int TabIndex { set; get; }


    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadControl();

            //ApplyShowHideRule();
        }
    }

    private void ApplyShowHideRule()
    {
        // Thin Crust / Regular
        IsThinCrustPanel.Visible = (ProductObject.CategoryID == PizzaCategoryId);


        // IsCustomizable
        if (ProductObject.IsCustomizable)
        {
            IsCustomizablePanel.Visible = true;
            BindDealOptions();
        }
        else
        {
            IsCustomizablePanel.Visible = false;
        }
    }

    private void LoadControl()
    {
        txtProductName.Text = ProductObject.Name;
        txtImage.ImageUrl = "~/Products/S_" + ProductObject.Image;
        txtDescription.Text = ProductObject.Description;
        if (ProductObject.OptionTypesInProductList != null && ProductObject.OptionTypesInProductList.Count > 0)
        {
            rptOptions.DataSource = ProductObject.OptionTypesInProductList;
            rptOptions.DataBind();
        }

        if (ProductObject.AdOnTypeInProductList != null && ProductObject.AdOnTypeInProductList.Count > 0)
        {
            //BindAdons();
            rptAdonsType.DataSource = ProductObject.AdOnTypeInProductList;
            rptAdonsType.DataBind();
        }
    }

    #region Data Bind Events

    protected void PizzaHalfSelection_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton button = sender as ImageButton;

        HiddenField SelectedSize = ((ImageButton)sender).Parent.FindControl("SelectedSize") as HiddenField;
        SelectedSize.Value = button.CommandArgument;

        CheckBox txtDouble = ((ImageButton)sender).Parent.FindControl("txtDouble") as CheckBox;
        txtDouble.Checked = false;
        //TODO: set all sibbling buttons to unselected.

        switch (button.CommandArgument)
        {
            case "0":
                button.ImageUrl = "~/image/None-Selected.png";

                break;
            case "1":
                button.ImageUrl = "~/image/Full_Selected.png"; button.CommandName = "2";


                break;
            case "2":

                button.ImageUrl = "~/image/FirstHalf_Selected.png"; button.CommandName = "2";

                break;
            case "3":

                button.ImageUrl = "~/image/2ndHalf_Selected.png"; button.CommandName = "2";

                break;
            case "4":
                if (txtDouble.Checked = button.CommandName.Equals("1"))
                {
                    button.ImageUrl = "~/image/Double_Selected.png"; button.CommandName = "2";
                }
                else
                {
                    button.ImageUrl = "~/image/Double_NotSelected.png"; button.CommandName = "1";
                }
                break;
        }
        if (OnUpdatePrice != null)
        {
            OnUpdatePrice(sender, e);
        }
    }

    protected void rptOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            OptionTypesInProduct obj = e.Item.DataItem as OptionTypesInProduct;
            if (obj.ProductOptionsList != null && obj.ProductOptionsList.Count > 0)
            {
                if (obj.IsMultiSelect)
                {
                    CheckBoxList chklist = e.Item.FindControl("CheckBoxList") as CheckBoxList;
                    if (obj.IsSamePrice)
                    {
                        chklist.AutoPostBack = false;
                    }
                    chklist.DataSource = obj.ProductOptionsList;
                    chklist.DataBind();
                }
                else
                {
                    RadioButtonList radiolist = e.Item.FindControl("RadioButtonList") as RadioButtonList;
                    if (obj.IsSamePrice)
                    {
                        radiolist.AutoPostBack = false;
                    }
                    else
                    {
                        radiolist.SelectedIndexChanged += new EventHandler(radiolist_SelectedIndexChanged);
                    }
                    radiolist.DataSource = obj.ProductOptionsList;
                    radiolist.DataBind();
                    if (obj.ProductOptionsList != null && obj.ProductOptionsList.Count == 1)
                    {
                        radiolist.Items[0].Selected = true;
                    }
                }
            }
        }
    }

    protected void rptAdonsType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        AdOnTypeInProduct obj = e.Item.DataItem as AdOnTypeInProduct;
        Repeater rptAdons = e.Item.FindControl("rptAdons") as Repeater;
        rptAdons.DataSource = obj.Adons;
        rptAdons.DataBind();

        Panel AdonHeading = e.Item.FindControl("AdonHeading") as Panel;
        Panel NoneFullHeading = e.Item.FindControl("NoneFullHeading") as Panel;
        if (obj.DisplayFormat == 1)
        {
            AdonHeading.Visible = true;
        }
        else if (obj.DisplayFormat == 4)
        {
            NoneFullHeading.Visible = true;
        }
    }

    protected void rptAdons_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item is RepeaterItem)
        {
            RepeaterItem item = e.Item;
            Adon obj = item.DataItem as Adon;
            if (obj != null)
            {
                Label adonname = item.FindControl("txtAdonName") as Label;
                adonname.Text = obj.AdOnName;

                if (obj.DisplayFormat == 1)
                {
                    RadioButtonList adonoptions = item.FindControl("AdonOptions") as RadioButtonList;
                    DefaultSelected(item, obj.DefaultSelected);
                }
                else if (obj.DisplayFormat == 2)
                {
                    RadioButton AdonRadioButton = item.FindControl("AdonRadioButton") as RadioButton;
                    AdonRadioButton.Visible = true;
                }
                else if (obj.DisplayFormat == 3)
                {
                    CheckBox AdonCheckBox = item.FindControl("AdonCheckBox") as CheckBox;
                    AdonCheckBox.Visible = true;
                }
                else if (obj.DisplayFormat == 4)
                {
                    DefaultSelected(item, obj.DefaultSelected, false, false, false);
                }
            }
        }
        //Adon obj = e.Item.DataItem as Adon;
        //if (obj != null)
        //{
        //    Label adonname = e.Item.FindControl("txtAdonName") as Label;
        //    adonname.Text = obj.AdOnName;
        //    if (obj.DisplayFormat == 1)
        //    {
        //        RadioButtonList adonoptions = e.Item.FindControl("AdonOptions") as RadioButtonList;
        //        adonoptions.Items[obj.DefaultSelected].Selected = true;
        //        adonoptions.Visible = true;
        //        if (obj.DefaultSelected != 0)
        //        {
        //            adonoptions.SelectedValue = obj.DefaultSelected.ToString();
        //        }
        //        CheckBox txtDouble = e.Item.FindControl("txtDouble") as CheckBox;
        //        txtDouble.Visible = true;
        //    }
        //    else if (obj.DisplayFormat == 2)
        //    {
        //        RadioButton AdonRadioButton = e.Item.FindControl("AdonRadioButton") as RadioButton;
        //        AdonRadioButton.Visible = true;
        //    }
        //    else if (obj.DisplayFormat == 3)
        //    {
        //        CheckBox AdonCheckBox = e.Item.FindControl("AdonCheckBox") as CheckBox;
        //        AdonCheckBox.Visible = true;
        //    }
        //    else if (obj.DisplayFormat == 4)
        //    {
        //        RadioButtonList adonoptions = e.Item.FindControl("NoneFullRadioButtonList") as RadioButtonList;
        //        adonoptions.Items[obj.DefaultSelected].Selected = true;
        //        adonoptions.Visible = true;
        //        if (obj.DefaultSelected != 0)
        //        {
        //            adonoptions.SelectedValue = obj.DefaultSelected.ToString();
        //        }                
        //    }
        //}

    }
    #endregion

    #region Radio Button,Check Box Events

    protected void chklist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnUpdatePrice != null)
        {
            OnUpdatePrice(sender, e);
        }
    }

    protected void radiolist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnUpdatePrice != null)
        {
            OnUpdatePrice(sender, e);
        }
    }

    protected void txtDouble_CheckedChanged(object sender, EventArgs e)
    {
        if (OnUpdatePrice != null)
        {
            OnUpdatePrice(sender, e);
        }
    }

    protected void adonoptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnUpdatePrice != null)
        {
            OnUpdatePrice(sender, e);
        }
    }

    protected void NoneFullRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnUpdatePrice != null)
        {
            OnUpdatePrice(sender, e);
        }
    }
    #endregion

    #endregion

    #region Private & Public Methods


    private void BindDealOptions()
    {
        List<Product> products = (from product in entities.Products
                                  where product.CategoryID == ProductObject.CategoryID
                                  select product).ToList();
        ddlDealOptions.DataSource = products;
        ddlDealOptions.DataTextField = "Name";
        ddlDealOptions.DataValueField = "ProductID";
        ddlDealOptions.DataBind();
    }


    private void BindAdons()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["BPID"]))
        {
            long branchproductid = long.Parse(Request.QueryString["BPID"]);
            ProductsInBranch productbranch = (from bp in entities.ProductsInBranches
                                              where bp.BranchProductID == branchproductid
                                              select bp).FirstOrDefault();

            List<BusinessObjects.AdOnTypeInProduct> adontypeslist = (from otp in entities.AdOnTypeInProducts
                                                                     join bp in entities.ProductsInBranches on otp.BrachProductID equals bp.BranchProductID
                                                                     where bp.BranchProductID == branchproductid
                                                                     select otp).ToList();
            if (adontypeslist.Count > 0)
                rptAdonsType.DataSource = adontypeslist;
            else
                rptAdonsType.DataSource = null;
        }
    }


    public Double GetPrice()
    {
        Double productPrice = ProductObject.UnitPrice, toppingPrice = 0;
        bool CalculateAdonPrice = false;
        if (rptOptions.Items.Count > 0)
        {
            foreach (RepeaterItem optionItem in rptOptions.Items)
            {
                HiddenField OptionTypeID = optionItem.FindControl("hdOptionTypeId") as HiddenField;
                OptionTypesInProduct optionTypeInProduct = ProductObject.OptionTypesInProductList.Where(ot => ot.OptionTypeID == Convert.ToInt16(OptionTypeID.Value)).FirstOrDefault();
                if (optionTypeInProduct.IsMultiSelect)
                {
                    CheckBoxList chklist = optionItem.FindControl("CheckBoxList") as CheckBoxList;
                    if (chklist != null)
                    {
                        bool changePrice = true;
                        foreach (ListItem checkBox in chklist.Items)
                        {
                            if (checkBox.Selected)
                            {
                                ProductOptions productOption = optionTypeInProduct.ProductOptionsList.Where(po => po.OptionID == Convert.ToInt32(checkBox.Value)).First();
                                if (optionTypeInProduct.IsSamePrice && optionTypeInProduct.IsProductPriceChangeType)
                                {
                                    if (changePrice)
                                    {
                                        if (productPrice == 0)
                                            productPrice = productOption.Price;
                                        else
                                            productPrice += productOption.Price;
                                        changePrice = false;
                                    }
                                }
                                else if (!optionTypeInProduct.IsSamePrice && optionTypeInProduct.IsProductPriceChangeType)
                                {
                                    productPrice += productOption.Price;
                                }
                                else if (optionTypeInProduct.IsSamePrice)
                                {
                                    productPrice = productOption.Price;
                                }
                            }
                        }
                    }
                }
                else
                {
                    RadioButtonList radiolist = optionItem.FindControl("RadioButtonList") as RadioButtonList;
                    if (radiolist != null && !String.IsNullOrEmpty(radiolist.SelectedValue))
                    {
                        CalculateAdonPrice = true;
                        if (optionTypeInProduct.IsSamePrice)
                        {
                            ProductOptions productOption = optionTypeInProduct.ProductOptionsList[0];
                            productPrice = productOption.Price;
                        }
                        else
                        {
                            ProductOptions productOption = optionTypeInProduct.ProductOptionsList.Where(po => po.OptionID == Convert.ToInt32(radiolist.SelectedValue)).FirstOrDefault();
                            productPrice = productOption.Price;
                            toppingPrice = productOption.ToppingPrice;
                        }
                    }
                    else
                    {
                        productPrice = ProductObject.UnitPrice;
                    }
                }
                if (CalculateAdonPrice)
                {
                    productPrice = CalculateToppingPrice(productPrice, toppingPrice);
                }
            }
        }
        else
        {
            productPrice = CalculateToppingPrice();
        }
        return productPrice;
    }

    private double CalculateToppingPrice()
    {
        Double productPrice = ProductObject.UnitPrice;
        Int16 freeToppingCount = 0;
        foreach (RepeaterItem adonTypeItem in rptAdonsType.Items)
        {
            HiddenField hdAdonTypeId = adonTypeItem.FindControl("hdAdonTypeId") as HiddenField;
            AdOnTypeInProduct adonType = ProductObject.AdOnTypeInProductList.Where(at => at.AdonTypeID == Convert.ToInt16(hdAdonTypeId.Value)).FirstOrDefault();

            Repeater rptAdons = adonTypeItem.FindControl("rptAdons") as Repeater;
            foreach (RepeaterItem adonItem in rptAdons.Items)
            {
                HiddenField hdAdonId = adonItem.FindControl("hdAdonId") as HiddenField;
                Adon adon = adonType.Adons.Where(a => a.AdOnID == Convert.ToInt32(hdAdonId.Value)).FirstOrDefault();
                if (adonType.DisplayFormat == 1)
                {

                    //RadioButtonList txtAdonOptions = adonItem.FindControl("AdonOptions") as RadioButtonList;
                    HiddenField selectedSize = adonItem.FindControl("SelectedSize") as HiddenField;
                    short selectedoption = short.Parse(selectedSize.Value);
                    CheckBox txtDouble = adonItem.FindControl("txtDouble") as CheckBox;
                    //short selectedoption = short.Parse(txtAdonOptions.SelectedValue);
                    if (adon.DefaultSelected == 0 && (selectedoption == 1 || selectedoption == 2 || selectedoption == 3))
                    {       // When None was default selected and user has changed default selected option
                        if (!adonType.IsFreeAdonType)
                        {
                            if (txtDouble.Checked)
                            {
                                if (freeToppingCount == ProductObject.NumberOfFreeTopping)
                                {
                                    productPrice += (2 * Convert.ToDouble(adonType.Price));
                                }
                                else
                                {
                                    productPrice += Convert.ToDouble(adonType.Price);
                                    freeToppingCount++;
                                }
                            }
                            else
                            {
                                if (freeToppingCount == ProductObject.NumberOfFreeTopping)
                                {
                                    productPrice += Convert.ToDouble(adonType.Price);
                                }
                                else
                                    freeToppingCount++;
                            }
                        }
                    }
                    else  // When other than None is pre selected and user has also selected Double check box
                    {
                        if (txtDouble.Checked && selectedoption != 0)
                        {
                            productPrice += Convert.ToDouble(adonType.Price);
                        }
                        else
                        {
                            txtDouble.Checked = false;
                        }
                    }
                }
                else if (adonType.DisplayFormat == 2 || adonType.DisplayFormat == 3)
                {
                }
                else if (adonType.DisplayFormat == 4)
                {
                    RadioButtonList NoneFullRadioButtonList = adonItem.FindControl("NoneFullRadioButtonList") as RadioButtonList;
                    if (!NoneFullRadioButtonList.SelectedValue.Equals("0"))
                    {
                        short selectedoption = short.Parse(NoneFullRadioButtonList.SelectedValue);
                        if (adon.DefaultSelected == 0 && selectedoption == 1)
                        {
                            if (!adonType.IsFreeAdonType)
                            {
                                productPrice += Convert.ToDouble(adonType.Price);
                            }
                        }
                    }
                }
            }
        }
        return productPrice;
    }

    private double CalculateToppingPrice(Double productPrice, Double toppingPrice)
    {
        Int16 freeToppingCount = 0;
        foreach (RepeaterItem adonTypeItem in rptAdonsType.Items)
        {
            HiddenField hdAdonTypeId = adonTypeItem.FindControl("hdAdonTypeId") as HiddenField;
            AdOnTypeInProduct adonType = ProductObject.AdOnTypeInProductList.Where(at => at.AdonTypeID == Convert.ToInt16(hdAdonTypeId.Value)).FirstOrDefault();

            Repeater rptAdons = adonTypeItem.FindControl("rptAdons") as Repeater;
            foreach (RepeaterItem adonItem in rptAdons.Items)
            {
                HiddenField hdAdonId = adonItem.FindControl("hdAdonId") as HiddenField;
                Adon adon = adonType.Adons.Where(a => a.AdOnID == Convert.ToInt32(hdAdonId.Value)).FirstOrDefault();
                if (adonType.DisplayFormat == 1)
                {
                    //RadioButtonList txtAdonOptions = adonItem.FindControl("AdonOptions") as RadioButtonList;
                    CheckBox txtDouble = adonItem.FindControl("txtDouble") as CheckBox;
                    //short selectedoption = short.Parse(txtAdonOptions.SelectedValue);

                    HiddenField SelectedSize = adonItem.FindControl("SelectedSize") as HiddenField;
                    short selectedoption = short.Parse(SelectedSize.Value);

                    if (adon.DefaultSelected == 0 && selectedoption != 0)//(selectedoption == 1 || selectedoption == 2 || selectedoption == 3))
                    {       // When None was default selected and user has changed default selected option
                        if (!adonType.IsFreeAdonType)
                        {
                            if (txtDouble.Checked)
                            {
                                if (freeToppingCount == ProductObject.NumberOfFreeTopping)
                                {
                                    if (toppingPrice > 0)
                                        productPrice += 2 * toppingPrice;
                                    else
                                        productPrice += (2 * Convert.ToDouble(adonType.Price));
                                }
                                else
                                {
                                    if (toppingPrice > 0)
                                        productPrice += toppingPrice;
                                    else
                                        productPrice += Convert.ToDouble(adonType.Price);
                                    freeToppingCount++;
                                }
                            }
                            else
                            {
                                if (freeToppingCount == ProductObject.NumberOfFreeTopping)
                                {
                                    if (toppingPrice > 0)
                                        productPrice += toppingPrice;
                                    else
                                        productPrice += Convert.ToDouble(adonType.Price);
                                }
                                else
                                    freeToppingCount++;
                            }
                        }
                    }
                    else  // When other than None is pre selected and user has also selected Double check box
                    {
                        if (txtDouble.Checked && selectedoption != 0)
                        {
                            if (toppingPrice > 0)
                                productPrice += toppingPrice;
                            else
                                productPrice += Convert.ToDouble(adonType.Price);
                        }
                        else
                        {
                            txtDouble.Checked = false;
                        }
                    }
                    UnSelectOtherSize(adonItem, selectedoption);
                }
                else if (adonType.DisplayFormat == 2 || adonType.DisplayFormat == 3)
                {
                }
                else if (adonType.DisplayFormat == 4)
                {
                    RadioButtonList NoneFullRadioButtonList = adonItem.FindControl("NoneFullRadioButtonList") as RadioButtonList;
                    short selectedoption = short.Parse(NoneFullRadioButtonList.SelectedValue);

                    if (!NoneFullRadioButtonList.SelectedValue.Equals("0"))
                    {
                        if (adon.DefaultSelected == 0 && selectedoption == 1)
                        {
                            if (!adonType.IsFreeAdonType)
                            {
                                if (toppingPrice > 0)
                                    productPrice += toppingPrice;
                                else
                                    productPrice += Convert.ToDouble(adonType.Price);
                            }
                        }
                    }
                    UnSelectOtherSize(adonItem, selectedoption);
                }
            }
        }
        return productPrice;
    }

    public OrderDetailSubProduct GetOrderDetailSubProduct()
    {
        Double productPrice = 0, toppingPrice = 0;
        bool CalculateAdonPrice = false;

        #region Order Detail Sub Product

        OrderDetailSubProduct orderDetailSubProduct = new OrderDetailSubProduct();
        orderDetailSubProduct.ProductId = ProductObject.ProductID;
        orderDetailSubProduct.Quantity = 1;
        orderDetailSubProduct.ProductName = ProductObject.Name;
        orderDetailSubProduct.RecipientName = txtRecipientName.Text.Trim();
        orderDetailSubProduct.Comments = txtInstruction.Text;
        orderDetailSubProduct.OrderDetailSubProductAdons = new List<OrderDetailSubProductAdon>();
        orderDetailSubProduct.OrderDetailSubProductOptions = new List<OrderDetailSubProductOption>();

        if (ProductObject.CategoryID == PizzaCategoryId)
        {
            orderDetailSubProduct.CrustType = (rblThinCrust.SelectedValue.ConvertToBool()) ? "Thin Crust" : "Regular";
        }
    

        #endregion

        foreach (RepeaterItem optionItem in rptOptions.Items)
        {
            #region Order Detail Sub Product Option

            #endregion
            HiddenField OptionTypeID = optionItem.FindControl("hdOptionTypeId") as HiddenField;
            OptionTypesInProduct optionTypeInProduct = ProductObject.OptionTypesInProductList.Where(ot => ot.OptionTypeID == Convert.ToInt16(OptionTypeID.Value)).FirstOrDefault();
            if (optionTypeInProduct.IsMultiSelect)
            {
                CheckBoxList chklist = optionItem.FindControl("CheckBoxList") as CheckBoxList;
                if (chklist != null)
                {
                    bool changePrice = true;
                    foreach (ListItem checkBox in chklist.Items)
                    {
                        if (checkBox.Selected)
                        {
                            ProductOptions productOption = optionTypeInProduct.ProductOptionsList.Where(po => po.OptionID == Convert.ToInt32(checkBox.Value)).First();
                            if (optionTypeInProduct.IsSamePrice && optionTypeInProduct.IsProductPriceChangeType)
                            {
                                if (changePrice)
                                {
                                    if (productPrice == 0)
                                        productPrice = productOption.Price;
                                    else
                                        productPrice += productOption.Price;
                                    changePrice = false;
                                }
                            }
                            else if (!optionTypeInProduct.IsSamePrice && optionTypeInProduct.IsProductPriceChangeType)
                            {
                                productPrice += productOption.Price;
                            }
                            else if (optionTypeInProduct.IsSamePrice)
                            {
                                productPrice += productOption.Price;
                            }
                            OrderDetailSubProductOption orderDetailSubProductOption = new OrderDetailSubProductOption();
                            orderDetailSubProductOption.ProductOptionId = productOption.OptionID;
                            orderDetailSubProductOption.ProductOptionName = productOption.OptionName;
                            orderDetailSubProductOption.Price = productOption.Price;
                            orderDetailSubProductOption.ProductOptionTypeName = optionTypeInProduct.OptionTypeName;
                            orderDetailSubProduct.OrderDetailSubProductOptions.Add(orderDetailSubProductOption);
                        }
                    }
                }
            }
            else
            {
                RadioButtonList radiolist = optionItem.FindControl("RadioButtonList") as RadioButtonList;
                if (radiolist != null && !String.IsNullOrEmpty(radiolist.SelectedValue))
                {
                    
                    OrderDetailSubProductOption orderDetailSubProductOption = new OrderDetailSubProductOption();
                    if (optionTypeInProduct.IsSamePrice)
                    {
                        ProductOptions productOption = optionTypeInProduct.ProductOptionsList.Where(po => po.OptionID == Convert.ToInt32(radiolist.SelectedValue)).FirstOrDefault();
                        productPrice = optionTypeInProduct.ProductOptionsList[0].Price;
                        orderDetailSubProductOption.ProductOptionName = productOption.OptionName;
                        orderDetailSubProductOption.ProductOptionTypeName = optionTypeInProduct.OptionTypeName;
                        CalculateAdonPrice = optionTypeInProduct.IsAdonPriceVary;
                    }
                    else
                    {
                        ProductOptions productOption = optionTypeInProduct.ProductOptionsList.Where(po => po.OptionID == Convert.ToInt32(radiolist.SelectedValue)).FirstOrDefault();

                        orderDetailSubProductOption.ProductOptionId = productOption.OptionID;
                        orderDetailSubProductOption.ProductOptionName = productOption.OptionName;
                        orderDetailSubProductOption.Price = productOption.Price;
                        orderDetailSubProductOption.Price = optionTypeInProduct.ProductOptionsList[0].Price;
                        orderDetailSubProductOption.ProductOptionTypeName = optionTypeInProduct.OptionTypeName;
                        CalculateAdonPrice = optionTypeInProduct.IsAdonPriceVary;
                    }
                    orderDetailSubProduct.OrderDetailSubProductOptions.Add(orderDetailSubProductOption);
                }
                else
                {
                    productPrice = ProductObject.UnitPrice;
                }
            }
            if (CalculateAdonPrice)
            {

                Int16 freeToppingCount = 0;
                foreach (RepeaterItem adonTypeItem in rptAdonsType.Items)
                {
                    HiddenField hdAdonTypeId = adonTypeItem.FindControl("hdAdonTypeId") as HiddenField;
                    AdOnTypeInProduct adonType = ProductObject.AdOnTypeInProductList.Where(at => at.AdonTypeID == Convert.ToInt16(hdAdonTypeId.Value)).FirstOrDefault();

                    Repeater rptAdons = adonTypeItem.FindControl("rptAdons") as Repeater;
                    foreach (RepeaterItem adonItem in rptAdons.Items)
                    {
                        Double adonPrice = 0;
                        HiddenField hdAdonId = adonItem.FindControl("hdAdonId") as HiddenField;
                        Adon adon = adonType.Adons.Where(a => a.AdOnID == Convert.ToInt32(hdAdonId.Value)).FirstOrDefault();

                        HiddenField SelectedSize = adonItem.FindControl("SelectedSize") as HiddenField;
                        short selectedoption = short.Parse(SelectedSize.Value);

                        CheckBox txtDouble = adonItem.FindControl("txtDouble") as CheckBox;

                        OrderDetailSubProductAdon orderDetailSubAdon = new OrderDetailSubProductAdon();
                        orderDetailSubAdon.AdOnId = adon.AdOnID;
                        orderDetailSubAdon.AdonName = adon.AdOnName;
                        orderDetailSubAdon.AdonTypeName = adonType.AdOnTypeName;
                        orderDetailSubAdon.SelectedAdonOption = selectedoption;
                        orderDetailSubAdon.IsDoubleSelected = txtDouble.Checked;
                        if (adon.DefaultSelected == 0 && (selectedoption == 1 || selectedoption == 2 || selectedoption == 3))
                        {       // When None was default selected and user has changed default selected option
                            if (!adonType.IsFreeAdonType)
                            {
                                if (txtDouble.Checked)
                                {
                                    if (freeToppingCount == ProductObject.NumberOfFreeTopping)
                                    {
                                        if (toppingPrice > 0)
                                            adonPrice += 2 * toppingPrice;
                                        else
                                            adonPrice += (2 * Convert.ToDouble(adonType.Price));
                                    }
                                    else
                                    {
                                        if (toppingPrice > 0)
                                            productPrice += toppingPrice;
                                        else
                                            productPrice += Convert.ToDouble(adonType.Price);
                                        freeToppingCount++;
                                    }
                                }
                                else
                                {
                                    if (freeToppingCount == ProductObject.NumberOfFreeTopping)
                                    {
                                        if (toppingPrice > 0)
                                        {
                                            productPrice += toppingPrice;
                                            orderDetailSubAdon.Price = toppingPrice;
                                        }
                                        else
                                        {
                                            productPrice += Convert.ToDouble(adonType.Price);
                                            orderDetailSubAdon.Price = Convert.ToDouble(adonType.Price);
                                        }

                                    }
                                    else
                                        freeToppingCount++;
                                }
                            }
                            orderDetailSubProduct.OrderDetailSubProductAdons.Add(orderDetailSubAdon);
                        }
                        else  // When other than None is pre selected and user has also selected Double check box
                        {
                            if (txtDouble.Checked && selectedoption != 0)
                            {
                                if (toppingPrice > 0)
                                    adonPrice += toppingPrice;
                                else
                                    adonPrice += Convert.ToDouble(adonType.Price);
                                orderDetailSubAdon.Price = adonPrice;
                                orderDetailSubProduct.OrderDetailSubProductAdons.Add(orderDetailSubAdon);
                            }
                            else
                            {
                                txtDouble.Checked = false;
                            }
                        }

                        productPrice += adonPrice;

                    }
                }
            } // CalculateAdonPrice
        }// rptOptions

        orderDetailSubProduct.Price = productPrice;


        return orderDetailSubProduct;
    }

    #endregion

    private void UnSelectOtherSize(RepeaterItem childitem, Int16 selectedSize)
    {
        ImageButton None = childitem.FindControl("None") as ImageButton;
        ImageButton First = childitem.FindControl("First") as ImageButton;
        ImageButton Full = childitem.FindControl("Full") as ImageButton;
        ImageButton Second = childitem.FindControl("Second") as ImageButton;
        ImageButton Double = childitem.FindControl("Double") as ImageButton;

        switch (selectedSize)
        {
            case 0:
                First.CommandName = Full.CommandName = Second.CommandName = Double.CommandName = "1";
                First.ImageUrl = "~/image/FirstHalf_NotSelected.png";
                Full.ImageUrl = "~/image/Full_NotSelected.png";
                Second.ImageUrl = "~/image/2ndHalf_NotSelected.png";
                Double.ImageUrl = "~/image/Double_NotSelected.png";
                break;
            case 1:
                None.CommandName = First.CommandName = Second.CommandName = "1";
                None.ImageUrl = "~/image/None-NotSelected.png";
                First.ImageUrl = "~/image/FirstHalf_NotSelected.png";
                Second.ImageUrl = "~/image/2ndHalf_NotSelected.png";

                break;
            case 2:
                None.CommandName = Full.CommandName = Second.CommandName = "1";
                None.ImageUrl = "~/image/None-NotSelected.png";
                Full.ImageUrl = "~/image/Full_NotSelected.png";
                Second.ImageUrl = "~/image/2ndHalf_NotSelected.png";
                break;
            case 3:
                None.CommandName = First.CommandName = Second.CommandName = "1";
                None.ImageUrl = "~/image/None-NotSelected.png";
                First.ImageUrl = "~/image/FirstHalf_NotSelected.png";
                Full.ImageUrl = "~/image/Full_NotSelected.png";
                break;
        }
    }

    private void DefaultSelected(RepeaterItem childitem, Int16 defaultSelected, bool showFirstHalf = true, bool showSecondHalf = true, bool showDouble = true)
    {
        ImageButton None = childitem.FindControl("None") as ImageButton;
        ImageButton First = childitem.FindControl("First") as ImageButton;
        ImageButton Full = childitem.FindControl("Full") as ImageButton;
        ImageButton Second = childitem.FindControl("Second") as ImageButton;
        ImageButton Double = childitem.FindControl("Double") as ImageButton;
        HiddenField SelectedSize = childitem.FindControl("SelectedSize") as HiddenField;
        First.Visible = showFirstHalf;
        Second.Visible = showSecondHalf;
        Double.Visible = showDouble;
        switch (defaultSelected)
        {
            case 0:
                None.CommandName = "2";
                None.ImageUrl = "~/image/None-Selected.png";
                break;
            case 1:
                Full.CommandName = "2";
                Full.ImageUrl = "~/image/Full_Selected.png";
                SelectedSize.Value = "1";
                None.CommandName = "2";
                None.ImageUrl = "~/image/None-NotSelected.png";

                break;
            case 2:
                First.CommandName = "2";
                First.ImageUrl = "~/image/FirstHalf_Selected.png";
                SelectedSize.Value = "1";
                None.CommandName = "2";
                None.ImageUrl = "~/image/None-NotSelected.png";
                break;
            case 3:
                Second.CommandName = "2";
                Second.ImageUrl = "~/image/2ndHalf_Selected.png";
                SelectedSize.Value = "1";
                None.CommandName = "2";
                None.ImageUrl = "~/image/None-NotSelected.png";
                break;
        }
    }

    protected void ddlDealOptions_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Products product = GetProductDetailByProductId();

        if (!String.IsNullOrEmpty(product.OptionTypeInProductXml))
        {
            product.OptionTypesInProductList = Utility.XmlToObjectList<OptionTypesInProduct>(product.OptionTypeInProductXml, "//OptionTypesInProduct");
        }
        if (!String.IsNullOrEmpty(product.AdonTypeInProducctXml))
        {
            product.AdOnTypeInProductList = Utility.XmlToObjectList<AdOnTypeInProduct>(product.AdonTypeInProducctXml, "//AdonTypesInProduct");
        }

        ProductObject = product;   // Will throw exception here, have to change GetProductDetails function to return only one product
        LoadControl();
    }


    private Products GetProductDetailByProductId()
    {
        return _productManager.GetProductDetailByProductId(Convert.ToInt32(ddlDealOptions.SelectedValue), ProductObject.ComboId);
    }

    private string GetThinCrustValue()
    {
        if (rblThinCrust != null)
            return (rblThinCrust.SelectedValue.ConvertToBool() ? "Thin Crust" : "Regular");
        return "";
    }
}