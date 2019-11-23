using System;
using System.Collections.Generic;
using Notes.Models;
using System.Threading;

namespace Notes.Tools.Services
{
    internal class StubNotesService : INotesService
    {
        private List<NoteDTO> _notes = new List<NoteDTO>();

        public StubNotesService()
        {
            _notes.Add(new NoteDTO("Test note 1", "Test text\ntext\ntext text text\ntext"));
            _notes.Add(new NoteDTO("Test note 2", "Test text\ntext\ntext text text\ntext"));
            _notes.Add(new NoteDTO("Test note 3", "Test text\ntext\ntext text text\ntext"));
        }

        public NoteDTO AddNote(NoteDTO note, Guid customerGuid)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Add note for customer {customerGuid}: title {note.Title}, text {note.Text}");
            _notes.Add(note);
            return note;
        }

        public bool DeleteNote(Guid guid)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Delete note with guid {guid}");
            return true;
        }

        public NoteDTO GetNote(Guid guid)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Get note with guid {guid}");
            return new NoteDTO("Test note", "Test text\ntext\ntext text text\ntext");
        }

        public List<ShortNoteDTO> GetNotes(Guid customerGuid)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Get notes for {customerGuid}");
            List<ShortNoteDTO> res = new List<ShortNoteDTO>();
            foreach (NoteDTO note in _notes)
            {
                res.Add(new ShortNoteDTO(note.Guid, note.Title));
            }
            return res;
        }

        public CustomerDTO Login(string login, string password)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Login customer: login {login}, password {password}");
            return new CustomerDTO(login, password, "Polina", "Shlepakova", "polina@gmail.com");
        }

        public CustomerDTO Register(CustomerDTO customer)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Register customer: login {customer.Login}, password {customer.Password}, first name {customer.FirstName}, last name {customer.LastName}, email {customer.Email}");
            return new CustomerDTO(customer.Login, customer.Password, customer.FirstName, customer.LastName, customer.Email);
        }

        public NoteDTO UpdateNote(NoteDTO note)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Update note: title {note.Title}, text {note.Text}");
            return note;
        }

        public bool LoginExists(string login)
        {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Checked if user exists.");
            return login.Equals("abc");
        }
    }
}
