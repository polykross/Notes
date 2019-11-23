using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Notes.Models;
using Notes.Tools;
using Notes.Tools.Managers;
using Notes.Views;

namespace Notes.ViewModels
{
    internal class SignInViewModel : BaseViewModel
    {
        #region Fields
        private string _login;
        private string _password;

        #region Commands
        private ICommand _signInCommand;
        private ICommand _toSignUpCommand;
        #endregion
        #endregion

        #region Properties
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand =
                           new RelayCommand<object>(SignInImplementation, CanSignInExecute));
            }
        }

        public ICommand ToSignUpCommand
        {
            get
            {
                return _toSignUpCommand ?? (_toSignUpCommand = new RelayCommand<object>(ToSignUpImplementation));
            }
        }

        #endregion
        #endregion
        private bool CanSignInExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_login) && !String.IsNullOrWhiteSpace(_password);
        }

        private async void SignInImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                CustomerDTO currentUser;
                try
                {
                    string encryptedPassword = EncryptionHelper.GenerateHash(_password, _login);
                    currentUser = StationManager.NotesService.Login(_login, encryptedPassword);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed fo user {_login}. Reason:{Environment.NewLine}{ex.Message}");
                    return false;
                }
                if (currentUser == null)
                {
                    MessageBox.Show("Login or password is incorrect.");
                    return false;
                }
                StationManager.CurrentUser = currentUser;
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                NavigationManager.Instance.Navigate(new NotesView());
            }
        }

        private void ToSignUpImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(new SignUpView());
        }
    }
}
