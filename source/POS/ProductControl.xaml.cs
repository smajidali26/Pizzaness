using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessEntities;

namespace POS
{
    /// <summary>
    /// Interaction logic for ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
        #region Delegate
        // Delegate declaration
        public delegate void UpdatePrice(object sender, EventArgs e);

        // Event declaration
        public event UpdatePrice OnUpdatePrice;

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

        #region Private 
        private Products _product;

        private Double _defaultAdonPrice = 0;

        private Double _optionPrice = 0;

        private ProductOptions _selectedProductOption = null;

        #endregion

        public ProductControl(Products product)
        {
            InitializeComponent();
            _product = product;
            lbOptions.ItemsSource = product.OptionTypesInProductList;
            lbToppings.ItemsSource = product.AdOnTypeInProductList;
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            var item = (ProductOptions)(sender as FrameworkElement).DataContext;
            var selectedOption = (RadioButton)((sender as FrameworkElement).Parent as FrameworkElement).FindName("SelectedOption");
            selectedOption.IsChecked = true;
            _selectedProductOption = item;
            _optionPrice = item.Price;
            _defaultAdonPrice = item.ToppingPrice;
            if (OnUpdatePrice != null)
            {
                OnUpdatePrice(sender, e);
            }
        }

        private void ToppingSize_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel parentPanel = (StackPanel)button.Parent;
            Image none = (Image)parentPanel.FindName("None");
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
            //SelectedAdons.Text = String.Empty;
            //foreach (Adon adon in ProductAdonList)
            //{
            //    SelectedAdons.Text += adon.AdOnName + ", ";
            //}
            if (OnUpdatePrice != null)
            {
                OnUpdatePrice(sender, e);
            }
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

