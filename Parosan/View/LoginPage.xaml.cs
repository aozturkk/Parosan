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
using Parosan.View;
using System.IO;

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
            databaseCheck();
        }

        private void databaseCheck()
        {
            //Firstly check database folder if it not exist , create it
            if (!Directory.Exists("db"))
            {
                Directory.CreateDirectory("db");
            }

            //Secondly check database file if it not exist , create it
            if (!File.Exists("db/parosan.db"))
            {
                DatabaseController databaseController = new DatabaseController();
                databaseController.createDatabase();
            }
            
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
            
            bool result = loginController.checkUser(usernameTextBox.Text,passwordBox.Password);

            if (result) 
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
                
            }
            else
            {
                loginFaildLabel.Content = "Incorrect username or password !";

            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {

            Register register = new Register();
            register.ShowDialog();
            
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (passwordBox.Password.Length == 0)
            {
                passwordTextBlock.Text = "Password";
            }

            else
            {
                passwordTextBlock.Text = "";
            }
               
        }
    }
}
