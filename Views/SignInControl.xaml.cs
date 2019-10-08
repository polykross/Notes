using System;
using System.Windows;
using System.Windows.Controls;

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
        }
        private void SignInBt_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TBLogin.Text) ||
                String.IsNullOrWhiteSpace(PBPassword.Password))
            {
                MessageBox.Show("Login or password is empty!");
                return;
            }
            MessageBox.Show($"Login successful for user {TBLogin.Text}");
            // todo switch to main window
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0); // Application.Close(); MainWindow.Close();
        }

        private void SignUpBt_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TBLogin.Text) ||
                String.IsNullOrWhiteSpace(PBPassword.Password))
            {
                MessageBox.Show("Login or password is empty!");
                return;
            }
            MessageBox.Show($"User with name {TBLogin.Text} was created");
            // todo switch to main window
        }
    }
}
