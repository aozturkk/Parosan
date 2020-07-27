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
        private PaymentModel selectedPayment = new PaymentModel();
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

        private void deletePayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentController paymentController = new PaymentController();
            paymentController.deletePayment(selectedPayment.id);
            paymentView.ItemsSource = paymentController.printPayment();
            ICollectionView view = CollectionViewSource.GetDefaultView(paymentView.ItemsSource);
            view.Refresh();
        }

        private void editPayment_Click(object sender, RoutedEventArgs e)
        {
            AddPayment updatePayment = new AddPayment();

            updatePayment.title.Text = selectedPayment.title;
            updatePayment.card_number.Text = selectedPayment.card_number;
            updatePayment.expiry_date.Text = selectedPayment.expiry_date;
            updatePayment.cvc.Text = selectedPayment.cvc;
            updatePayment.pin.Text = selectedPayment.pin;

            updatePayment.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            updatePayment.ShowDialog();

            if (updatePayment.title.Text != "" || updatePayment.card_number.Text != "" || updatePayment.expiry_date.Text != "" || updatePayment.cvc.Text != "")
            {
                PaymentModel updetedPayment = new PaymentModel();

                updetedPayment.id = selectedPayment.id;
                updetedPayment.title = updatePayment.title.Text;
                updetedPayment.card_number = updatePayment.card_number.Text;
                updetedPayment.expiry_date = updatePayment.expiry_date.Text;
                updetedPayment.cvc = updatePayment.cvc.Text;
                updetedPayment.pin = updatePayment.pin.Text;
                updetedPayment.user_id = UserModel.id;

                PaymentController paymentController = new PaymentController();
                paymentController.updatePayment(updetedPayment);

                paymentView.ItemsSource = paymentController.printPayment();
                ICollectionView view = CollectionViewSource.GetDefaultView(paymentView.ItemsSource);
                view.Refresh();
            }
        }
        private void paymentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (PaymentModel)paymentView.SelectedItem;
            if (item != null)
            {
                selectedPayment = item;
            }
        }
    }
}
