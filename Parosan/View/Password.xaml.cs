using System;
using System.Collections.Generic;
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
        public Password()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordController passwordController = new PasswordController();
            passwordController.printPassword();


            passowrdView.ItemsSource = passwordController.passwords;
        }   
        private void addPassword_Click(object sender, RoutedEventArgs e)
        {
            AddPassword addPassword = new AddPassword();
            addPassword.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            addPassword.ShowDialog();
            addPassword.Close();
        }
    }
}
