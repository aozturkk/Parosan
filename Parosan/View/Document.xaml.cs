using Microsoft.Win32;
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

namespace Parosan.View
{
    /// <summary>
    /// Document.xaml etkileşim mantığı
    /// </summary>
    public partial class Document : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        public Document()
        {
            InitializeComponent();
        }

        private void addDocument_Click(object sender, RoutedEventArgs e)
        {
            DocumentControl documentControl = new DocumentControl();
         
            documentControl.Owner = mainWindow;
            mainWindow.Opacity = 0.4;

            documentControl.ShowDialog();
        }

        private void exportDocument_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void deleteDocument_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
