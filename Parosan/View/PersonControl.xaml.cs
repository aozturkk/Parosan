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
    /// PersonControl.xaml etkileşim mantığı
    /// </summary>
    public partial class PersonControl : Window
    {
        public int itemID;
        private PersonController personController = new PersonController();
      
        public PersonControl()
        {
            InitializeComponent();
        }
        private void savePerson_Click(object sender, RoutedEventArgs e)
        {
            PersonModel personModel = new PersonModel();
            personModel.name = name.Text;
            personModel.surname = surname.Text;
            personModel.phone = phone.Text;
            personModel.email = email.Text;
            personModel.description = description.Text;

            if (controlType.Content.ToString() == "New Person")
            {
                personController.addPerson(personModel);
            }

            if (controlType.Content.ToString() == "Edit Person")
            {
                personModel.id = itemID;
                personController.updatePerson(personModel);

            }



            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;
        }
        private void cancelSavePerson_Click(object sender, RoutedEventArgs e)
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
