using System;
using System.Collections.Generic;
using System.Linq;
using Notes.NotesServiceImpl;
using CustomerDTO = Notes.Models.CustomerDTO;
using NoteDTO = Notes.Models.NoteDTO;
using ShortNoteDTO = Notes.Models.ShortNoteDTO;
using static Notes.Tools.Services.DTOAdapter;

namespace Notes.Tools.Services
{
    internal class NotesService: INotesService
    {
        private readonly NotesServiceClient _client;

        public NotesService()
        {
            _client = new NotesServiceImpl.NotesServiceClient("BasicHttpBinding_INotesService");
        }

        public CustomerDTO Register(CustomerDTO customer)
        {
            return ToClientDTO(_client.Register(ToServiceDTO(customer)));
        }

        public CustomerDTO Login(string login, string password)
        {
            return ToClientDTO(_client.Login(login, password));
        }

        public bool LoginExists(string login)
        {
            return _client.LoginExists(login);
        }

        public List<ShortNoteDTO> GetNotes(Guid customerGuid)
        {
            return _client.GetNotes(customerGuid).Select(ToClientDTO).ToList();
        }

        public NoteDTO GetNote(Guid guid)
        {
            return ToClientDTO(_client.GetNote(guid));
        }

        public NoteDTO AddNote(NoteDTO note, Guid customerGuid)
        {
            return ToClientDTO(_client.AddNote(ToServiceDTO(note), customerGuid));
        }

        public NoteDTO UpdateNote(NoteDTO note)
        {
            return ToClientDTO(_client.UpdateNote(ToServiceDTO(note)));
        }

        public bool DeleteNote(Guid guid)
        {
            return _client.DeleteNote(guid);
        }
    }
}
