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
            PasswordController passwordController = new PasswordController();
            passwordView.ItemsSource = passwordController.printPassword();

        }
        private void addPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordControl addPassword = new PasswordControl();
            PasswordController passwordController = new PasswordController();

            addPassword.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            addPassword.controlType.Content = "Add Password";
            addPassword.ShowDialog();


            if(addPassword.account_name.Text!=""|| addPassword.username.Text!="" || addPassword.password.Text != "")
            {
                PasswordModel passwordModel = new PasswordModel();
                passwordModel.account_name = addPassword.account_name.Text;
                passwordModel.username = addPassword.username.Text;
                passwordModel.password = addPassword.password.Text;
                passwordModel.address = addPassword.address.Text;




                passwordController.addPassword(passwordModel);


                passwordView.ItemsSource = passwordController.printPassword();
                ICollectionView view = CollectionViewSource.GetDefaultView(passwordView.ItemsSource);
                view.Refresh();

            }

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
            PasswordController passwordController = new PasswordController();

            passwordController.deletePassword(selectedPassword.id);

            passwordView.ItemsSource = passwordController.printPassword();
            ICollectionView view = CollectionViewSource.GetDefaultView(passwordView.ItemsSource);
            view.Refresh();
        }

        private void editPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordControl updatePassword = new PasswordControl();

            updatePassword.account_name.Text = selectedPassword.account_name;
            updatePassword.username.Text = selectedPassword.username;
            updatePassword.password.Text = selectedPassword.password;
            updatePassword.address.Text = selectedPassword.address;

            updatePassword.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            updatePassword.controlType.Content = "Edit Password";
            updatePassword.ShowDialog();

            if (updatePassword.account_name.Text != "" || updatePassword.username.Text != "" || updatePassword.password.Text != "")
            {
                PasswordModel updetedPassword = new PasswordModel();

                updetedPassword.id = selectedPassword.id;
                updetedPassword.account_name = updatePassword.account_name.Text;
                updetedPassword.username = updatePassword.username.Text;
                updetedPassword.password = updatePassword.password.Text;
                updetedPassword.address = updatePassword.address.Text;
                updetedPassword.user_id = UserModel.id;

                PasswordController passwordController = new PasswordController();
                passwordController.updatePassword(updetedPassword);

                passwordView.ItemsSource = passwordController.printPassword();
                ICollectionView view = CollectionViewSource.GetDefaultView(passwordView.ItemsSource);
                view.Refresh();
            }
        }
    }
}
