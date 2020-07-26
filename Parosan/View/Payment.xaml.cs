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
using Parosan.Model;
using Parosan.Controller;
using System.ComponentModel;

namespace Parosan.View
{
    /// <summary>
    /// Payment.xaml etkileşim mantığı
    /// </summary>
    public partial class Payment : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        public Payment()
        {
            InitializeComponent();
            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
                listPayment();
        }
        public void listPayment()
        {
            PaymentController passwordController = new PaymentController();
            paymentView.ItemsSource = passwordController.printPayment();

        }
        private void deletePayment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editPayment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addPayment_Click(object sender, RoutedEventArgs e)
        {
            AddPayment addPayment = new AddPayment();
            PaymentController paymentController = new PaymentController();
           
            addPayment.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            addPayment.ShowDialog();

            if(addPayment.title.Text!=""|| addPayment.card_number.Text!=""|| addPayment.expiry_date.Text!="" || addPayment.cvc.Text != "")
            {
                PaymentModel newPayment = new PaymentModel();
                newPayment.title = addPayment.title.Text;
                newPayment.card_number = addPayment.card_number.Text;
                newPayment.expiry_date = addPayment.expiry_date.Text;
                newPayment.cvc = addPayment.cvc.Text;
                newPayment.pin = addPayment.pin.Text;

                paymentController.addPayment(newPayment);

                paymentView.ItemsSource = paymentController.printPayment();
                ICollectionView view = CollectionViewSource.GetDefaultView(paymentView.ItemsSource);
                view.Refresh();
            }

        
        }
           
    }
}
