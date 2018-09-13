using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BusinessEntities;

namespace POS
{
    public class POSWindow : Window
    {
        public POSWindow()
        {
        }

        #region Properties
        public string PageProperty { get; set; }

        public UserLogin UserLoginObj { get; set; }
        #endregion
    }
}
