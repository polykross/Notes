using GalaSoft.MvvmLight.Command;
using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Notes.ViewModels
{
    internal class SignInViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _login;
        private string _password;

        #region Commands
        private RelayCommand<object> _signInCommand;
        private RelayCommand<object> _signUpCommand;
        private RelayCommand<object> _closeCommand;
        #endregion
        #endregion

        #region Properties
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value.Replace(" ", "Space");
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = "";
                for (int i = 0; i < value.Length; i++)
                {
                    _password += "*";
                }
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand<object>(
                           o =>
                           {
                               MessageBox.Show($"Login successful for user {_login}");
                               // TODO: switch to main window

                           }, o => CanExecuteCommand()));
            }
        }

        public RelayCommand<Object> SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand<object>(
                           o =>
                           {
                               MessageBox.Show($"User with name {_login} was created");
                               // TODO: switch to main window

                           }, o => CanExecuteCommand()));
            }
        }

        public RelayCommand<Object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>(o => Environment.Exit(0)));
            }
        }

        #endregion
        #endregion

        public bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_login) && !string.IsNullOrWhiteSpace(_password);
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
