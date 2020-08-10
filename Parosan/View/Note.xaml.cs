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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Parosan.View
{
    /// <summary>
    /// Note.xaml etkileşim mantığı
    /// </summary>
    public partial class Note : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        private NoteModel selectedNote = new NoteModel(); 
        public Note()
        {
            InitializeComponent();
        }
        public void listNote()
        {
            NoteController noteController = new NoteController();
            noteView.ItemsSource = noteController.listNote();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            listNote();
        }
        private void noteView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (NoteModel)noteView.SelectedItem;
            if (item != null)
            {
                selectedNote = item;
            }
        }

        private void newNote_Click(object sender, RoutedEventArgs e)
        {
            NoteControl noteControl = new NoteControl();
            noteControl.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            noteControl.controlType.Content = "New Note";
            noteControl.ShowDialog();
            listNote();
        }

        private void editNote_Click(object sender, RoutedEventArgs e)
        {
            NoteControl noteControl = new NoteControl();
            noteControl.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            noteControl.controlType.Content = "Edit Note";
            noteControl.ShowDialog();
            listNote();
        }

        private void viewNote_Click(object sender, RoutedEventArgs e)
        {
            NoteControl noteControl = new NoteControl();
            noteControl.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            noteControl.controlType.Content = "View";
            noteControl.saveNote.Visibility = Visibility.Hidden;
            noteControl.cancelSaveNote.Visibility = Visibility.Hidden;
            noteControl.close.Visibility = Visibility.Visible;
            noteControl.title.Text = selectedNote.title;
            noteControl.title.IsReadOnly = true;
            noteControl.content.AppendText(selectedNote.content);
            noteControl.content.IsReadOnly = true;
            noteControl.ShowDialog();


        }

        private void deleteNote_Click(object sender, RoutedEventArgs e)
        {
            NoteController noteController = new NoteController();
            noteController.deleteNote(selectedNote.id);
            listNote();
        }

      
    }
}
