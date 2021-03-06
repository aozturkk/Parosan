﻿using Parosan.Controller;
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
            noteView.ItemsSource = noteController.printNote();

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
            noteControl.itemID = selectedNote.id;
            noteControl.title.Text = selectedNote.title;
            noteControl.content.AppendText(selectedNote.content);
            noteControl.ShowDialog();

            listNote();
        }

        private void viewNote_Click(object sender, RoutedEventArgs e)
        {
            NoteControl viewNote = new NoteControl();
            viewNote.Owner = mainWindow;
            mainWindow.Opacity = 0.4;
            viewNote.controlType.Content = "View";

            //Disable save and close button and active close button
            viewNote.saveNote.Visibility = Visibility.Hidden;
            viewNote.cancelSaveNote.Visibility = Visibility.Hidden;
            viewNote.close.Visibility = Visibility.Visible;

            viewNote.title.Text = selectedNote.title;
            viewNote.title.IsReadOnly = true;

            viewNote.content.AppendText(selectedNote.content);
            viewNote.content.IsReadOnly = true;

            viewNote.ShowDialog();


        }

        private void deleteNote_Click(object sender, RoutedEventArgs e)
        {
            NoteController noteController = new NoteController();
            noteController.deleteNote(selectedNote.id);
            listNote();
        }

      
    }
}
