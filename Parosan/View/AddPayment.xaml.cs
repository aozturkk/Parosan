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
    /// AddPayment.xaml etkileşim mantığı
    /// </summary>
    public partial class AddPayment : Window
    {
        public AddPayment()
        {
            InitializeComponent();
        }

        private void paymentAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }

        private void cancelPaymentAdd_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            card_number.Text = "";
            expiry_date.Text = "";
            cvc.Text = "";
            pin.Text = "";
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }
    }
}
