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
    public partial class AddPassword : Window
    {
        public AddPassword()
        {
            InitializeComponent();
        }

        private void cancelPasswordAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }

        private void passwordAdd_Click(object sender, RoutedEventArgs e)
        {
           
           
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
            
        }

        private void PasswordsTab_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
