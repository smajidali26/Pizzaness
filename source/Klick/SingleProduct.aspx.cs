using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using Core;
using Telerik.Web.UI;
using BusinessService;

public partial class SingleProduct : BasePage
{
    private KlickEntities entities = new KlickEntities();
    private BranchManager _branchManager = new BranchManager();
    private decimal productprice = 0;
    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["PID"]))
            {
                int pid = int.Parse(Request.QueryString["PID"]);
                Product product = (from p in entities.Products
                                   where p.ProductID == pid
                                   select p).FirstOrDefault();
                if (product != null)
                {
                    txtProductName.Text = product.Name;
                    txtImage.ImageUrl = "~/Products/S_" + product.Image;
                    txtDescription.Text = product.Description;                    
                }

                BindOptions();
                txtOptions.DataBind();
                BindAdons();
                txtAdonsList.DataBind();
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsClosed"]) && (_branchManager.IsStoreClose(BranchId) || IsStoreClosed()))
                {
                    AddToCartButton.Visible = false;
                }

            //ApplyShowHideRule(product);
            }
        }
    }

    private void ApplyShowHideRule(Product product)
    {
        // Thin Crust / Regular
        int category = ConfigurationManager.AppSettings["CategoryId"].ConvertToInt();
        IsThinCrustPanel.Visible = (product.CategoryID == category);
    }

    private void BindOptions()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["BPID"]))
        {
            long branchproductid = long.Parse(Request.QueryString["BPID"]);
            ProductsInBranch productbranch = (from bp in entities.ProductsInBranches
                                              where bp.BranchProductID == branchproductid
                                              select bp).FirstOrDefault();
            if (!IsPostBack)
            {
                txtNetPrice.Text = Decimal.Round((Decimal)productbranch.Price, 2).ToString();
            }
            List<OptionTypesInProduct> optiontypeslist = (from otp in entities.OptionTypesInProducts
                                                          join bp in entities.ProductsInBranches on otp.BranchProductID equals bp.BranchProductID
                                                          where bp.BranchProductID == branchproductid
                                                          select otp).ToList();
            if (optiontypeslist.Count > 0)
                txtOptions.DataSource = optiontypeslist;
            else
                txtOptions.DataSource = null;
        }
    }

    private void BindAdons()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["BPID"]))
        {
            long branchproductid = long.Parse(Request.QueryString["BPID"]);
            ProductsInBranch productbranch = (from bp in entities.ProductsInBranches
                                              where bp.BranchProductID == branchproductid
                                              select bp).FirstOrDefault();            
            List<AdOnTypeInProduct> adontypeslist = (from otp in entities.AdOnTypeInProducts
                                                          join bp in entities.ProductsInBranches on otp.BrachProductID equals bp.BranchProductID
                                                          where bp.BranchProductID == branchproductid
                                                          select otp).ToList();
            if (adontypeslist.Count > 0)
                txtAdonsList.DataSource = adontypeslist;
            else
                txtAdonsList.DataSource = null;

        }   
    }

    protected void txtOptions_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        if (e.Item is RadListViewDataItem)
        {
            RadListViewDataItem item = e.Item as RadListViewDataItem;
            OptionTypesInProduct obj = item.DataItem as OptionTypesInProduct;
            List<ProductOption> optionlist = (from po in entities.ProductOptions
                                              select po).ToList();
            var results = (from po in optionlist
                           join pop in obj.ProductOptionsInProducts on po.OptionID equals pop.ProductOptionID
                           orderby pop.DisplayOrder
                           select new { po.OptionName, pop.ProductOptionsInProductID }).ToList();
            if (obj.IsMultiSelect)
            {
                CheckBoxList chklist = item.FindControl("CheckBoxList") as CheckBoxList;
                if (obj.IsSamePrice)
                {
                    chklist.AutoPostBack = false;
                }                
                chklist.DataSource = results;                
                chklist.DataBind();
            }
            else
            {
                RadioButtonList radiolist = item.FindControl("RadioButtonList") as RadioButtonList;
                RequiredFieldValidator rfvOptionSelect = item.FindControl("rfvOptionSelect") as RequiredFieldValidator;
                if (obj.IsSamePrice)
                {
                    radiolist.AutoPostBack = false;
                }
                rfvOptionSelect.Visible = true;
                radiolist.DataSource = results;
                radiolist.DataBind();
            }
        }
    }

    protected void radiolist_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList radiolist = sender as RadioButtonList;
        GetPrice();
    }

    protected void chklist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPrice();
    }

    protected void txtOptions_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        BindOptions();
    }

    protected void txtAdonsList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {

    }

    protected void txtAdonsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        if (e.Item is RadListViewDataItem)
        {
            RadListViewDataItem item = e.Item as RadListViewDataItem;
            AdOnTypeInProduct obj = item.DataItem as AdOnTypeInProduct;
            RadListView adons = item.FindControl("txtAdons") as RadListView;
            Panel AdonHeading = item.FindControl("AdonHeading") as Panel;
            Panel NoneFullHeading = item.FindControl("NoneFullHeading") as Panel;  
            adons.DataSource = obj.ProductAdons.OrderBy(pd=>pd.Adon.AdOnName);
            adons.DataBind();
            if (obj.DisplayFormat == 1)
            {
                AdonHeading.Visible = true;
            }
            else if (obj.DisplayFormat == 4)
            {
                NoneFullHeading.Visible = true;
            }
        }
    }

    protected void Adons_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        if (e.Item is RadListViewDataItem)
        {
            RadListViewDataItem item = e.Item as RadListViewDataItem;
            ProductAdon obj = item.DataItem as ProductAdon;
            if (obj != null)
            {
                Label adonname = item.FindControl("txtAdonName") as Label;
                adonname.Text = obj.Adon.AdOnName;
                
                if (obj.AdOnTypeInProduct.DisplayFormat == 1)
                {
                    RadioButtonList adonoptions = item.FindControl("AdonOptions") as RadioButtonList;
                    DefaultSelected(item, obj.DefaultSelected);
                }
                else if (obj.AdOnTypeInProduct.DisplayFormat == 2)
                {
                    RadioButton AdonRadioButton = item.FindControl("AdonRadioButton") as RadioButton;
                    AdonRadioButton.Visible = true;
                }
                else if (obj.AdOnTypeInProduct.DisplayFormat == 3)
                {
                    CheckBox AdonCheckBox = item.FindControl("AdonCheckBox") as CheckBox;
                    AdonCheckBox.Visible = true;
                }
                else if (obj.AdOnTypeInProduct.DisplayFormat == 4)
                {
                    DefaultSelected(item, obj.DefaultSelected,false,false,false);
                }
            }
        }
    }

    protected void txtDouble_CheckedChanged(object sender, EventArgs e)
    {
        GetPrice();
    }

    protected void adonoptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPrice();
    }

    protected void PizzaHalfSelection_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton button = sender as ImageButton;
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
                if (button.CommandName.Equals("1"))
                {
                    button.ImageUrl = "~/image/Double_Selected.png"; button.CommandName = "2";
                }
                else
                {
                    button.ImageUrl = "~/image/Double_NotSelected.png"; button.CommandName = "1";
                }
                break;
        }
        GetPrice();
    }

    protected void NoneFullRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPrice();
    }

    private void GetPrice()
    {
        try
        {
            long branchproductid = long.Parse(Request.QueryString["BPID"]);
            ProductsInBranch bpObj = (from bp in entities.ProductsInBranches
                                      where bp.BranchProductID == branchproductid
                                      select bp).FirstOrDefault();
            if (bpObj != null)
            {
                if (bpObj.AddToCheckOut != null && bpObj.AddToCheckOut.Value)
                    productprice = (Decimal)bpObj.CheckOutPrice;
                else
                    productprice = (Decimal)bpObj.Price;

                #region Options
                
                RadListViewDataItemCollection optioncollection = txtOptions.Items;
                decimal adonpricefromOptionType = 0;
                foreach (RadListViewDataItem item in optioncollection)
                {
                    long id = (long)item.GetDataKeyValue("ProductsOptionTypeId");
                    OptionTypesInProduct OTP = (from otp in entities.OptionTypesInProducts
                                                where otp.ProductsOptionTypeId == id
                                                select otp).FirstOrDefault();
                    
                    if (OTP.IsSamePrice)
                    {
                        if (OTP.IsProductPriceChangeType)
                            productprice = (decimal) OTP.ProductOptionsInProducts.ToList()[0].Price;
                    }
                    else
                    {
                        if (OTP.IsMultiSelect)
                        {
                            CheckBoxList chkList = item.FindControl("CheckBoxList") as CheckBoxList;
                            foreach (ListItem listitem in chkList.Items)
                            {
                                if (listitem.Selected)
                                {
                                    long POP_ID = long.Parse(listitem.Value); ////Product Option In Product ID
                                    ProductOptionsInProduct POP_obj = (from pop in entities.ProductOptionsInProducts
                                                                       where pop.ProductOptionsInProductID == POP_ID
                                                                       select pop).FirstOrDefault();
                                    productprice += (decimal)POP_obj.Price;                                    
                                }
                            }
                        }
                        else
                        {
                            RadioButtonList radioList = item.FindControl("RadioButtonList") as RadioButtonList;
                            foreach (ListItem listitem in radioList.Items)
                            {
                                if (listitem.Selected)
                                {
                                    long POP_ID = long.Parse(listitem.Value); ////Product Option In Product ID
                                    ProductOptionsInProduct POP_obj = (from pop in entities.ProductOptionsInProducts
                                                                       where pop.ProductOptionsInProductID == POP_ID
                                                                       select pop).FirstOrDefault();
                                    
                                    if ((bool)OTP.IsProductPriceChangeType)
                                    {
                                        productprice = (decimal)POP_obj.Price;
                                        if (POP_obj.ToppingPrice != null)
                                            adonpricefromOptionType = (decimal)POP_obj.ToppingPrice;
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                RadListViewDataItemCollection adoncollection = txtAdonsList.Items;
                foreach (RadListViewDataItem mainitem in adoncollection)
                {
                    RadListView txtAdons = mainitem.FindControl("txtAdons") as RadListView;
                    long productadontypeid = (long)mainitem.GetDataKeyValue("ProductsAdOnTypeId");
                    AdOnTypeInProduct adontypeinproduct = (from ATP in entities.AdOnTypeInProducts
                                                           where ATP.ProductsAdOnTypeId == productadontypeid
                                                           select ATP).FirstOrDefault();

                    RadListViewDataItemCollection childadoncollection = txtAdons.Items;
                    foreach (RadListViewDataItem childitem in childadoncollection)
                    {
                        long id = (long)childitem.GetDataKeyValue("ProductAdOnID");
                        ProductAdon productadonObj = (from PA in entities.ProductAdons
                                                      where PA.ProductAdOnID == id
                                                      select PA).FirstOrDefault();
                        if (productadonObj != null)
                        {
                            if (productadonObj.AdOnTypeInProduct.DisplayFormat == 1)
                            {
                                HiddenField SelectedSize = childitem.FindControl("SelectedSize") as HiddenField;
                                HiddenField IsDouble = childitem.FindControl("IsDouble") as HiddenField;
                                short selectedoption = 0,isDouble = 0;
                                
                                    
                                selectedoption = short.Parse(SelectedSize.Value);
                                isDouble = short.Parse(IsDouble.Value);
                                
                                if (selectedoption != 0)
                                {
                                    if (productadonObj.DefaultSelected == 0 && (selectedoption == 1 || selectedoption == 2 || selectedoption == 3))
                                    {
                                        if (!productadonObj.Adon.AdonType1.IsFreeAdonType)
                                        {
                                            if (isDouble == 1)
                                            {
                                                if (adonpricefromOptionType > 0)
                                                    productprice += 2 * adonpricefromOptionType;
                                                else
                                                    productprice += (2 * (decimal)adontypeinproduct.Price);
                                            }
                                            else
                                            {
                                                if (adonpricefromOptionType > 0)
                                                    productprice += adonpricefromOptionType;
                                                else
                                                    productprice += (decimal)adontypeinproduct.Price;
                                            }
                                        }
                                        else
                                        {
                                            if (isDouble == 1)
                                            {
                                                if (adonpricefromOptionType > 0)
                                                    productprice += adonpricefromOptionType;
                                                else
                                                    productprice += (decimal)adontypeinproduct.Price;
                                            }
                                        }
                                    }
                                    else if (isDouble == 1)
                                    {
                                        if (adonpricefromOptionType > 0)
                                            productprice += adonpricefromOptionType;
                                        else
                                            productprice += (decimal)adontypeinproduct.Price;
                                    }
                                }
                                else
                                {
                                    ImageButton Double = childitem.FindControl("Double") as ImageButton;
                                    IsDouble = childitem.FindControl("IsDouble") as HiddenField;
                                    Double.ImageUrl = "~/image/Double_NotSelected.png";
                                    IsDouble.Value = "0";
                                }
                                UnSelectOtherSize(childitem,selectedoption);
                                
                            }
                            else if (productadonObj.AdOnTypeInProduct.DisplayFormat == 2 || productadonObj.AdOnTypeInProduct.DisplayFormat == 3)
                            {
                                if (!productadonObj.Adon.AdonType1.IsFreeAdonType)
                                {
                                    if (adonpricefromOptionType > 0)
                                        productprice += adonpricefromOptionType;
                                    else
                                        productprice += (decimal)adontypeinproduct.Price;
                                }
                            }
                            else if (productadonObj.AdOnTypeInProduct.DisplayFormat == 4)
                            {
                                HiddenField SelectedSize = childitem.FindControl("SelectedSize") as HiddenField;
                                HiddenField IsDouble = childitem.FindControl("IsDouble") as HiddenField;
                                short selectedoption = 0, isDouble = 0;

                                selectedoption = short.Parse(SelectedSize.Value);
                                isDouble = short.Parse(IsDouble.Value);
                                
                                if (productadonObj.DefaultSelected == 0 && selectedoption == 1)
                                {
                                    if (!productadonObj.Adon.AdonType1.IsFreeAdonType)
                                    {
                                        productprice += (decimal)adontypeinproduct.Price;                                            
                                    }
                                }
                                UnSelectOtherSize(childitem, selectedoption);
                            }
                        }
                    }
                }
                txtNetPrice.Text = Decimal.Round(productprice, 2).ToString(); 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void AddToCartButton_Click(object sender, EventArgs e)
    {
        BusinessEntities.Orders order = SessionUserOrder;
        if (order == null)
        {
            order = new BusinessEntities.Orders();
        }
            

        try
        {
            long branchproductid = long.Parse(Request.QueryString["BPID"]);
            ProductsInBranch bpObj = (from bp in entities.ProductsInBranches
                                      where bp.BranchProductID == branchproductid
                                      select bp).FirstOrDefault();
            if (bpObj != null)
            {
                if (order.OrderDetailsList == null)
                {
                    order.OrderDetailsList = new List<BusinessEntities.OrderDetails>();
                    SessionOrderAdonList = new List<List<BusinessEntities.OrderDetailAdOns>>();
                    SessionOrderDetailOptionList = new List<List<BusinessEntities.OrderDetailOptions>>();
                    order.OrderStatusID = BusinessEntities.OrderStatus.NewOrder;
                }
                BusinessEntities.OrderDetails orderdetail = new BusinessEntities.OrderDetails();

                if (bpObj.Product.CategoryID == PizzaCategoryId)
                {
                    string thinCrust = GetThinCrustValue();
                    orderdetail.CrustType = thinCrust;
                }
                orderdetail.Price = Convert.ToDouble(bpObj.Price);
                orderdetail.CategoryName = bpObj.Product.ProductCategory.Name;
                orderdetail.ProductName = txtProductName.Text;
                orderdetail.ProductID = bpObj.ProductID;
                orderdetail.ProductImage = bpObj.Product.Image;
                orderdetail.Quantity = Convert.ToInt32(txtQuantity.Value);
                if (!string.IsNullOrEmpty(txtRecipientName.Text))
                    orderdetail.RecipientName = txtRecipientName.Text;
                if (!string.IsNullOrEmpty(txtInstruction.Text))
                    orderdetail.Comments = txtInstruction.Text;
                decimal adonpricefromOptionType = 0;
                adonpricefromOptionType = GetOrderOptions(orderdetail, adonpricefromOptionType);

                GetOrderAdons(orderdetail, adonpricefromOptionType);

                orderdetail.ItemTotal = orderdetail.Price * orderdetail.Quantity;
                orderdetail.OrderDetailID = order.OrderDetailsList.Count + 1;
                order.OrderDetailsList.Add(orderdetail);
                order.OrderTotal += orderdetail.ItemTotal;
                SessionUserOrder = order;
                SessionUserOrderTotal = order.OrderTotal;
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", "<script type='text/javascript'>CloseOnReload()</script>");

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string GetThinCrustValue()
    {
            if (rblThinCrust != null)
                return (rblThinCrust.SelectedValue.ConvertToBool() ? "Thin Crust" : "Regular");
        return "";
    }

    private void GetOrderAdons(BusinessEntities.OrderDetails orderdetail, decimal adonpricefromOptionType)
    {
        RadListViewDataItemCollection adoncollection = txtAdonsList.Items;
        List<BusinessEntities.OrderDetailAdOns> list = new List<BusinessEntities.OrderDetailAdOns>();
        foreach (RadListViewDataItem mainitem in adoncollection)
        {
            Label txtAdonType = mainitem.FindControl("txtAdonTypeName") as Label;
            RadListView txtAdons = mainitem.FindControl("txtAdons") as RadListView;
            long productadontypeid = (long)mainitem.GetDataKeyValue("ProductsAdOnTypeId");
            AdOnTypeInProduct adontypeinproduct = (from ATP in entities.AdOnTypeInProducts
                                                   where ATP.ProductsAdOnTypeId == productadontypeid
                                                   select ATP).FirstOrDefault();

            RadListViewDataItemCollection childadoncollection = txtAdons.Items;
            foreach (RadListViewDataItem childitem in childadoncollection)
            {
                long id = (long)childitem.GetDataKeyValue("ProductAdOnID");
                ProductAdon productadonObj = (from PA in entities.ProductAdons
                                              where PA.ProductAdOnID == id
                                              select PA).FirstOrDefault();
                if (productadonObj != null)
                {
                    BusinessEntities.OrderDetailAdOns orderdetailadon = new BusinessEntities.OrderDetailAdOns();
                    orderdetailadon.AdOnId = productadonObj.Adon.AdOnID;
                    orderdetailadon.AdonName = productadonObj.Adon.AdOnName;
                    orderdetailadon.AdonTypeName = txtAdonType.Text;

                    if (productadonObj.AdOnTypeInProduct.DisplayFormat == 1)
                    {
                        HiddenField SelectedSize = childitem.FindControl("SelectedSize") as HiddenField;
                        HiddenField IsDouble = childitem.FindControl("IsDouble") as HiddenField;
                        short selectedoption = 0, isDouble = 0;


                        selectedoption = short.Parse(SelectedSize.Value);
                        isDouble = short.Parse(IsDouble.Value);
                        if (selectedoption != 0)
                        {
                            orderdetailadon.SelectedAdonOption = (BusinessEntities.SelectedOption)selectedoption;
                            if (productadonObj.DefaultSelected == 0 && (selectedoption == 1 || selectedoption == 2 || selectedoption == 3))
                            {
                                if (!productadonObj.Adon.AdonType1.IsFreeAdonType)
                                {
                                    if (isDouble == 1)
                                    {
                                        if (adonpricefromOptionType > 0)
                                        {
                                            orderdetail.Price += Convert.ToDouble(2 * adonpricefromOptionType);
                                        }
                                        else
                                        {
                                            orderdetail.Price += Convert.ToDouble(2 * (decimal)adontypeinproduct.Price);

                                        }
                                        orderdetailadon.IsDoubleSelected = true;
                                    }
                                    else
                                    {
                                        if (adonpricefromOptionType > 0)
                                        {
                                            orderdetail.Price += Convert.ToDouble(adonpricefromOptionType);

                                        }
                                        else
                                        {
                                            orderdetail.Price += Convert.ToDouble((decimal)adontypeinproduct.Price);

                                        }
                                        orderdetailadon.IsDoubleSelected = false;
                                    }
                                }
                                else
                                {
                                    orderdetail.Price += Convert.ToDouble(adontypeinproduct.Price);
                                }
                                list.Add(orderdetailadon);
                            }
                            else if (isDouble == 1)
                            {
                                if (adonpricefromOptionType > 0)
                                {
                                    orderdetail.Price += Convert.ToDouble(adonpricefromOptionType);

                                }
                                else
                                {
                                    orderdetail.Price += Convert.ToDouble((decimal)adontypeinproduct.Price);
                                }
                                orderdetailadon.IsDoubleSelected = true;
                                list.Add(orderdetailadon);
                            }
                            else if (productadonObj.DefaultSelected == 1)
                            {
                                orderdetail.Price += 0;
                                orderdetailadon.IsDoubleSelected = false;
                                list.Add(orderdetailadon);
                            }
                        }
                    }
                    else if (productadonObj.AdOnTypeInProduct.DisplayFormat == 2)
                    {
                        list.Add(orderdetailadon);
                    }
                    else if (productadonObj.AdOnTypeInProduct.DisplayFormat == 4)
                    {
                        HiddenField SelectedSize = childitem.FindControl("SelectedSize") as HiddenField;
                        short selectedoption = 0;
                        selectedoption = short.Parse(SelectedSize.Value);

                        orderdetailadon.SelectedAdonOption = (BusinessEntities.SelectedOption)selectedoption;
                        if (productadonObj.DefaultSelected == 0 && selectedoption == 1)
                        {
                            if (!productadonObj.Adon.AdonType1.IsFreeAdonType)
                            {
                                orderdetail.Price += Convert.ToDouble((decimal)adontypeinproduct.Price);
                            }
                        }
                        list.Add(orderdetailadon);
                    }
                }
            }
        }
        SessionOrderAdonList.Add(list);
    }

    private decimal GetOrderOptions(BusinessEntities.OrderDetails orderdetail, decimal adonpricefromOptionType)
    {
        RadListViewDataItemCollection optioncollection = txtOptions.Items;
        List<BusinessEntities.OrderDetailOptions> list = new List<BusinessEntities.OrderDetailOptions>();
        foreach (RadListViewDataItem item in optioncollection)
        {
            long id = (long)item.GetDataKeyValue("ProductsOptionTypeId");
            OptionTypesInProduct OTP = (from otp in entities.OptionTypesInProducts
                                        where otp.ProductsOptionTypeId == id
                                        select otp).FirstOrDefault();
            if (OTP.IsSamePrice && !OTP.IsMultiSelect)
            {
                RadioButtonList radioList = item.FindControl("RadioButtonList") as RadioButtonList;
                foreach (ListItem listitem in radioList.Items)
                {
                    if (listitem.Selected)
                    {
                        long POP_ID = long.Parse(listitem.Value); ////Product Option In Product ID
                        ProductOptionsInProduct POP_obj = (from pop in entities.ProductOptionsInProducts
                                                           where pop.ProductOptionsInProductID == POP_ID
                                                           select pop).FirstOrDefault();
                        if (OTP.IsProductPriceChangeType)
                            orderdetail.Price = Convert.ToDouble(POP_obj.Price);

                        if ((bool)OTP.IsProductPriceChangeType && OTP.IsAdonPriceVary)
                        {
                            if (POP_obj.ToppingPrice != null)
                            {
                                adonpricefromOptionType = (decimal)POP_obj.ToppingPrice;

                            }
                        }
                        BusinessEntities.OrderDetailOptions orderdetailoption = new BusinessEntities.OrderDetailOptions();
                        orderdetailoption.ProductOptionId = POP_obj.ProductOptionID;
                        orderdetailoption.ProductOptionName = POP_obj.ProductOption.OptionName;
                        orderdetailoption.ProductOptionTypeName =
                            POP_obj.OptionTypesInProduct.OptionType.OptionDisplayText;
                        if (OTP.IsProductPriceChangeType)
                            orderdetailoption.Price = Convert.ToDouble(POP_obj.Price.Value);
                        list.Add(orderdetailoption);
                    }
                }

            }
            else
            {
                if (OTP.IsMultiSelect)
                {
                    #region CheckBox List
                    
                    CheckBoxList chkList = item.FindControl("CheckBoxList") as CheckBoxList;
                    foreach (ListItem listitem in chkList.Items)
                    {
                        if (listitem.Selected)
                        {
                            long POP_ID = long.Parse(listitem.Value); ////Product Option In Product ID
                            ProductOptionsInProduct POP_obj = (from pop in entities.ProductOptionsInProducts
                                                               where pop.ProductOptionsInProductID == POP_ID
                                                               select pop).FirstOrDefault();
                            orderdetail.Price += Convert.ToDouble(POP_obj.Price);

                            BusinessEntities.OrderDetailOptions orderdetailoption = new BusinessEntities.OrderDetailOptions();
                            orderdetailoption.ProductOptionId = POP_obj.ProductOptionID;
                            orderdetailoption.ProductOptionName = POP_obj.ProductOption.OptionName;
                            orderdetailoption.Price = Convert.ToDouble(POP_obj.Price.Value);
                            orderdetailoption.ProductOptionTypeName = POP_obj.OptionTypesInProduct.OptionType.OptionDisplayText;
                            list.Add(orderdetailoption);
                        }
                    }
                    #endregion

                }
                else
                {
                    RadioButtonList radioList = item.FindControl("RadioButtonList") as RadioButtonList;
                    foreach (ListItem listitem in radioList.Items)
                    {
                        #region 
                        
                        if (listitem.Selected)
                        {
                            long POP_ID = long.Parse(listitem.Value); ////Product Option In Product ID
                            ProductOptionsInProduct POP_obj = (from pop in entities.ProductOptionsInProducts
                                                               where pop.ProductOptionsInProductID == POP_ID
                                                               select pop).FirstOrDefault();
                            orderdetail.Price = Convert.ToDouble(POP_obj.Price);

                            if ((bool)OTP.IsProductPriceChangeType)
                            {
                                if (POP_obj.ToppingPrice != null)
                                {
                                    adonpricefromOptionType = (decimal)POP_obj.ToppingPrice;

                                }
                            }
                            BusinessEntities.OrderDetailOptions orderdetailoption = new BusinessEntities.OrderDetailOptions();
                            orderdetailoption.ProductOptionId = POP_obj.ProductOptionID;
                            orderdetailoption.ProductOptionName = POP_obj.ProductOption.OptionName;
                            orderdetailoption.ProductOptionTypeName =
                            POP_obj.OptionTypesInProduct.OptionType.OptionDisplayText;
                            orderdetailoption.Price = Convert.ToDouble(POP_obj.Price);
                            list.Add(orderdetailoption);

                        }
                        #endregion

                    }
                }
            }
        }
        SessionOrderDetailOptionList.Add(list);
        return adonpricefromOptionType;
    }

    private void UnSelectOtherSize(RadListViewDataItem childitem,Int16 selectedSize)
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

    private void DefaultSelected(RadListViewDataItem childitem, Int16 defaultSelected,bool showFirstHalf = true,bool showSecondHalf = true,bool showDouble = true)
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
    
}