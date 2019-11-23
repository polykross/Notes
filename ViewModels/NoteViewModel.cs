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
    class NoteViewModel : BaseViewModel
    {
        #region Fields
        private NoteDTO _note;

        #region Commands
        private ICommand _goBackCommand;
        private ICommand _refreshCommand;
        private ICommand _logoutCommand;
        private ICommand _saveCommand;
        #endregion
        #endregion

        #region Properties
        public NoteDTO Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ?? (_goBackCommand =
                           new RelayCommand<object>(GoBackImplementation, CanGoBackExecute));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand =
                           new RelayCommand<object>(RefreshImplementation, CanRefreshExecute));
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand =
                           new RelayCommand<object>(LogoutImplementation, CanLogoutExecute));
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand =
                           new RelayCommand<object>(SaveImplementation, CanSaveExecute));
            }
        }

        #endregion
        #endregion

        private bool _newNote;

        public NoteViewModel()
        {
            _newNote = true;
            _note = new NoteDTO("", "");
        }

        public NoteViewModel(NoteDTO note)
        {
            _newNote = false;
            _note = note;
        }

        private bool CanGoBackExecute(object obj)
        {
            return true;
        }

        private void GoBackImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(new NotesView());
        }

        private bool CanRefreshExecute(object obj)
        {
            return !_newNote;
        }

        private async void RefreshImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                NoteDTO note = StationManager.NotesService.GetNote(Note.Guid);
                if (note != null)
                {
                    Note = note;
                }
                else
                {
                    MessageBox.Show($"Could not refresh note.");
                }
            });
            LoaderManager.Instance.HideLoader();
        }

        private bool CanLogoutExecute(object obj)
        {
            return true;
        }

        private void LogoutImplementation(object obj)
        {
            StationManager.Logout();
            NavigationManager.Instance.Navigate(new SignInView());
        }

        private bool CanSaveExecute(object obj)
        {
            return true;
        }

        private async void SaveImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            bool result = await Task.Run(() =>
            {
                NoteDTO note;
                if (_newNote)
                {
                    note = StationManager.NotesService.AddNote(Note, StationManager.CurrentUser.Guid);
                }
                else
                {
                    note = StationManager.NotesService.UpdateNote(Note);
                }
                if (note != null)
                {
                    Note = note;
                }
                else
                {
                    MessageBox.Show($"Could not save note.");
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
    }
}
