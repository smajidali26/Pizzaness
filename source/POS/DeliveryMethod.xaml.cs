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
    /// Interaction logic for DeliveryMethod.xaml
    /// </summary>
    public partial class DeliveryMethod : Elysium.Controls.Window
    {
        public OrderType SelectedOrderType{get;set;}
        
        public DeliveryMethod(OrderType orderType)
        {
            InitializeComponent();
            SelectedOrderType = orderType;
        }

        private void DeliveryMethod_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner)._orderType = (OrderType)Convert.ToInt16(((Button)e.OriginalSource).CommandParameter);
            this.Close();
        }
    }
}
