using System;
using System.Windows;

namespace POS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

    }
}
