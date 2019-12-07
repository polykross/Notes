using System;
using Notes.Models;
using Notes.Tools.Services;
using Notes.Tools.Storage;

namespace Notes.Tools.Managers
{
    internal static class StationManager
    {
        private static INotesService _notesService;

        private static ICurrentUserStorage _currentUserStorage;

        internal static CustomerDTO CurrentUser { get; set; }

        internal static INotesService NotesService => _notesService;

        internal static void Initialize(INotesService notesService, ICurrentUserStorage currentUserStorage)
        {
            _notesService = notesService;
            _currentUserStorage = currentUserStorage;
            // get logged in user
            CurrentUser = _currentUserStorage.GetCurrentUser();
        }

        internal static void Logout()
        {
            CurrentUser = null;
        }

        internal static void CloseApp()
        {
            _currentUserStorage.SaveCurrentUser(CurrentUser);
            Environment.Exit(0);
        }
    }
}