        public Double GetPrice()
        {
            Double productPrice = 0;

            
            if (_optionPrice == 0)
                productPrice = _product.UnitPrice;
            else
                productPrice = _optionPrice;
            for (int i = 0; i < lbOptions.Items.Count; i++)
            {
                DependencyObject obj = lbOptions.ItemContainerGenerator.ContainerFromIndex(i);
                if (obj != null)
                {
                    ContentPresenter adonTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                    OptionTypesInProduct optionType = (OptionTypesInProduct)adonTypeContentPresenter.Content;
                    ListBox Options = FindVisualChild<ListBox>(obj);

                    if (!optionType.IsMultiSelect && optionType.IsSamePrice)
                    {
                    }
                }
            }
            Int16 freeToppingCount = 0;
            for (int i = 0; i < lbToppings.Items.Count; i++)
            {
                DependencyObject obj = lbToppings.ItemContainerGenerator.ContainerFromIndex(i);
                if (obj != null)
                {
                    ContentPresenter adonTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                    AdOnTypeInProduct adonType = (AdOnTypeInProduct)adonTypeContentPresenter.Content;
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
                                if (freeToppingCount == _product.NumberOfFreeTopping)
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
                                else
                                {
                                    freeToppingCount++;
                                }
                            }
                        }
                    }
                }
            }
            return productPrice;
        }

        public OrderDetailSubProduct GetOrderDetailSubProduct()
        {
            Double productPrice = 0, toppingPrice = 0;
            bool CalculateAdonPrice = false;
            List<OrderDetailSubProductAdon> orderDetailAdonList = new List<OrderDetailSubProductAdon>();
            List<OrderDetailSubProductOption> orderDetailOptionsList = new List<OrderDetailSubProductOption>();

            #region Order Detail Sub Product

            OrderDetailSubProduct orderDetailSubProduct = new OrderDetailSubProduct();
            orderDetailSubProduct.ProductId = _product.ProductID;
            orderDetailSubProduct.Quantity = 1;
            orderDetailSubProduct.ProductName = _product.Name;
            orderDetailSubProduct.RecipientName = String.Empty;
            orderDetailSubProduct.Comments = String.Empty;
            orderDetailSubProduct.OrderDetailSubProductAdons = new List<OrderDetailSubProductAdon>();
            orderDetailSubProduct.OrderDetailSubProductOptions = new List<OrderDetailSubProductOption>();

            #endregion
                        
            #region Options
            
            
            for (int i = 0; i < lbOptions.Items.Count; i++)
            {
                DependencyObject obj = lbOptions.ItemContainerGenerator.ContainerFromIndex(i);
                if (obj != null)
                {
                    ContentPresenter optionTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                    OptionTypesInProduct optionType = (OptionTypesInProduct)optionTypeContentPresenter.Content;
                    ListBox OptionList = FindVisualChild<ListBox>(obj);
                    if (optionType.IsSamePrice && !optionType.IsMultiSelect)
                    {
                        CalculateAdonPrice = true;
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
                                OrderDetailSubProductOption orderdetailoption = new OrderDetailSubProductOption();
                                orderdetailoption.ProductOptionId = productOptions.OptionID;
                                orderdetailoption.ProductOptionName = productOptions.OptionName;
                                orderdetailoption.Price = Convert.ToDouble(productOptions.Price);

                                orderDetailSubProduct.OrderDetailSubProductOptions.Add(orderdetailoption);

                            }
                        }
                    }
                    else if (!optionType.IsSamePrice && !optionType.IsMultiSelect)
                    {
                        CalculateAdonPrice = true;
                        if (OptionList.Items.Count == 1)
                        {
                            DependencyObject obj1 = OptionList.ItemContainerGenerator.ContainerFromIndex(0);
                            // Getting the ContentPresenter of myListBoxItem
                            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(obj1);
                            // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                            
                            ProductOptions productOptions = (ProductOptions)myContentPresenter.Content;
                            OrderDetailSubProductOption orderdetailoption = new OrderDetailSubProductOption();
                            orderdetailoption.ProductOptionId = productOptions.OptionID;
                            orderdetailoption.ProductOptionName = productOptions.OptionName;
                            orderdetailoption.Price = productOptions.Price;
                            orderDetailSubProduct.OrderDetailSubProductOptions.Add(orderdetailoption);
                            if (_defaultAdonPrice == 0)
                                _defaultAdonPrice = productOptions.ToppingPrice;
                        }
                        else
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
                                    OrderDetailSubProductOption orderdetailoption = new OrderDetailSubProductOption();
                                    orderdetailoption.ProductOptionId = productOptions.OptionID;
                                    orderdetailoption.ProductOptionName = productOptions.OptionName;
                                    orderdetailoption.Price = productOptions.Price;
                                    orderDetailSubProduct.OrderDetailSubProductOptions.Add(orderdetailoption);
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region Toppings
            if (CalculateAdonPrice)
            {
                Int16 freeToppingCount = 0;

                for (int i = 0; i < lbToppings.Items.Count; i++)
                {
                    DependencyObject obj = lbToppings.ItemContainerGenerator.ContainerFromIndex(i);
                    if (obj != null)
                    {
                        ContentPresenter adonTypeContentPresenter = FindVisualChild<ContentPresenter>(obj);
                        AdOnTypeInProduct adonType = (AdOnTypeInProduct)adonTypeContentPresenter.Content;
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

                                    OrderDetailSubProductAdon orderdetailadon = new OrderDetailSubProductAdon();
                                    orderdetailadon.AdOnId = adon.AdOnID;
                                    orderdetailadon.AdonName = adon.AdOnName;
                                    orderdetailadon.AdonTypeName = adon.AdOnTypeName;
                                    orderdetailadon.SelectedAdonOption = SelectedTopping;

                                    orderDetailSubProduct.OrderDetailSubProductAdons.Add(orderdetailadon);
                                    if (freeToppingCount == _product.NumberOfFreeTopping)
                                    {
                                        if (adon.DefaultSelected != SelectedTopping)
                                        {
                                            if (_defaultAdonPrice > 0)
                                                productPrice += _defaultAdonPrice;
                                            else
                                                productPrice += (Double)adonType.Price;
                                        }
                                    }
                                    else
                                    {
                                        freeToppingCount++;
                                    }

                                }
                                else
                                {
                                    Adon adon = (Adon)myContentPresenter.Content;

                                    OrderDetailSubProductAdon orderdetailadon = new OrderDetailSubProductAdon();
                                    orderdetailadon.AdOnId = adon.AdOnID;
                                    orderdetailadon.AdonName = adon.AdOnName;
                                    orderdetailadon.AdonTypeName = adon.AdOnTypeName;
                                    orderdetailadon.SelectedAdonOption = SelectedTopping;

                                    orderDetailSubProduct.OrderDetailSubProductAdons.Add(orderdetailadon);
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            orderDetailSubProduct.Price = productPrice;
            return orderDetailSubProduct;
        }
    }
}
