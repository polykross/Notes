using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Notes.Models;
using Notes.Tools;
using Notes.Tools.Managers;
using Notes.Views;

namespace Notes.ViewModels
{
    internal class SignUpViewModel : BaseViewModel
    {
        #region Fields
        private string _login;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _email;

        #region Commands
        private ICommand _signUpCommand;
        private ICommand _toSignInCommand;
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
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        #region Commands

        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand =
                           new RelayCommand<object>(SignUpImplementation, CanSignUpExecute));
            }
        }

        public ICommand ToSignInCommand
        {
            get
            {
                return _toSignInCommand ?? (_toSignInCommand = new RelayCommand<object>(ToSignInImplementation));
            }
        }

        #endregion
        #endregion
        private bool CanSignUpExecute(object obj)
        {
            return !String.IsNullOrEmpty(_login) &&
                   !String.IsNullOrEmpty(_password) &&
                   !String.IsNullOrEmpty(_firstName) &&
                   !String.IsNullOrEmpty(_lastName) &&
                   !String.IsNullOrEmpty(_email);
        }

        private async void SignUpImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    if (!new EmailAddressAttribute().IsValid(_email))
                    {
                        MessageBox.Show($"Email {_email} is not valid.");
                        return false;
                    }
                    if (StationManager.NotesService.LoginExists(_login))
                    {
                        MessageBox.Show($"User with login {_login} already exists.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed for user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                try
                {
                    CustomerDTO user = new CustomerDTO(_login, _password, _firstName, _lastName, _email);
                    user = StationManager.NotesService.Register(user);
                    StationManager.CurrentUser = user;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign Up failed for user {_login}. Reason:{Environment.NewLine} {ex.Message}");
                    return false;
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                NavigationManager.Instance.Navigate(new NotesView());
            }
        }

        private void ToSignInImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(new SignInView());
        }
    }
}
