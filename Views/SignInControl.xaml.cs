using System;
using System.Windows;
using System.Windows.Controls;
using Notes.ViewModels;

namespace Notes.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SignInControl : UserControl
    {
        public SignInControl()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
