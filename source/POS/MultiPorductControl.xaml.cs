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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessEntities;

namespace POS
{
    /// <summary>
    /// Interaction logic for MultiPorductControl.xaml
    /// </summary>
    public partial class MultiPorductControl : UserControl
    {
        #region Properties

        public ICollection<Products> MultiProducts { get; set; }

        #endregion

        #region Delegate
        // Delegate declaration
        public delegate void UpdatePrice(object sender, EventArgs e);

        // Event declaration
        public event UpdatePrice OnUpdatePrice;

        #endregion

        #region Constructor
        
        public MultiPorductControl(ICollection<Products> product)
        {
            InitializeComponent();
            MultiProducts = product;
            SetTabs();
        }

        #endregion

        #region Events
        
        #endregion

        #region Methods

        private void SetTabs()
        {
            for (int i = 0; i < MultiProducts.Count; i++)
            {
                AddTab(MultiProducts.ElementAt(i).Name, MultiProducts.ElementAt(i));
            }
        }

        private void AddTab(String tabName,Products product)
        {
            TabItem item1 = new TabItem();
            item1.Header = tabName;
            ProductControl productControl = new ProductControl(product);
            productControl.OnUpdatePrice += productControl_OnUpdatePrice;
            item1.Content = productControl;
            MultiProductTabs.Items.Add(item1);
        }

        private void productControl_OnUpdatePrice(object sender, EventArgs e)
        {
            if (OnUpdatePrice != null)
            {
                OnUpdatePrice(sender, e);
            }
        }

        public Double GetPrice()
        {
            Double price = 0;
            foreach (TabItem item in MultiProductTabs.Items)
            {
                ProductControl productControl = (ProductControl)item.Content;
                price += productControl.GetPrice();
            }
            return price;
        }

        public List<OrderDetailSubProduct> GetOrderDetailSubProducts()
        {
            List<OrderDetailSubProduct> products = new List<OrderDetailSubProduct>();
            foreach (TabItem item in MultiProductTabs.Items)
            {
                ProductControl productControl = (ProductControl)item.Content;
                products.Add(productControl.GetOrderDetailSubProduct());
            }
            return products;
        }

        #endregion
    }
}
