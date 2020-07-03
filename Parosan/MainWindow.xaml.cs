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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Parosan.Controller;
using Parosan.View;
using Parosan.Model;

namespace Parosan
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            username.Content = UserModel.username;
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void passwordsTabClick(object sender, RoutedEventArgs e)
        {
            Password password = new Password();
           // password.passwordView.Items.Clear();
            MainController.callUserControl(mainContent, password);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainController.callUserControl(mainContent, new Password());
           
        }


        private void paymentsTab_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment();
            // password.passwordView.Items.Clear();
            MainController.callUserControl(mainContent, payment);
        }

        private void exitTab_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
