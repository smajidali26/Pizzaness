using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessEntities;
using BusinessService;
using Core;

namespace POS
{
    /// <summary>
    /// Interaction logic for OrderAdd.xaml
    /// </summary>
    public partial class OrderAdd : Elysium.Controls.Window
    {
        #region Private
        public static ProductCategories _category = new ProductCategories();

        private ProductOptionManager _productOptionManager = new ProductOptionManager();

        private AdonTypeManager _adonTypeManager = new AdonTypeManager();

        private ProductManager _productManager = new ProductManager();

        private Double _defaultAdonPrice = 0;

        private Double _optionPrice = 0;

        private ProductOptions _selectedProductOption = null;
        #endregion

        #region Properties
        public ICollection<Adon> ProductAdonList { get; set; }

        public Orders UserOrder { get; set; }

        public ICollection<List<BusinessEntities.OrderDetailOptions>> OrderDetailOptionList
        {
            get;
            set;
        }

        public ICollection<List<BusinessEntities.OrderDetailAdOns>> OrderDetailAdonList
        {
            get;
            set;
        }

        #endregion

        #region Consturctor

        public OrderAdd()
        {
            InitializeComponent();
        }

        public OrderAdd(ProductCategories category, Orders order, ICollection<List<OrderDetailOptions>> _OrderDetailOptionList, ICollection<List<OrderDetailAdOns>> _OrderDetailAdonList)
        {
            InitializeComponent();
            _category = category;
            UserOrder = order;
            OrderDetailOptionList = _OrderDetailOptionList;
            OrderDetailAdonList = _OrderDetailAdonList;
            ProductManager _productManager = new ProductManager();
            lbProducts.ItemsSource = _productManager.GetProductByCategoryId(_category.CategoryID);
        }

        #endregion

