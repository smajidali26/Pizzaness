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
using BusinessService;

namespace POS
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Elysium.Controls.Window
    {
        #region Private
        private UsersManager _userManager = new UsersManager();
        #endregion

        public Login()
        {
            InitializeComponent();
            Username.Focus();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Progress.Visibility = System.Windows.Visibility.Visible;
            UserLogin userLogin = _userManager.ValidateUser(Username.Text.Trim(), Password.Password);
            if (userLogin != null)
            {
                MainWindow mainWindow = new MainWindow(userLogin);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
