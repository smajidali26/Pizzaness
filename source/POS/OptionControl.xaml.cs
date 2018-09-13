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

namespace POS
{
    /// <summary>
    /// Interaction logic for OptionControl.xaml
    /// </summary>
    public partial class OptionControl : UserControl
    {
        public OptionControl()
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(OptionName))
            {
                OptionText.Text = OptionName;
            }
        }

        public static readonly DependencyProperty OptionNameProperty = DependencyProperty.Register("OptionName", typeof(String), typeof(OptionControl), new FrameworkPropertyMetadata(string.Empty));

        public String OptionName
        {
            get { return GetValue(OptionNameProperty).ToString(); }
            set { SetValue(OptionNameProperty, value); }
        }

        public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.Register("IsMultiSelect", typeof(bool), typeof(OptionControl), new FrameworkPropertyMetadata(false));
        
        public bool IsMultiSelect
        {
            get { return (bool)GetValue(IsMultiSelectProperty); }
            set { SetValue(IsMultiSelectProperty, value); }
        }

        public static readonly DependencyProperty IsSamePriceProperty = DependencyProperty.Register("IsSamePrice", typeof(bool), typeof(OptionControl), new FrameworkPropertyMetadata(false));

        public bool IsSamePrice
        {
            get { return (bool)GetValue(IsSamePriceProperty); }
            set { SetValue(IsSamePriceProperty, value); }
        }
    }
}
