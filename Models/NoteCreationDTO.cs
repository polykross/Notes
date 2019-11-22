using System;
using System.Runtime.Serialization;

namespace Notes.Models
{
    [DataContract]
    internal class NoteCreationDTO
    {
        #region Fields
        [DataMember(Name = "Title")]
        private string _title;
        [DataMember(Name = "Text")]
        private string _text;
        #endregion

        #region Properties
        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Text
        {
            get => _text;
            set => _text = value;
        }
        #endregion

        #region Constructor
        public NoteCreationDTO(string title, string text)
        {
            _title = title;
            _text = text;
        }
        #endregion
    }
}
