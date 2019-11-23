using System;
using Notes.Models;
using Notes.Tools.Services;

namespace Notes.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        private static INotesService _notesService;

        internal static CustomerDTO CurrentUser { get; set; }

        internal static INotesService NotesService => _notesService;

        internal static void Initialize(INotesService notesService)
        {
            _notesService = notesService;
            CurrentUser = new CustomerDTO("polina", "abc", "Polina", "Abc", "polina@gmail.com");
        }

        internal static void Logout()
        {
            CurrentUser = null;
        }

        internal static void CloseApp()
        {
            StopThreads?.Invoke();
            Environment.Exit(0);
        }
    }
}
