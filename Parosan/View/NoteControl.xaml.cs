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
    /// NoteControl.xaml etkileşim mantığı
    /// </summary>
    public partial class NoteControl : Window
    {
        public int itemID;
        private NoteController noteController = new NoteController();

        public NoteControl()
        {
            InitializeComponent();
        }

        private void cancelSaveNote_Click(object sender, RoutedEventArgs e)
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

        private void saveNote_Click(object sender, RoutedEventArgs e)
        {
            
            NoteModel noteModel = new NoteModel();
            noteModel.title = title.Text;
            var textRange = new TextRange(content.Document.ContentStart, content.Document.ContentEnd);
            noteModel.content = textRange.Text;
            noteModel.date = (DateTime.Now).ToString();
            
            if(controlType.Content.ToString() =="New Note")
            {
                noteController.addNewNote(noteModel);
            }

            if (controlType.Content.ToString() == "Edit Note")
            {
                noteModel.id = itemID;
                noteController.updateNote(noteModel);

            }

            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            mainWindow.Opacity = 1;

        }

    }
}
