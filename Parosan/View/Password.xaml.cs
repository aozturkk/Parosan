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
        private void addPassword_Click(object sender, RoutedEventArgs e)
        {
            AddPassword addPassword = new AddPassword();
            addPassword.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            addPassword.ShowDialog();

            PasswordModel passwordModel = new PasswordModel();
            

            passwordModel.id = 5;
            passwordModel.account_name = addPassword.account_name.Text;
            passwordModel.username = addPassword.username.Text;
            passwordModel.password = addPassword.password.Text;
            passwordModel.address = addPassword.address.Text;
            passwordModel.user_id = "1";

            
            PasswordController passwordController = new PasswordController();
            passwordController.addPassword(passwordModel);
            passwordView.ItemsSource = passwordController.printPassword(); 
            ICollectionView view = CollectionViewSource.GetDefaultView(passwordView.ItemsSource);
            view.Refresh();

        }
        public void listPassword()
        {
            PasswordController passwordController = new PasswordController();
            passwordView.ItemsSource = passwordController.printPassword(); 

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

        }
    }
}
