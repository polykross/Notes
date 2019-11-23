﻿using System;
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

        public ICommand AddNoteCommand
        {
            get
            {
                return _addNoteCommand ?? (_addNoteCommand =
                           new RelayCommand<object>(AddNoteImplementation, CanAddNoteExecute));
            }
        }

        public ICommand ShowNoteCommand
        {
            get
            {
                return _showNoteCommand ?? (_showNoteCommand =
                           new RelayCommand<object>(ShowNoteImplementation, CanShowNoteExecute));
            }
        }

        public ICommand DeleteNoteCommand
        {
            get
            {
                return _deleteNoteCommand ?? (_deleteNoteCommand =
                           new RelayCommand<object>(DeleteNoteImplementation, CanDeleteNoteExecute));
            }
        }

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

        private bool CanRefreshExecute(object obj)
        {
            return true;
        }

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
                return true;
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
            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }

        private bool CanAddNoteExecute(object obj)
        {
            return true;
        }

        private void AddNoteImplementation(object obj)
        {
            System.Console.WriteLine("Navigating to add note");
        }

        private bool CanShowNoteExecute(object obj)
        {
            return true;
        }

        private async void ShowNoteImplementation(object obj)
        {
            System.Console.WriteLine($"Loading note with guid {obj}");
            LoaderManager.Instance.ShowLoader();
            NoteDTO note;
            var result = await Task.Run(() =>
            {
                note = StationManager.NotesService.GetNote((Guid) obj);
                if (note == null)
                {
                    MessageBox.Show($"Could not load note.");
                    return false;
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                System.Console.WriteLine("Loaded note");
            }
        }

        private bool CanDeleteNoteExecute(object obj)
        {
            return true;
        }

        private async void DeleteNoteImplementation(object obj)
        {
            System.Console.WriteLine($"Deleting note with guid {obj}");
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                return StationManager.NotesService.DeleteNote((Guid)obj);
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                Notes.Remove(Notes.Where(note => note.Guid == (Guid)obj).First());
                System.Console.WriteLine("Deleted note");
            }
        }
    }
}
