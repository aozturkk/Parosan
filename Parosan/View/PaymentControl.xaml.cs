using Parosan.Controller;
using Parosan.Model;
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
    public partial class PaymentControl : Window
    {
        public int itemID;
        private PaymentController paymentController = new PaymentController();

        public PaymentControl()
        {
            InitializeComponent();
        }

        private void savePayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentModel paymentModel = new PaymentModel();

            paymentModel.title = title.Text;
            paymentModel.card_number = card_number.Text;
            paymentModel.expiry_date = expiry_date.Text;
            paymentModel.cvc = cvc.Text;
            paymentModel.pin = pin.Text;

            if (controlType.Content.ToString() == "New Payment")
            {
                paymentController.addPayment(paymentModel);
            }

            if (controlType.Content.ToString() == "Edit Payment")
            {
                paymentModel.id = itemID;
                paymentController.updatePayment(paymentModel);
            }

            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }

        private void cancelSavePayment_Click(object sender, RoutedEventArgs e)
        {
           
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
