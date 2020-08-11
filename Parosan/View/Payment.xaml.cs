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
            PaymentController paymentController = new PaymentController();
            paymentView.ItemsSource = paymentController.printPayment();
            ICollectionView view = CollectionViewSource.GetDefaultView(paymentView.ItemsSource);
            view.Refresh();

        }
       

        private void newPayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentControl addPayment = new PaymentControl();
            PaymentController paymentController = new PaymentController();
           
            addPayment.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            addPayment.controlType.Content = "New Payment";
            addPayment.ShowDialog();

            listPayment();


        }

        private void deletePayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentController paymentController = new PaymentController();
            paymentController.deletePayment(selectedPayment.id);
            listPayment();
        }

        private void editPayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentControl updatePayment = new PaymentControl();

            updatePayment.title.Text = selectedPayment.title;
            updatePayment.card_number.Text = selectedPayment.card_number;
            updatePayment.expiry_date.Text = selectedPayment.expiry_date;
            updatePayment.cvc.Text = selectedPayment.cvc;
            updatePayment.pin.Text = selectedPayment.pin;

            updatePayment.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            updatePayment.controlType.Content = "Edit Payment";
            updatePayment.itemID = selectedPayment.id;

            updatePayment.ShowDialog();

            listPayment();
        }
        private void viewPayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentControl viewPayment = new PaymentControl();

            viewPayment.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            viewPayment.controlType.Content = "View";

            viewPayment.savePayment.Visibility = Visibility.Hidden;
            viewPayment.cancelSavePayment.Visibility = Visibility.Hidden;
            viewPayment.close.Visibility = Visibility.Visible;

            viewPayment.title.Text = selectedPayment.title;
            viewPayment.title.IsReadOnly = true;

            viewPayment.card_number.Text = selectedPayment.card_number;
            viewPayment.card_number.IsReadOnly = true;

            viewPayment.expiry_date.Text = selectedPayment.expiry_date;
            viewPayment.expiry_date.IsReadOnly = true;

            viewPayment.cvc.Text = selectedPayment.cvc;
            viewPayment.cvc.IsReadOnly = true;

            viewPayment.pin.Text = selectedPayment.pin;
            viewPayment.pin.IsReadOnly = true;

            viewPayment.ShowDialog();

        }
        // Selcet item for editing 
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
