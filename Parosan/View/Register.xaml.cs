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
    /// Register.xaml etkileşim mantığı
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            if(password.Text != passwordAgain.Text)
            {
                resultLabel.Content = "Passwords Mismatch !";
            }
        }

        private void cancelPasswordAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