        #region Events
        private void lbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Products product = (Products)e.AddedItems[0];
                if (product.IsSpecial)
                {
                    MultiProductPanel.Children.Clear();
                    SingleProductPanel.Visibility = System.Windows.Visibility.Collapsed;
                    #region Combo product
                    
                    ICollection<Products> productsList = _productManager.GetDealProductsByProductIdForOrder(product.ProductID);
                    ICollection<Products> finalProductsList = new List<Products>();
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
                    #endregion

                    MultiPorductControl control = new MultiPorductControl(finalProductsList);
                    MultiProductPanel.Visibility = System.Windows.Visibility.Visible;
                    control.OnUpdatePrice += control_OnUpdatePrice;
                    MultiProductPanel.Children.Add(control);
                }
                else
                {
                    lbOptions.ItemsSource = _productOptionManager.GetOptionTypeByProductId(product.ProductID);
                    lbToppings.ItemsSource = _adonTypeManager.GetAdonByProductId(product.ProductID);
                }
                Price.Content = product.DefaultBranchProductPrice.ToString("C", CultureInfo.CurrentCulture);
            }
            
        }

        private void control_OnUpdatePrice(object sender, EventArgs e)
        {
            MultiPorductControl control = (MultiPorductControl)MultiProductPanel.Children[0];
            Price.Content = control.GetPrice().ToString("C", CultureInfo.CurrentCulture);
        }
        
        private void Option_Click(object sender, RoutedEventArgs e)
        {
            var item = (ProductOptions)(sender as FrameworkElement).DataContext;
            var selectedOption = (RadioButton)((sender as FrameworkElement).Parent as FrameworkElement).FindName("SelectedOption");
            selectedOption.IsChecked = true;
            _selectedProductOption = item;
            _optionPrice = item.Price;
            _defaultAdonPrice = item.ToppingPrice;
            GetPrice();
        }

        private void ToppingSize_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel parentPanel = (StackPanel)button.Parent;
            Image none =(Image)parentPanel.FindName("None");
            Image first = (Image)parentPanel.FindName("First");
            Image full = (Image)parentPanel.FindName("Full");
            Image second = (Image)parentPanel.FindName("Second");
            TextBlock SelectedTopping = (TextBlock)parentPanel.FindName("SelectedTopping");
            SelectedTopping.Text = (String)button.CommandParameter;
            switch ((String)button.CommandParameter)
            {
                case "0":
                    none.Source = new BitmapImage(new Uri(@"/Images/None-Selected.png", UriKind.Relative));
                    first.Source = new BitmapImage(new Uri(@"/Images/FirstHalf_NotSelected.png", UriKind.Relative));
                    full.Source = new BitmapImage(new Uri(@"/Images/Full_NotSelected.png", UriKind.Relative));
                    second.Source = new BitmapImage(new Uri(@"/Images/2ndHalf_NotSelected.png", UriKind.Relative));
                    break;
                case "1":
                    none.Source = new BitmapImage(new Uri(@"/Images/None-NotSelected.png", UriKind.Relative));
                    first.Source = new BitmapImage(new Uri(@"/Images/FirstHalf_NotSelected.png", UriKind.Relative));
                    full.Source = new BitmapImage(new Uri(@"/Images/Full_Selected.png", UriKind.Relative));
                    second.Source = new BitmapImage(new Uri(@"/Images/2ndHalf_NotSelected.png", UriKind.Relative));
                    break;
                case "2":
                     none.Source = new BitmapImage(new Uri(@"/Images/None-NotSelected.png", UriKind.Relative));
                    first.Source = new BitmapImage(new Uri(@"/Images/FirstHalf_Selected.png", UriKind.Relative));
                    full.Source = new BitmapImage(new Uri(@"/Images/Full_NotSelected.png", UriKind.Relative));
                    second.Source = new BitmapImage(new Uri(@"/Images/2ndHalf_NotSelected.png", UriKind.Relative));
                    break;
                case "3":
                    none.Source = new BitmapImage(new Uri(@"/Images/None-NotSelected.png", UriKind.Relative));
                    first.Source = new BitmapImage(new Uri(@"/Images/FirstHalf_NotSelected.png", UriKind.Relative));
                    full.Source = new BitmapImage(new Uri(@"/Images/Full_NotSelected.png", UriKind.Relative));
                    second.Source = new BitmapImage(new Uri(@"/Images/2ndHalf_Selected.png", UriKind.Relative));
                    break;
            }
            var item = (Adon)(sender as FrameworkElement).DataContext;
            if (ProductAdonList == null)
                ProductAdonList = new List<Adon>();
            if (((String)button.CommandParameter).Equals("0"))
            {
                ProductAdonList.Remove(item);
            }
            else
            {
                if (ProductAdonList.Contains(item))
                {
                    ProductAdonList.Remove(item);
                    ProductAdonList.Add(item);
                }
                else
                    ProductAdonList.Add(item);
            }
            SelectedAdons.Text = String.Empty;
            foreach (Adon adon in ProductAdonList)
            {
                SelectedAdons.Text += adon.AdOnName + ", ";
            }
            GetPrice();
        }

        private void OrderNowButton_Click(object sender, RoutedEventArgs e)
        {
            Double productPrice = 0;
            Products product = (Products)lbProducts.SelectedItem;
            List<OrderDetailAdOns> orderDetailAdonList = new List<OrderDetailAdOns>();
            List<OrderDetailOptions> orderDetailOptionsList = new List<OrderDetailOptions>();
            if (UserOrder == null)
                UserOrder = new Orders();
            if (_optionPrice == 0)
                productPrice = product.DefaultBranchProductPrice;
            else
                productPrice = _optionPrice;
            
            if (UserOrder.OrderDetailsList == null)
            {
                UserOrder.OrderDetailsList = new List<BusinessEntities.OrderDetails>();
                //SessionOrderAdonList = new List<List<BusinessEntities.OrderDetailAdOns>>();
                //SessionOrderDetailOptionList = new List<List<BusinessEntities.OrderDetailOptions>>();
                UserOrder.OrderStatusID = BusinessEntities.OrderStatus.NewOrder;
            }
            BusinessEntities.OrderDetails orderdetail = new BusinessEntities.OrderDetails();
            if (!product.IsSpecial)
            {
                if (_optionPrice > 0)
                    orderdetail.Price = _optionPrice;
                else
                    orderdetail.Price = Convert.ToDouble(product.DefaultBranchProductPrice);
            }
            orderdetail.CategoryName = _category.Name;
            orderdetail.ProductName = product.Name;
            orderdetail.ProductID = product.ProductID;
            orderdetail.ProductImage = product.Image;
            orderdetail.Quantity = 1;
            if (product.IsSpecial)
            {
                MultiPorductControl control = (MultiPorductControl)MultiProductPanel.Children[0];
                orderdetail.Price = control.GetPrice();
                orderdetail.OrderDetailSubProducts = control.GetOrderDetailSubProducts();
                orderdetail.IsGroupProduct = true;
            }
            else
            {
                #region Get Selected Option
                
                
                for (int i = 0; i < lbOptions.Items.Count; i++)
                {
                    DependencyObject obj = lbOptions.ItemContainerGenerator.ContainerFromIndex(i);
                    ContentPresenter optionTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                    OptionType optionType = (OptionType)optionTypeContentPresenter.Content;
                    ListBox OptionList = FindVisualChild<ListBox>(obj);
                    if (optionType.IsSamePrice && !optionType.IsMultiSelect)
                    {
                        for (int j = 0; j < OptionList.Items.Count; j++)
                        {
                            DependencyObject obj1 = OptionList.ItemContainerGenerator.ContainerFromIndex(j);
                            // Getting the ContentPresenter of myListBoxItem
                            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(obj1);
                            // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                            RadioButton selectedOption = (RadioButton)myDataTemplate.FindName("SelectedOption", myContentPresenter);
                            if (selectedOption.IsChecked.Value)
                            {
                                ProductOptions productOptions = (ProductOptions)myContentPresenter.Content;
                                OrderDetailOptions orderdetailoption = new OrderDetailOptions();
                                orderdetailoption.ProductOptionId = productOptions.OptionID;
                                orderdetailoption.ProductOptionName = productOptions.OptionName;
                                orderdetailoption.Price = Convert.ToDouble(productOptions.Price);
                                orderDetailOptionsList.Add(orderdetailoption);
                                orderdetail.Price = productOptions.Price;
                            }
                        }
                    }
                    else if (!optionType.IsSamePrice && !optionType.IsMultiSelect)
                    {
                        for (int j = 0; j < OptionList.Items.Count; j++)
                        {
                            DependencyObject obj1 = OptionList.ItemContainerGenerator.ContainerFromIndex(j);
                            // Getting the ContentPresenter of myListBoxItem
                            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(obj1);
                            // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                            RadioButton selectedOption = (RadioButton)myDataTemplate.FindName("SelectedOption", myContentPresenter);
                            if (selectedOption.IsChecked.Value)
                            {
                                ProductOptions productOptions = (ProductOptions)myContentPresenter.Content;
                                OrderDetailOptions orderdetailoption = new OrderDetailOptions();
                                orderdetailoption.ProductOptionId = productOptions.OptionID;
                                orderdetailoption.ProductOptionName = productOptions.OptionName;
                                orderdetailoption.Price = productOptions.Price;
                                orderDetailOptionsList.Add(orderdetailoption);
                                orderdetail.Price = productOptions.Price;
                            }
                        }
                    }
                }

                #endregion


                #region Get Selected Toppings


                for (int i = 0; i < lbToppings.Items.Count; i++)
                {
                    DependencyObject obj = lbToppings.ItemContainerGenerator.ContainerFromIndex(i);
                    ContentPresenter adonTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                    AdonType adonType = (AdonType)adonTypeContentPresenter.Content;
                    ListBox AdonList = FindVisualChild<ListBox>(obj);
                    for (int j = 0; j < AdonList.Items.Count; j++)
                    {
                        DependencyObject obj1 = AdonList.ItemContainerGenerator.ContainerFromIndex(j);
                        // Getting the ContentPresenter of myListBoxItem
                        ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(obj1);
                        // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                        DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                        Int16 SelectedTopping = Convert.ToInt16(((TextBlock)myDataTemplate.FindName("SelectedTopping", myContentPresenter)).Text);
                        if (SelectedTopping != 0)
                        {
                            if (!adonType.IsFreeAdonType)
                            {

                                Adon adon = (Adon)myContentPresenter.Content;

                                OrderDetailAdOns orderdetailadon = new OrderDetailAdOns();
                                orderdetailadon.AdOnId = adon.AdOnID;
                                orderdetailadon.AdonName = adon.AdOnName;
                                orderdetailadon.AdonTypeName = adon.AdOnTypeName;
                                orderdetailadon.SelectedAdonOption = (SelectedOption)SelectedTopping;

                                orderDetailAdonList.Add(orderdetailadon);
                                if (adon.DefaultSelected != SelectedTopping)
                                {
                                    if (_defaultAdonPrice > 0)
                                        productPrice += _defaultAdonPrice;
                                    else
                                        productPrice += (Double)adonType.Price;
                                }
                            }
                        }
                    }
                }

                #endregion
            }
            double discountPrice = 0;
            Double.TryParse(txtDiscount.Text, out discountPrice);
            if (discountPrice > 0)
                orderdetail.Price = discountPrice;
            else if(orderdetail.Price == 0)
                orderdetail.Price = productPrice;

            UserOrder.OrderDetailsList.Add(orderdetail);
            UserOrder.OrderTotal += orderdetail.Price;
            OrderDetailOptionList.Add(orderDetailOptionsList);
            OrderDetailAdonList.Add(orderDetailAdonList);

            lbOptions.ItemsSource = null;
            lbToppings.ItemsSource = null;
            _defaultAdonPrice = 0;
            _optionPrice = 0;
            SelectedAdons.Text = string.Empty;
            txtDiscount.Text = String.Empty;
            Price.Content = String.Empty;
            lbProducts.UnselectAll();
            this.Close();
        }

        #endregion

        #region Methods
        public static IEnumerable<Products> GetProductByCateogory()
        {
            ProductManager _productManager = new ProductManager();
            return _productManager.GetProductByCategoryId(_category.CategoryID);
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
             where childItem : DependencyObject
        {
            // Search immediate children
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child is childItem)
                    return (childItem)child;

                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);

                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }

        private void GetPrice()
        {
            Double productPrice = 0;

            Products product =  (Products)lbProducts.SelectedItem;
            if (_optionPrice == 0)
                productPrice = product.DefaultBranchProductPrice;
            else
                productPrice = _optionPrice;
            for (int i = 0; i < lbOptions.Items.Count; i++)
            {
                DependencyObject obj = lbOptions.ItemContainerGenerator.ContainerFromIndex(i);
                ContentPresenter adonTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                OptionType optionType = (OptionType)adonTypeContentPresenter.Content;
                ListBox Options = FindVisualChild<ListBox>(obj);

                if (!optionType.IsMultiSelect && optionType.IsSamePrice)
                {
                }
            }

            for (int i = 0; i < lbToppings.Items.Count; i++)
            {
                DependencyObject obj = lbToppings.ItemContainerGenerator.ContainerFromIndex(i);
                ContentPresenter adonTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                AdonType adonType = (AdonType)adonTypeContentPresenter.Content;
                ListBox AdonList = FindVisualChild<ListBox>(obj);
                
                for (int j = 0; j < AdonList.Items.Count; j++)
                {
                    DependencyObject obj1 = AdonList.ItemContainerGenerator.ContainerFromIndex(j);
                    // Getting the ContentPresenter of myListBoxItem
                    ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(obj1);
                    // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                    Int16 SelectedTopping = Convert.ToInt16(((TextBlock)myDataTemplate.FindName("SelectedTopping", myContentPresenter)).Text);
                    if (SelectedTopping != 0)
                    {
                        if (!adonType.IsFreeAdonType)
                        {
                            Adon adon = (Adon)myContentPresenter.Content;
                            if (adon.DefaultSelected != SelectedTopping)
                            {
                                if (_defaultAdonPrice > 0)
                                    productPrice += _defaultAdonPrice;
                                else
                                    productPrice += (Double)adonType.Price;
                            }
                        }
                    }
                }
            }
            Price.Content = productPrice.ToString("C", CultureInfo.CurrentCulture);
        }
        #endregion

        
    }
}
