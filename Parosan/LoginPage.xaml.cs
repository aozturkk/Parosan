using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Parosan.Controller;

namespace Parosan
{
    /// <summary>
    /// LoginPage.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            LoginController loginController = new LoginController();
            
            bool result = loginController.checkUser(usernameTextBox.Text,passwordTextBox.Text);

            if (result) 
            {
                Console.WriteLine("login succesful !");
            }
            else
            {
                loginFaildLabel.Content = "Incorrect username or password !";
            }
        }

       
    }
}
