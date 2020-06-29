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
using Parosan.Controller;

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
            RegisterController registerController = new RegisterController();

            if (username.Text==""||password.Password=="" || passwordAgain.Password == "")
            {
                resultLabel.Content = "Please fill all area !";
            }
            else if(password.Password != passwordAgain.Password)
            {
                resultLabel.Content = "Passwords Mismatch !";
            }
            else
            {
                if (registerController.registerUser(username.Text, password.Password))
                {
                    resultLabel.Foreground = new SolidColorBrush(Colors.Blue);
                    resultLabel.Content = "Register Completed !";
                    username.Text = "";
                    password.Password = "";
                    passwordAgain.Password = "";
                   

                }
                else
                {
                    resultLabel.Content = "Username is used !";
                }

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
