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
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
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
        /// <summary>
        /// Returns whether sign up can execute. It can execute if all fields are filled.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanSignUpExecute(object obj)
        {
            return !string.IsNullOrEmpty(_login) &&
                   !string.IsNullOrEmpty(_password) &&
                   !string.IsNullOrEmpty(_firstName) &&
                   !string.IsNullOrEmpty(_lastName) &&
                   !string.IsNullOrEmpty(_email);
        }

        /// <summary>
        /// Sign up user asyncronously. While the sign up is executing, the loader is shown.
        /// If user provided invalid email or login that already exists, the appropriate message is shown.
        /// </summary>
        /// <param name="obj"></param>
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
                    string encryptedPassword = EncryptionHelper.GenerateHash(_password, _login);
                    var user = new CustomerDTO(_login, encryptedPassword, _firstName, _lastName, _email);
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

        /// <summary>
        /// Navigate to sign in page.
        /// </summary>
        /// <param name="obj"></param>
        private void ToSignInImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(new SignInView());
        }
    }
}
