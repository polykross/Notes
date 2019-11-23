namespace Notes.Tools.Services
{
    internal class DTOAdapter
    {
        public static NotesServiceImpl.CustomerDTO ToServiceDTO(Models.CustomerDTO customer)
        {
            return new NotesServiceImpl.CustomerDTO
            {
                Guid = customer.Guid,
                Login = customer.Login,
                Password = customer.Password,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                LastLoginDate = customer.LastLoginDate
            };
        }

        public static NotesServiceImpl.NoteDTO ToServiceDTO(Models.NoteDTO note)
        {
            return new NotesServiceImpl.NoteDTO
            {
                Guid = note.Guid,
                Title = note.Title,
                Text = note.Text,
                CreationDate = note.CreationDate,
                LastEditDate = note.LastEditDate
            };
        }

        public static NotesServiceImpl.ShortNoteDTO ToServiceDTO(Models.ShortNoteDTO note)
        {
            return new NotesServiceImpl.ShortNoteDTO
            {
                Guid = note.Guid,
                Title = note.Title
            };
        }

        public static Models.CustomerDTO ToClientDTO(NotesServiceImpl.CustomerDTO customer)
        {
            return new Models.CustomerDTO(
                guid: customer.Guid,
                login: customer.Login,
                password: customer.Password,
                firstName: customer.FirstName,
                lastName: customer.LastName,
                email: customer.Email,
                lastLoginDate: customer.LastLoginDate
            );
        }

        public static Models.NoteDTO ToClientDTO(NotesServiceImpl.NoteDTO note)
        {
            return new Models.NoteDTO(
                guid: note.Guid,
                title: note.Title,
                text: note.Text,
                creationDate: note.CreationDate,
                lastEditDate: note.LastEditDate
            );
        }

        public static Models.ShortNoteDTO ToClientDTO(NotesServiceImpl.ShortNoteDTO note)
        {
            return new Models.ShortNoteDTO(
                guid: note.Guid,
                title: note.Title
            );
        }
    }
}
