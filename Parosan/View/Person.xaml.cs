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
            PersonController passwordController = new PersonController();
            personView.ItemsSource = passwordController.printPerson();

        }
        private void addPerson_Click(object sender, RoutedEventArgs e)
        {
            PersonControl personControl = new PersonControl();
            PersonController personController = new PersonController();

            personControl.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            personControl.ShowDialog();

            if (personControl.name.Text != "" || personControl.surname.Text != "" )
            {
                PersonModel newPerson = new PersonModel();
                newPerson.name = personControl.name.Text;
                newPerson.surname = personControl.surname.Text;
                newPerson.phone = personControl.phone.Text;
                newPerson.email = personControl.email.Text;
                newPerson.description = personControl.description.Text;

                personController.addPerson(newPerson);

                personView.ItemsSource = personController.printPerson();
                ICollectionView view = CollectionViewSource.GetDefaultView(personView.ItemsSource);
                view.Refresh();
            }
        }

        private void editPerson_Click(object sender, RoutedEventArgs e)
        {

            PersonControl personControl = new PersonControl();

            personControl.name.Text = selectedPerson.name;
            personControl.surname.Text = selectedPerson.surname;
            personControl.phone.Text = selectedPerson.phone;
            personControl.email.Text = selectedPerson.email;
            personControl.description.Text = selectedPerson.description;

            personControl.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            personControl.ShowDialog();

            if (personControl.name.Text != "" || personControl.surname.Text != "")
            {
                PersonModel updetedPerson = new PersonModel();

                updetedPerson.id = selectedPerson.id;
                updetedPerson.name = personControl.name.Text;
                updetedPerson.surname = personControl.surname.Text;
                updetedPerson.phone = personControl.phone.Text;
                updetedPerson.email = personControl.email.Text;
                updetedPerson.description = personControl.description.Text;
                updetedPerson.user_id = UserModel.id;

                PersonController personController = new PersonController();
                personController.updatePerson(updetedPerson);
                
                personView.ItemsSource = personController.printPerson();
                ICollectionView view = CollectionViewSource.GetDefaultView(personView.ItemsSource);
                view.Refresh();
            }
        }

        private void deletePerson_Click(object sender, RoutedEventArgs e)
        {
            PersonController personController = new PersonController();
            personController.deletePerson(selectedPerson.id);

            personView.ItemsSource = personController.printPerson();
            ICollectionView view = CollectionViewSource.GetDefaultView(personView.ItemsSource);
            view.Refresh();


        }

        private void personView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (PersonModel)personView.SelectedItem;
            if (item != null)
            {
                selectedPerson = item;
            }
        }

       
    }
}
