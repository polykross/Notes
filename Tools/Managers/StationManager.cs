using System;
using Notes.Models;
using Notes.Tools.Services;
using Notes.Tools.Storage;

namespace Notes.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        private static INotesService _notesService;

        private static ICurrentUserStorage _currentUserStorage;

        internal static CustomerDTO CurrentUser { get; set; }

        internal static INotesService NotesService => _notesService;

        internal static void Initialize(INotesService notesService, ICurrentUserStorage currentUserStorage)
        {
            _notesService = notesService;
            _currentUserStorage = currentUserStorage;
            CurrentUser = _currentUserStorage.GetCurrentUser();
        }

        internal static void Logout()
        {
            CurrentUser = null;
        }

        internal static void CloseApp()
        {
            if (CurrentUser != null)
            {
                _currentUserStorage.SaveCurrentUser(CurrentUser);
            }
            StopThreads?.Invoke();
            Environment.Exit(0);
        }
    }
}
