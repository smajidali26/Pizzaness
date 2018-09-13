using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Transactions;
using System.Data;
using DataAccess;
using Telerik.Web.UI;
using BusinessService;
using BusinessEntities;
using Core;

public partial class MultiProduct : BasePage
{
    #region Private Methods
    
    private Double netProductPrice = 0;
    private ProductManager _productManager = new ProductManager();
    private BranchManager _branchManager = new BranchManager();
    public String test = string.Empty;

    #endregion

    #region Properties
    private ICollection<Products> ProductList { get; set; }
    #endregion

    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {

        ICollection<Products> list = GetProductsByProductId();
        //rptProducts.DataSource = list;
        //rptProducts.DataBind();
        int i = 0;
        ProductList = list;
        foreach (Products product in list)
        {
            i++;
            AddTab(product.Name, false, i);

            RadPageView pageView = new RadPageView();
            pageView.ID = product.Name + i;
            pageView.TabIndex = (short)i;
            Control pageViewContents = LoadControl("MyUserControl.ascx");
            ((MyUserControl)pageViewContents).TabIndex = i;
            ((MyUserControl)pageViewContents).ProductObject = product;
            ((MyUserControl)pageViewContents).OnUpdatePrice += MultiProduct_OnUpdatePrice;
            pageViewContents.ID = product.ProductID + "_userControl_" + i;
            pageView.Controls.Add(pageViewContents);
            RadMultiPage1.PageViews.Add(pageView);

        }
        if (list.Count == 1)
        {
            NextButton.Visible = false;
            AddToCartButton.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadTabStrip1.Tabs[0].Enabled = true;
            RadTabStrip1.Tabs[0].Selected = true;
            RadMultiPage1.PageViews[0].Enabled = true;
            RadMultiPage1.PageViews[0].Selected = true;
            ViewState["CurrentTab"] = 0;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsClosed"]) && (_branchManager.IsStoreClose(BranchId) || IsStoreClosed()))
            {
                NextButton.Visible = false;
                AddToCartButton.Visible = false;
            }
        }
    }
    
