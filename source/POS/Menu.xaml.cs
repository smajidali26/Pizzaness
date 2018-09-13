using System;
using System.Collections.Generic;
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
using BusinessService;
using BusinessEntities;
using System.ComponentModel;
using Microsoft.Windows.Controls.Primitives;
using System.Data;

namespace POS
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Elysium.Controls.Window, INotifyPropertyChanged
    {
        #region Private

        public event PropertyChangedEventHandler PropertyChanged;

        private ProductCategoryManager _categoryManager = new ProductCategoryManager();

        private ProductManager _productManager = new ProductManager();

        private OrderManager orderManager = new OrderManager();

        private BranchManager branchManager = new BranchManager();

        private bool _showWIndow = false;

        private Orders _order = new Orders();

        public ContactInfo ContactInfoObject = null;

        private Double deduction = 0;

        private ICollection<List<OrderDetailOptions>> _OrderDetailOptionList = new List<List<OrderDetailOptions>>();
        
        private ICollection<List<OrderDetailAdOns>> _OrderDetailAdonList = new List<List<OrderDetailAdOns>>();
        
        #endregion

        #region Properties

        public ICollection<ProductCategories> Categories { get; set; }

        public Orders UserOrder
        {
            get { return _order; }
            set {
                this._order = value;
                NotifyPropertyChanged("UserOrder");
            }
        }

        #endregion

        public Menu(OrderType orderType,ContactInfo _contactInfo)
        {
            InitializeComponent();
            lbCategories.MaxWidth = MainGrid.MaxWidth = System.Windows.SystemParameters.WorkArea.Width;
            MainGrid.MaxHeight = System.Windows.SystemParameters.WorkArea.Height;
            _order.OrderTypeID = orderType;
            UserOrder.ContactInfoId = _contactInfo.ContactInfoId;
            ContactInfoObject = _contactInfo;
        }

        #region Events
        
        private void lbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_showWIndow)
            {
                OrderAdd orderAdd = new OrderAdd((ProductCategories)e.AddedItems[0], _order, _OrderDetailOptionList, _OrderDetailAdonList);
                orderAdd.ShowDialog();
                
                _showWIndow = false;
                if (UserOrder != null && UserOrder.OrderDetailsList != null)
                {
                    OrderNow.Visibility = System.Windows.Visibility.Visible;
                    OrderDetail.ItemsSource = null;
                    //OrderDetail.DataContext = UserOrder.OrderDetailsList;
                    OrderDetail.ItemsSource = UserOrder.OrderDetailsList;
                    OrderDetail.Items.Refresh();
                    OrderTotal.Content = "$" + UserOrder.OrderTotal;
                }
                else
                {
                    OrderNow.Visibility = System.Windows.Visibility.Hidden;
                    OrderDetail.DataContext = null;
                    OrderDetail.UpdateLayout();
                }
            }
        }
        
        private void Order_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            //walk up the tree to find the ListboxItem
            DependencyObject tvi = findParentTreeItem(button, typeof(ListBoxItem));
            //if not null cast the Dependancy object to type of Listbox item.
            if (tvi != null)
            {
                _showWIndow = true;
                ListBoxItem lbi = tvi as ListBoxItem;

                OrderAdd orderAdd = new OrderAdd((ProductCategories)((System.Windows.Controls.ContentControl)(lbi)).Content, _order, _OrderDetailOptionList, _OrderDetailAdonList);
                
                orderAdd.ShowDialog();
                orderAdd.Dispose();
                _showWIndow = false;
                if (UserOrder != null && UserOrder.OrderDetailsList != null)
                {
                    OrderNow.Visibility = System.Windows.Visibility.Visible;
                    OrderDetail.ItemsSource = null;
                    //OrderDetail.DataContext = UserOrder.OrderDetailsList;
                    OrderDetail.ItemsSource = UserOrder.OrderDetailsList;
                    OrderDetail.Items.Refresh();
                    OrderTotal.Content = "$" + UserOrder.OrderTotal;
                }
                else
                {
                    OrderNow.Visibility = System.Windows.Visibility.Hidden;
                    OrderDetail.DataContext = null;
                    OrderDetail.UpdateLayout();
                }
                /* //select it.
                 lbi.IsSelected = true; */
            }
        }

        private void Button_TouchEnter(object sender, TouchEventArgs e)
        {
            Button button = sender as Button;
            //walk up the tree to find the ListboxItem
            DependencyObject tvi = findParentTreeItem(button, typeof(ListBoxItem));
            //if not null cast the Dependancy object to type of Listbox item.
            if (tvi != null)
            {
                ListBoxItem lbi = tvi as ListBoxItem;
                OrderAdd orderAdd = new OrderAdd((ProductCategories)((System.Windows.Controls.ContentControl)(lbi)).Content, _order, _OrderDetailOptionList, _OrderDetailAdonList);

                orderAdd.ShowDialog();
                orderAdd.Dispose();
                _showWIndow = false;
                if (UserOrder != null && UserOrder.OrderDetailsList != null)
                {
                    OrderNow.Visibility = System.Windows.Visibility.Visible;
                    OrderDetail.ItemsSource = null;
                    //OrderDetail.DataContext = UserOrder.OrderDetailsList;
                    OrderDetail.ItemsSource = UserOrder.OrderDetailsList;
                    OrderDetail.Items.Refresh();
                    OrderTotal.Content = "$" + UserOrder.OrderTotal;
                }
                else
                {
                    OrderNow.Visibility = System.Windows.Visibility.Hidden;
                    OrderDetail.DataContext = null;
                    OrderDetail.UpdateLayout();
                }
            }
        }

        private void OrderNow_Click(object sender, RoutedEventArgs e)
        {
            Branches branch = branchManager.GetBranchById(1);

            UserOrder.BranchID = 1;
            UserOrder.ContactInfoId = ContactInfoObject.ContactInfoId;
            UserOrder.CustomerName = ContactInfoObject.FirstName + " " + ContactInfoObject.LastName;
            UserOrder.OrderStatusID = OrderStatus.NewOrder;
            UserOrder.TaxPercentage = (double)branch.TaxPercentage;

            if (UserOrder.OrderTypeID == OrderType.Deliver)
            {
                UserOrder.DeliveryCharges = (double)branch.DeliveryCharges;
                UserOrder.OrderTypeID = OrderType.Deliver;
            }
            else
            {
                UserOrder.OrderTypeID = OrderType.SelfPickup;
                UserOrder.DeliveryCharges = 0;
            }

            Double Tax = ((UserOrder.OrderTotal - deduction + UserOrder.DeliveryCharges) * Convert.ToDouble(branch.TaxPercentage)) / 100;
            UserOrder.OrderTotal = UserOrder.OrderTotal - deduction + UserOrder.DeliveryCharges + Tax;
            

            
            Int64 result = orderManager.AddOrder(UserOrder, _OrderDetailOptionList, _OrderDetailAdonList);

            if (result > 0)
            {
                MessageBox.Show("Order has been placed successfully.");
                string buffer = "\x1b\x1d\x61\x1";             //Center Alignment - Refer to Pg. 3-29
                //buffer = buffer + "\x5B" + "If loaded.. Logo1 goes here" + "\x5D\n";
                //buffer = buffer + "\x1B\x1C\x70\x1\x0";          //Stored Logo Printing - Refer to Pg. 3-38
                buffer = buffer + "\x1b\x69\x1 \x1b\x45 PIZZANESS \x1b\x46 \x1b\x69\x0 \n";
                buffer = buffer + "10829 Lanham Sevem Rd.\n";
                buffer = buffer + "Glenn Dale, MD 20796\n";
                buffer = buffer + "301-464-2600\n\n";
                buffer = buffer + "\x1b\x1d\x61\x0";             //Left Alignment - Refer to Pg. 3-29
                buffer = buffer + "\x1b\x44\x2\x10\x22\x0";      //Setting Horizontal Tab - Pg. 3-27
                buffer = buffer + "Date: " + DateTime.Now.ToString("MMM, dd yyyy") + "\x9 Time:" + DateTime.Now.ToString("HH:mm") + "\n";      //Moving Horizontal Tab - Pg. 3-26
                buffer = buffer + "------------------------------------------------ \n";
                buffer = buffer + ContactInfoObject.Title + " " + ContactInfoObject.FirstName + " " + ContactInfoObject.LastName + "\n";              
                buffer = buffer + ContactInfoObject.ContactAddressList.ElementAt(0).Address + "\n";
                buffer = buffer + ContactInfoObject.ContactAddressList.ElementAt(0).City + " " + ContactInfoObject.ContactAddressList.ElementAt(0).State + " " + ContactInfoObject.ContactAddressList.ElementAt(0).Zip + "\n";
                if (ContactInfoObject.Telephone.Length == 10)
                    buffer = buffer + ContactInfoObject.Telephone.Substring(0, 3) + "-" + ContactInfoObject.Telephone.Substring(3, 3) + "-" + ContactInfoObject.Telephone.Substring(6, 4) + "\n";
                else
                    buffer = buffer + ContactInfoObject.Telephone + "\n";
                if (!String.IsNullOrEmpty(ContactInfoObject.Mobile))
                {
                    if (ContactInfoObject.Mobile.Length == 10)
                        buffer = buffer + ContactInfoObject.Mobile.Substring(0, 3) + "-" + ContactInfoObject.Mobile.Substring(3, 3) + "-" + ContactInfoObject.Mobile.Substring(6, 4) + "\n";
                    else
                        buffer = buffer + ContactInfoObject.Mobile + "\n";
                }
                buffer = buffer + "------------------------------------------------ \n";
                buffer = buffer + "\x1b\x45";                    //Select Emphasized Printing - Pg. 3-14
                buffer = buffer + "ORDER DETAILS\n";
                buffer = buffer + "\x1b\x46";                    //Cancel Emphasized Printing - Pg. 3-14
                Int16 counter = 1;
                Double SubTotal = 0;
                buffer = buffer + "No." + "\x9 Item Name\x9\x9\x9 Price  \n";
                foreach (OrderDetails orderDetail in UserOrder.OrderDetailsList)
                {
                    buffer = buffer + counter + "\x9" + orderDetail.ProductName + "\x9\x9\x9$" + Math.Round(orderDetail.Price, 2) + "  \n";
                    if (orderDetail.IsGroupProduct)
                    {
                        foreach(OrderDetailSubProduct product in orderDetail.OrderDetailSubProducts)
                        {
                            buffer = buffer + "\x9 • " + product.ProductName + "\n";
                            if (product.OrderDetailSubProductOptions.Count > 0)
                            {
                                buffer = buffer + "\x9   Options:\n";
                                foreach (OrderDetailSubProductOption suboption in product.OrderDetailSubProductOptions)
                                {
                                    buffer = buffer + "\x9    " + suboption.ProductOptionName + "\n";
                                }
                            }
                            if (product.OrderDetailSubProductAdons.Count > 0)
                            {
                                buffer = buffer + "\x9   Toppings:\n";
                                foreach (OrderDetailSubProductAdon subadon in product.OrderDetailSubProductAdons)
                                {
                                    if ((SelectedOption)subadon.SelectedAdonOption != SelectedOption.None)
                                    buffer = buffer + "\x9    " + subadon.AdonName + "\n";
                                }
                            }
                        }
                    }
                    else
                    {

                        List<OrderDetailOptions> detailOptionList = _OrderDetailOptionList.ElementAt(counter - 1);

                        if (detailOptionList.Count > 0)
                        {
                            buffer = buffer + "\x9Options:\n";
                            foreach (OrderDetailOptions detailOption in detailOptionList)
                            {
                                buffer = buffer + "\x9" + detailOption.ProductOptionName + "\n";
                            }
                        }

                        List<OrderDetailAdOns> detailAdonList = _OrderDetailAdonList.ElementAt(counter - 1);
                        if (detailAdonList.Count > 0)
                        {
                            buffer = buffer + "\x9Toppings:\n";
                            foreach (OrderDetailAdOns detailAdon in detailAdonList)
                            {
                                if (detailAdon.SelectedAdonOption != SelectedOption.None)
                                    buffer = buffer + "\x9  " + detailAdon.AdonName + "(" + detailAdon.SelectedAdonOption.ToString() + ")\n";
                            }
                        }
                    }
                    counter++;
                    SubTotal += orderDetail.Price;
                }
                buffer = buffer + "Subtotal " + "\x9\x9\x9 $" + Math.Round(SubTotal, 2) + " \n";
                if (UserOrder.DeliveryCharges > 0)
                    buffer = buffer + "Delivery Charges " + "\x9" + "" + "\x9" + "" + "         $" + UserOrder.DeliveryCharges + ".00\n";
                buffer = buffer + "Tax " + "\x9" + "" + "\x9" + "" + "         $" + Math.Round(Tax, 2) + " \n";
                if (deduction > 0)
                    buffer = buffer + "Discount " + "\x9\x9\x9$" + Math.Round(deduction, 2) + " \n";
                buffer = buffer + "------------------------------------------------ \n";
                buffer = buffer + "Total" + "\x6" + "" + "\x9" + "\x1b\x69\x1\x1" + "         $" + Math.Round(UserOrder.OrderTotal, 2) + " \n";    //Character Expansion - Pg. 3-10
                buffer = buffer + "\x1b\x69\x0\x0";                                                          //Cancel Expansion - Pg. 3-10
                buffer = buffer + "------------------------------------------------ \n";
                buffer = buffer + "Thank you for ordering.";
                /*buffer = buffer + "Visa XXXX-XXXX-XXXX-0123\n\n";
                buffer = buffer + "\x1b\x34" + "Refunds and Exchanges" + "\x1b\x35\n";                       //Specify/Cencel White/Black Invert - Pg. 3-16
                buffer = buffer + "Within " + "\x1b\x2d\x1" + "30 days" + "\x1b\x2d\x0" + " with receipt\n"; //Specify/Cancel Underline Printing - Pg. 3-15
                buffer = buffer + "And tags attached\n\n";*/
                buffer = buffer + "\x1b\x1d\x61\x1";             //Center Alignment - Refer to Pg. 3-29
                // buffer = buffer + "\x1b\x62\x6\x2\x2" + " 12ab34cd56" + "\x1e\n";             //Barcode - Pg. 3-39 - 3-40
                buffer = buffer + "\x1b\x64\x02";                                            //Cut  - Pg. 3-41
                buffer = buffer + "\x7";                                                    //Open Cash Drawer


                // Allow the user to select a printer.
                PrintDialog pd = new PrintDialog();
                //pd.PrinterSettings = new PrinterSettings();
                //if (DialogResult.Value == pd.ShowDialog().Value)
                //{
                // Send a printer-specific to the printer.
                RawPrinterHelper.SendStringToPrinter(System.Configuration.ConfigurationSettings.AppSettings["PrinterName"], buffer);
                //}
                UserOrder = null;
                _OrderDetailOptionList.Clear();
                _OrderDetailAdonList.Clear();
                this.Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDetail.SelectedIndex > -1)
            {
                OrderDetails orderDetail = UserOrder.OrderDetailsList.ElementAt(OrderDetail.SelectedIndex);
                UserOrder.OrderTotal = UserOrder.OrderTotal - orderDetail.Price;
                List<OrderDetailOptions> orderDetailOptions = _OrderDetailOptionList.ElementAt(OrderDetail.SelectedIndex);
                List<OrderDetailAdOns> orderDetailAdons = _OrderDetailAdonList.ElementAt(OrderDetail.SelectedIndex);
                _OrderDetailOptionList.Remove(orderDetailOptions);
                _OrderDetailAdonList.Remove(orderDetailAdons);
                UserOrder.OrderDetailsList.Remove(orderDetail);
                OrderDetail.ItemsSource = null;
                //OrderDetail.DataContext = UserOrder.OrderDetailsList;
                OrderDetail.ItemsSource = UserOrder.OrderDetailsList;
                OrderDetail.Items.Refresh();
                OrderTotal.Content = "$" + UserOrder.OrderTotal;
            }
        }
        #endregion

        private DependencyObject findParentTreeItem(DependencyObject CurrentControl, Type ParentType)
        {
            bool notfound = true;
            while (notfound)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(CurrentControl);
                string ParentTypeName = ParentType.Name;
                //Compare current type name with what we want
                if (parent == null)
                {
                    System.Diagnostics.Debugger.Break();
                    notfound = false;
                    continue;
                }
                if (parent.GetType().Name == ParentTypeName)
                {
                    return parent;
                }
                //we haven't found it so walk up the tree.
                CurrentControl = parent;
            }
            return null;
        }

        #region Methods

        public static IEnumerable<ProductCategories> GetProductCategories()
        {
            ProductCategoryManager _categoryManager = new ProductCategoryManager();
            return _categoryManager.GetAllProductCategories(1,false);
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                  new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private void OrderDetail_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            OrderDetails orderDetail = (OrderDetails)e.Row.DataContext;
            DataRowView item = e.Row.Item as DataRowView;
            if (item != null)
            {
                object obj = item[0];
            }
        }

        private void OrderDetail_LoadingRowDetails(object sender, DataGridRowDetailsEventArgs e)
        {
            Grid grid = (Grid)e.DetailsElement;
            OrderDetails orderDetail = (OrderDetails)e.Row.DataContext;
            ListView OptionListView = (ListView)grid.FindName("Options");
            if (OptionListView != null)
                OptionListView.ItemsSource = _OrderDetailOptionList.ElementAt(e.Row.GetIndex());
            ListView ToppingsListView = (ListView)grid.FindName("Toppings");
            if (ToppingsListView != null)
                ToppingsListView.ItemsSource = _OrderDetailAdonList.ElementAt(e.Row.GetIndex());
        }

        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter == null)
                {
                    OrderDetail.ScrollIntoView(rowContainer, OrderDetail.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                }
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)OrderDetail.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                OrderDetail.UpdateLayout();
                OrderDetail.ScrollIntoView(OrderDetail.Items[index]);
                row = (DataGridRow)OrderDetail.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>
                    (v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public childItem FindVisualChild<childItem>(DependencyObject obj)
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
    }
}
