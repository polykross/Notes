using System;

namespace Notes.Models
{
    internal class User
    {
        #region Fields
        private Guid _guid;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _login;
        private string _password;
        private DateTime _lastLogin;
        #endregion

        #region Properties
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            private set
            {
                _guid = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            private set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            private set
            {
                _lastName = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            private set
            {
                _email = value;
            }
        }
        public string Login
        {
            get
            {
                return _login;
            }
            private set
            {
                _login = value;
            }
        }
        private string Password
        {
            get
            {
                return _password;
            }
        }
        public DateTime LastLogin
        {
            get
            {
                return _lastLogin;
            }
            private set
            {
                _lastLogin = value;
            }
        }
        #endregion

        #region Constructor

        public User(string firstName, string lastName, string email, string login, string password, DateTime lastLogin)
        {
            _guid = Guid.NewGuid();
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _login = login;
            SetPassword(password);
            _lastLogin = lastLogin;
        }
        #endregion

        private void SetPassword(string password)
        {
            // TODO: Add encryption
            _password = password;
        }

        internal bool CheckPassword(string password)
        {
            // TODO: Compare encrypted passwords
            return _password == password;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