    #region Buttons Events
    protected void AddToCartButton_Click(object sender, EventArgs e)
    {
        Products comboproduct = _productManager.GetProductById(Convert.ToInt32(Request.QueryString["PID"]));
        if (comboproduct != null)
        {
            try
            {
                Orders order = SessionUserOrder;
                if (order == null)
                {
                    order = new BusinessEntities.Orders();
                }

                if (order.OrderDetailsList == null)
                {
                    order.OrderDetailsList = new List<BusinessEntities.OrderDetails>();
                    SessionOrderAdonList = new List<List<BusinessEntities.OrderDetailAdOns>>();
                    SessionOrderDetailOptionList = new List<List<BusinessEntities.OrderDetailOptions>>();
                    order.OrderStatusID = BusinessEntities.OrderStatus.NewOrder;
                }

                #region Order Detail
                
                BusinessEntities.OrderDetails orderdetail = new BusinessEntities.OrderDetails();
                orderdetail.Price = comboproduct.DefaultBranchProductPrice;
                orderdetail.CategoryName = comboproduct.CategoryName;
                orderdetail.ProductName = comboproduct.Name;
                orderdetail.ProductID = comboproduct.ProductID;
                orderdetail.ProductImage = comboproduct.Image;
                orderdetail.Quantity = 1;
                orderdetail.IsGroupProduct = true;
               
                orderdetail.OrderDetailSubProducts = new List<OrderDetailSubProduct>();

                #endregion

                Double price = 0;
                foreach (RadPageView pageView in RadMultiPage1.PageViews)
                {

                    price += ((MyUserControl) pageView.Controls[0]).GetPrice();
                    var orderDetailObj = ((MyUserControl) pageView.Controls[0]).GetOrderDetailSubProduct();
                    orderdetail.OrderDetailSubProducts.Add(orderDetailObj);
                    orderdetail.RecipientName = orderDetailObj.RecipientName;
                }


                #region Code Commit
                /*RepeaterItemCollection gridItems = rptProducts.Items;
                foreach (RepeaterItem productItem in gridItems)
                {
                    Double productPrice = 0, toppingPrice = 0;
                    bool CalculateAdonPrice = false;
                    HiddenField ProductID = productItem.FindControl("hdProductId") as HiddenField;
                    RadTextBox RecipientName = productItem.FindControl("txtRecipientName") as RadTextBox;
                    RadTextBox Instruction = productItem.FindControl("txtInstruction") as RadTextBox;
                    Products product = products.Where(p => p.ProductID == Convert.ToInt32(ProductID.Value)).First();

                    #region Order Detail Sub Product

                    OrderDetailSubProduct orderDetailSubProduct = new OrderDetailSubProduct();
                    orderDetailSubProduct.ProductId = product.ProductID;
                    orderDetailSubProduct.Quantity = 1;
                    orderDetailSubProduct.RecipientName = RecipientName.Text;
                    orderDetailSubProduct.Comments = Instruction.Text;

                    #endregion


                    Repeater rptOptions = productItem.FindControl("rptOptions") as Repeater;

                    foreach (RepeaterItem optionItem in rptOptions.Items)
                    {
                        #region Order Detail Sub Product Option
                       
                        #endregion
                        HiddenField OptionTypeID = optionItem.FindControl("hdOptionTypeId") as HiddenField;
                        OptionTypesInProduct optionTypeInProduct = product.OptionTypesInProductList.Where(ot => ot.OptionTypeID == Convert.ToInt16(OptionTypeID.Value)).FirstOrDefault();
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
                                        ProductOptions productOption = optionTypeInProduct.ProductOptionsList[0];
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
                                        else if (optionTypeInProduct.IsProductPriceChangeType)
                                        {
                                            productPrice += productOption.Price;
                                        }
                                        OrderDetailSubProductOption orderDetailSubProductOption = new OrderDetailSubProductOption();
                                        orderDetailSubProductOption.ProductOptionId = productOption.OptionID;
                                        orderDetailSubProductOption.ProductOptionName = productOption.OptionName;
                                        orderDetailSubProductOption.Price = productOption.Price;
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
                                productPrice = product.UnitPrice;
                            }
                        }
                        if (CalculateAdonPrice)
                        {
                            Repeater rptAdonsType = productItem.FindControl("rptAdonsType") as Repeater;
                            Int16 freeToppingCount = 0;
                            foreach (RepeaterItem adonTypeItem in rptAdonsType.Items)
                            {
                                HiddenField hdAdonTypeId = adonTypeItem.FindControl("hdAdonTypeId") as HiddenField;
                                AdOnTypeInProduct adonType = product.AdOnTypeInProductList.Where(at => at.AdonTypeID == Convert.ToInt16(hdAdonTypeId.Value)).FirstOrDefault();

                                Repeater rptAdons = adonTypeItem.FindControl("rptAdons") as Repeater;
                                foreach (RepeaterItem adonItem in rptAdons.Items)
                                {
                                    Double adonPrice = 0;
                                    HiddenField hdAdonId = adonItem.FindControl("hdAdonId") as HiddenField;
                                    Adon adon = adonType.Adons.Where(a => a.AdOnID == Convert.ToInt32(hdAdonId.Value)).FirstOrDefault();
                                    RadioButtonList txtAdonOptions = adonItem.FindControl("AdonOptions") as RadioButtonList;
                                    CheckBox txtDouble = adonItem.FindControl("txtDouble") as CheckBox;
                                    short selectedoption = short.Parse(txtAdonOptions.SelectedValue);
                                    OrderDetailSubProductAdon orderDetailSubAdon = new OrderDetailSubProductAdon();
                                    orderDetailSubAdon.AdOnId = adon.AdOnID;
                                    orderDetailSubAdon.AdonName = adon.AdOnName;
                                    orderDetailSubAdon.SelectedAdonOption = selectedoption;
                                    orderDetailSubAdon.IsDoubleSelected = txtDouble.Checked;
                                    if (adon.DefaultSelected == 0 && (selectedoption == 1 || selectedoption == 2 || selectedoption == 3))
                                    {       // When None was default selected and user has changed default selected option
                                        if (!adonType.IsFreeAdonType)
                                        {
                                            if (txtDouble.Checked)
                                            {
                                                if (freeToppingCount == product.NumberOfFreeTopping)
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
                                                if (freeToppingCount == product.NumberOfFreeTopping)
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
                                                adonPrice += toppingPrice;
                                            else
                                                adonPrice += Convert.ToDouble(adonType.Price);
                                        }
                                        else
                                        {
                                            txtDouble.Checked = false;
                                        }
                                    }
                                    orderDetailSubAdon.Price = adonPrice;
                                    productPrice += adonPrice;
                                }
                            }
                        } // CalculateAdonPrice
                    }

                    netProductPrice += productPrice;
                    orderDetailSubProduct.Price = productPrice;
                }*/
#endregion

                orderdetail.ItemTotal = orderdetail.Price = price;
                order.OrderDetailsList.Add(orderdetail);


                //Add order details to log table when added to cart
                
                // Prepare data for log
                string sessionID = HttpContext.Current.Session.SessionID;
                string recepientName = orderdetail.RecipientName == "" ? null : orderdetail.RecipientName;
                string productDetail = string.Empty;
                string adons = string.Empty;

                foreach (OrderDetailSubProduct product in orderdetail.OrderDetailSubProducts)
                {
                    productDetail += product.ProductName + ": ";
                    foreach (var p in product.OrderDetailSubProductOptions)
                    {
                        productDetail += p.ProductOptionName + ",";
                    }
                    foreach (OrderDetailSubProductAdon adon in product.OrderDetailSubProductAdons)
                    {
                        adons += adon.AdOnId + ",";
                    }
                }

                //productDetail = productDetail[productDetail.Length - 1] == ','
                //                    ? productDetail.Substring(0, productDetail.Length - 2)
                //                    : productDetail;

                //adons = adons[adons.Length - 1] == ','
                //                    ? adons.Substring(0, adons.Length - 2)
                //                    : adons;

                productDetail = productDetail.Trim(',');
                adons = adons.TrimEnd(',');

                productDetail = productDetail == "" ? null : productDetail;
                adons = adons == "" ? null : adons;
                // get log message to pass to db
                string message = Common.AddToCartLogMessage(orderdetail.CategoryName, orderdetail.Price,
                                                            adons, productDetail, SessionUserFullName);

                // Log order details
                LogManager log = new LogManager();
                log.SaveLogData(sessionID, LogLevel.INFO.ToString(), Logger.AddToCart.ToString(), message, null);

                List<BusinessEntities.OrderDetailAdOns> list = new List<BusinessEntities.OrderDetailAdOns>();
                SessionOrderAdonList.Add(list);
                List<BusinessEntities.OrderDetailOptions> list1 = new List<BusinessEntities.OrderDetailOptions>();
                SessionOrderDetailOptionList.Add(list1);
                
                double preOrderPromoValue = 0.0d;

                //if (SessionPreOrderPromo!=null)
                //{
                //    preOrderPromoValue = SessionPreOrderPromo.PreOrderPromoValue;
                //}
                order.OrderTotal = order.OrderTotal + price;
                SessionUserOrder = order;
                SessionUserOrderTotal = order.OrderTotal;

                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", "<script type='text/javascript'>CloseOnReload()</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    private string GetThinCrustValue()
    {
        foreach (RadPageView pageView in RadMultiPage1.PageViews)
        {
            RadioButtonList rblThinCrust =
                ((RadioButtonList) ((MyUserControl) pageView.Controls[0]).FindControl("rblThinCrust"));
            if (rblThinCrust != null)
                return (rblThinCrust.SelectedValue.ConvertToBool() ? "Thin Crust" : "Regular");

        }
        return "";
    }

    protected void NextButton_Click(object sender, EventArgs e)
    {
        Int32 index = (Int32)ViewState["CurrentTab"];
        if (index <= RadTabStrip1.Tabs.Count)
        {
            if (index + 1 == RadTabStrip1.Tabs.Count - 1)
            {
                NextButton.Visible = false;
                AddToCartButton.Visible = true;
            }
            RadTabStrip1.Tabs[index + 1].Enabled = true;
            RadTabStrip1.Tabs[index + 1].Selected = true;
            RadMultiPage1.PageViews[index + 1].Enabled = true;
            RadMultiPage1.PageViews[index + 1].Selected = true;
            ViewState["CurrentTab"] = index + 1;
        }
        else
        {
            NextButton.Visible = false;
        }
    }

    protected void PreviousButton_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Tab Event
    
    protected void MultiProduct_OnUpdatePrice(object sender, EventArgs e)
    {
        Double price = 0;
        foreach (RadPageView pageView in RadMultiPage1.PageViews)
        {
            price += ((MyUserControl)pageView.Controls[0]).GetPrice();
        }
        txtNetPrice.Text = price.ToString();
    }

    #endregion

    #endregion

    #region Private Methods

    private ICollection<Products> GetProductsByProductId()
    {
        ICollection<Products> finalProductsList = new List<Products>();
        if (!string.IsNullOrEmpty(Request.QueryString["PID"]))
        {
            int pid = int.Parse(Request.QueryString["PID"]);
            Products product = _productManager.GetProductById(pid);
            txtNetPrice.Text = product.DefaultBranchProductPrice.ToString();
            ICollection<Products> productsList = _productManager.GetDealProductsByProductIdForOrder(pid);
            
            for (int i = 0; i < productsList.Count; i++)
            {
                if (productsList.ElementAt(i).Quantity > 1)
                {
                    for (int j = 0; j < productsList.ElementAt(i).Quantity; j++)
                    {
                        if (!String.IsNullOrEmpty(productsList.ElementAt(i).OptionTypeInProductXml))
                        {
                            // http://stackoverflow.com/questions/639471/use-xml-serialization-to-serialize-a-collection-without-the-parent-node
                            productsList.ElementAt(i).OptionTypesInProductList = Utility.XmlToObjectList<OptionTypesInProduct>(productsList.ElementAt(i).OptionTypeInProductXml, "//OptionTypesInProduct");
                        }
                        if (!String.IsNullOrEmpty(productsList.ElementAt(i).AdonTypeInProducctXml))
                        {
                            productsList.ElementAt(i).AdOnTypeInProductList = Utility.XmlToObjectList<AdOnTypeInProduct>(productsList.ElementAt(i).AdonTypeInProducctXml, "//AdonTypesInProduct");
                        }
                        finalProductsList.Add(productsList.ElementAt(i));
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(productsList.ElementAt(i).OptionTypeInProductXml))
                    {
                        // http://stackoverflow.com/questions/639471/use-xml-serialization-to-serialize-a-collection-without-the-parent-node
                        productsList.ElementAt(i).OptionTypesInProductList = Utility.XmlToObjectList<OptionTypesInProduct>(productsList.ElementAt(i).OptionTypeInProductXml, "//OptionTypesInProduct");
                    }
                    if (!String.IsNullOrEmpty(productsList.ElementAt(i).AdonTypeInProducctXml))
                    {
                        productsList.ElementAt(i).AdOnTypeInProductList = Utility.XmlToObjectList<AdOnTypeInProduct>(productsList.ElementAt(i).AdonTypeInProducctXml, "//AdonTypesInProduct");
                    }
                    finalProductsList.Add(productsList.ElementAt(i));
                }
            }
            
        }
        return finalProductsList;
    }

    private void AddTab(string tabName, bool enabled,Int32 counter)
    {
        RadTab tab = new RadTab(tabName);
        tab.Enabled = enabled;
        tab.SelectedImageUrl = "Image/" + counter + "_active.png";
        tab.ImageUrl = "Image/" + counter + "_normal.png";
        tab.TabIndex = (short)counter;
        RadTabStrip1.Tabs.Add(tab);
    }
    #endregion

}