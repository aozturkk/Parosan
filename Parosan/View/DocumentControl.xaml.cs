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
using System.Windows.Shapes;

namespace Parosan.View
{
    /// <summary>
    /// DocumentControl.xaml etkileşim mantığı
    /// </summary>
    public partial class DocumentControl : Window
    {
        public DocumentControl()
        {
            InitializeComponent();
        }

        private void documentSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            documentNameRaw.Content = openFileDialog.SafeFileName;

        }
    }
}
