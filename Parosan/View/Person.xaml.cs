using Parosan.Controller;
using Parosan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Parosan.View
{
    /// <summary>
    /// Person.xaml etkileşim mantığı
    /// </summary>
    public partial class Person : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private PersonModel selectedPerson = new PersonModel();
        public Person()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listPerson();
        }
        private void listPerson()
        {
            PersonController personController = new PersonController();
            personView.ItemsSource = personController.printPerson();
            ICollectionView view = CollectionViewSource.GetDefaultView(personView.ItemsSource);
            view.Refresh();

        }
        private void newPerson_Click(object sender, RoutedEventArgs e)
        {
            PersonControl personControl = new PersonControl();
            PersonController personController = new PersonController();

            personControl.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            personControl.controlType.Content = "New Person";
            personControl.ShowDialog();


            listPerson();
        }

        private void editPerson_Click(object sender, RoutedEventArgs e)
        {

            PersonControl updatePerson = new PersonControl();

            updatePerson.name.Text = selectedPerson.name;
            updatePerson.surname.Text = selectedPerson.surname;
            updatePerson.phone.Text = selectedPerson.phone;
            updatePerson.email.Text = selectedPerson.email;
            updatePerson.description.Text = selectedPerson.description;
            updatePerson.itemID = selectedPerson.id;

            updatePerson.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            updatePerson.controlType.Content = "Edit Person";
            updatePerson.ShowDialog();

            listPerson();
        }

        private void deletePerson_Click(object sender, RoutedEventArgs e)
        {
            PersonController personController = new PersonController();
            personController.deletePerson(selectedPerson.id);

            listPerson();


        }

        private void personView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (PersonModel)personView.SelectedItem;
            if (item != null)
            {
                selectedPerson = item;
            }
        }

        private void viewPerson_Click(object sender, RoutedEventArgs e)
        {
            PersonControl viewPerson = new PersonControl();
            viewPerson.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            viewPerson.controlType.Content = "View";

            //Disable save and close button and active close button
            viewPerson.savePerson.Visibility = Visibility.Hidden;
            viewPerson.cancelSavePerson.Visibility = Visibility.Hidden;
            viewPerson.close.Visibility = Visibility.Visible;

            viewPerson.name.Text = selectedPerson.name;
            viewPerson.name.IsReadOnly = true;

            viewPerson.surname.Text = selectedPerson.surname;
            viewPerson.surname.IsReadOnly = true;

            viewPerson.phone.Text = selectedPerson.phone;
            viewPerson.phone.IsReadOnly = true;

            viewPerson.email.Text = selectedPerson.email;
            viewPerson.email.IsReadOnly = true;

            viewPerson.description.Text = selectedPerson.description;
            viewPerson.description.IsReadOnly = true;
            

            viewPerson.ShowDialog();
        }
    }
}
