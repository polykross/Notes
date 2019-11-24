using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Notes.Models;
using Notes.Tools;
using Notes.Tools.Managers;
using Notes.Tools.Navigation;
using Notes.Views;

namespace Notes.ViewModels
{
    internal class NotesViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<ShortNoteDTO> _notes;

        #region Commands
        private ICommand _refreshCommand;
        private ICommand _logoutCommand;
        private ICommand _addNoteCommand;
        private ICommand _showNoteCommand;
        private ICommand _deleteNoteCommand;
        #endregion
        #endregion

        #region Properties
        public ObservableCollection<ShortNoteDTO> Notes
        {
            get => _notes;
            private set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        #region Commands
        public ICommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand =
                new RelayCommand<object>(RefreshImplementation));

        public ICommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand =
                new RelayCommand<object>(LogoutImplementation));

        public ICommand AddNoteCommand =>
            _addNoteCommand ?? (_addNoteCommand =
                new RelayCommand<object>(AddNoteImplementation));

        public ICommand ShowNoteCommand =>
            _showNoteCommand ?? (_showNoteCommand =
                new RelayCommand<object>(ShowNoteImplementation));

        public ICommand DeleteNoteCommand =>
            _deleteNoteCommand ?? (_deleteNoteCommand =
                new RelayCommand<object>(DeleteNoteImplementation));

        #endregion
        #endregion

        internal NotesViewModel()
        {
            _notes = new ObservableCollection<ShortNoteDTO>(StationManager.NotesService.GetNotes(StationManager.CurrentUser.Guid));
            if (_notes == null)
            {
                MessageBox.Show($"Could not load notes.");
                _notes = new ObservableCollection<ShortNoteDTO>();
            }
        }
        
        /// <summary>
        /// Refresh a list of notes asyncronously and show loader.
        /// </summary>
        /// <param name="obj"></param>
        private async void RefreshImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                var notes = new ObservableCollection<ShortNoteDTO>(StationManager.NotesService.GetNotes(StationManager.CurrentUser.Guid));
                if (notes != null)
                {
                    Notes = notes;
                }
                else
                {
                    MessageBox.Show($"Could not load notes.");
                    Notes = new ObservableCollection<ShortNoteDTO>();
                }
            });
            LoaderManager.Instance.HideLoader();
        }

        /// <summary>
        /// Logout user and navigate to sign in.
        /// </summary>
        /// <param name="obj"></param>
        private void LogoutImplementation(object obj)
        {
            StationManager.Logout();
            NavigationManager.Instance.Navigate(new SignInView());
        }

        /// <summary>
        /// Navigate to empty note view.
        /// </summary>
        /// <param name="obj"></param>
        private void AddNoteImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(new NoteView());
        }

        /// <summary>
        /// Navigate to note if it exists. If note was deleted by other session, a message is shown.
        /// </summary>
        /// <param name="obj"></param>
        private async void ShowNoteImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            NoteDTO note = await Task.Run(() =>
            {
                try
                {
                    NoteDTO receivedNote = StationManager.NotesService.GetNote((Guid)obj);
                    return receivedNote;
                }
                catch (Exception)
                {
                    return null;
                }
            });
            LoaderManager.Instance.HideLoader();
            if (note == null)
            {
                MessageBox.Show($"Note does not exist (deleted by other session). You can refresh to see an updated list of notes.");
            }
            else
            {
                NavigationManager.Instance.Navigate(new NoteView(note));
            }
        }

        /// <summary>
        /// Delete note on server and in collection on client.
        /// </summary>
        /// <param name="obj"></param>
        private async void DeleteNoteImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() => StationManager.NotesService.DeleteNote((Guid)obj));
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                Notes.Remove(Notes.First(note => note.Guid == (Guid)obj));
            }
        }
    }
}
