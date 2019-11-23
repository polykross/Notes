﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Notes.Tools.Navigation;
using Notes.ViewModels;
using Notes.Models;

namespace Notes.Views
{
    /// <summary>
    /// Interaction logic for NoteView.xaml
    /// </summary>
    public partial class NoteView : UserControl, INavigatable
    {
        internal NoteView()
        {
            InitializeComponent();
            DataContext = new NoteViewModel();
        }

        internal NoteView(NoteDTO note)
        {
            InitializeComponent();
            DataContext = new NoteViewModel(note);
        }
    }
}
