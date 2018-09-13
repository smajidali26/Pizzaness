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
using BusinessEntities;

namespace POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Controls.Window
    {
        public UserLogin UserLoginObj { get; set; }

        public OrderType _orderType = OrderType.None;

        public ContactInfo ContactInfoObject = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(UserLogin userLogin)
        {
            InitializeComponent();
            UserLoginObj = userLogin;
        }

        private void OrderNow_Click(object sender, RoutedEventArgs e)
        {
            DeliveryMethod deliveryMethod = new DeliveryMethod(_orderType);
            deliveryMethod.Owner = this;
            deliveryMethod.ShowDialog();
            Proceed proceed = new Proceed();
            proceed.Owner = this;
            proceed.ShowDialog();

            Menu page = new Menu(_orderType,ContactInfoObject);
            page.Owner = this;
            page.ShowDialog();
        }
    }
}
