﻿using System;
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
        /// <summary>
        /// Whether sign in command can be executed. It can be executed iff Login and Password are filled.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if sign in commang can be executed.</returns>
        private bool CanSignInExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }

        /// <summary>
        /// Sign user in asyncronously. While the sign in is executing, the loader is shown.
        /// If login or password is incorrect, a message for the user is shown.
        /// </summary>
        /// <param name="obj"></param>
        private async void SignInImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                CustomerDTO currentUser;
                try
                {
                    if (!StationManager.NotesService.LoginExists(_login))
                    {
                        MessageBox.Show("Login or password is incorrect.");
                        return false;
                    }
                    string encryptedPassword = EncryptionHelper.GenerateHash(_password, _login);
                    currentUser = StationManager.NotesService.Login(_login, encryptedPassword);
                }
                catch (Exception)
                {
                    MessageBox.Show("Login or password is incorrect.");
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

        /// <summary>
        /// Navigate to sign up page.
        /// </summary>
        /// <param name="obj"></param>
        private void ToSignUpImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(new SignUpView());
        }
    }
}
