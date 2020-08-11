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
using Parosan.Model;

namespace Parosan.View
{
    /// <summary>
    /// AddPassword.xaml etkileşim mantığı
    /// </summary>
    public partial class PasswordControl : Window
    {
        public int itemID;
        private PasswordController passwordController = new PasswordController();
        public PasswordControl()
        {
            InitializeComponent();
        }

        private void cancelSavePassword_Click(object sender, RoutedEventArgs e)
        {  
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;

        }

        private void savePassword_Click(object sender, RoutedEventArgs e)
        {

            PasswordModel passwordModel = new PasswordModel();
            passwordModel.account_name = account_name.Text;
            passwordModel.username = username.Text;
            passwordModel.password = password.Text;
            passwordModel.address = address.Text;

            if(controlType.Content.ToString() == "New Password")
            {
                passwordController.addPassword(passwordModel);
            }

            if (controlType.Content.ToString() == "Edit Password")
            {
                passwordModel.id = itemID;
                passwordController.updatePassword(passwordModel);
            }


            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;

           

        }


        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }
    }
}
