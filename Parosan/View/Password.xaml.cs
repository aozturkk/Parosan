using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Parosan.Controller;
using Parosan.Model;
using Parosan.View;

namespace Parosan.View
{
    /// <summary>
    /// Password.xaml etkileşim mantığı
    /// </summary>
    public partial class Password : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private PasswordModel selectedPassword = new PasswordModel();
        private PasswordController passwordController = new PasswordController(); 

        public Password()
        {
            InitializeComponent();
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listPassword();
        }

        public void listPassword()
        {          
           
            passwordView.ItemsSource = passwordController.printPassword();
            ICollectionView view = CollectionViewSource.GetDefaultView(passwordView.ItemsSource);
            view.Refresh();

        }
        private void newPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordControl newPassword = new PasswordControl();

            newPassword.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            newPassword.controlType.Content = "New Password";
            newPassword.ShowDialog();

            listPassword();
        }
   

        private void passwordView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (PasswordModel)passwordView.SelectedItem;
            if (item != null)
            {
                selectedPassword = item;
            }
        }

        private void deletePassword_Click(object sender, RoutedEventArgs e)
        {
          
            passwordController.deletePassword(selectedPassword.id);

            listPassword();
        }

        private void editPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordControl updatePassword = new PasswordControl();

            updatePassword.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            
            updatePassword.controlType.Content = "Edit Password";
            updatePassword.itemID = selectedPassword.id;
            updatePassword.account_name.Text = selectedPassword.account_name;
            updatePassword.username.Text = selectedPassword.username;
            updatePassword.password.Text = selectedPassword.password;
            updatePassword.address.Text = selectedPassword.address;

            updatePassword.ShowDialog();

            listPassword();

        }

        private void viewPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordControl viewPassword = new PasswordControl();
            viewPassword.Owner = mainWindow;
            mainWindow.Opacity = 0.4;

            viewPassword.controlType.Content = "View";

            viewPassword.savePassword.Visibility = Visibility.Hidden;
            viewPassword.cancelSavePassword.Visibility = Visibility.Hidden;
            viewPassword.close.Visibility = Visibility.Visible;

            viewPassword.account_name.Text = selectedPassword.account_name;
            viewPassword.account_name.IsReadOnly = true;

            viewPassword.username.Text = selectedPassword.username;
            viewPassword.username.IsReadOnly = true;

            viewPassword.password.Text = selectedPassword.password;
            viewPassword.password.IsReadOnly = true;

            viewPassword.address.Text = selectedPassword.address;
            viewPassword.address.IsReadOnly = true;


            viewPassword.ShowDialog();


        }
    }
}
