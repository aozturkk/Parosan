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

namespace Parosan.View
{
    /// <summary>
    /// PersonControl.xaml etkileşim mantığı
    /// </summary>
    public partial class PersonControl : Window
    {
        public PersonControl()
        {
            InitializeComponent();
        }
        private void savePerson_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }
        private void cancelSavePerson_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            surname.Text = "";
            phone.Text = "";
            email.Text = "";
            description.Text = "";
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }

      
    }
}
