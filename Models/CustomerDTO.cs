using Notes.Tools;
using System.Runtime.Serialization;

namespace Notes.Models
{
    [DataContract]
    internal class CustomerDTO
    {
        #region Fields
        [DataMember(Name = "Login")]
        private string _login;
        [DataMember(Name = "Password")]
        private string _password;
        [DataMember(Name = "FirstName")]
        private string _firstName;
        [DataMember(Name = "LastName")]
        private string _lastName;
        [DataMember(Name = "Email")]
        private string _email;
        #endregion

        #region Properties
        public string Login
        {
            get => _login;
            set => _login = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">User's last name.</param>
        /// <param name="email">User's email.</param>
        /// <param name="login">User's login.</param>
        /// <param name="password">Clear text password, which will be hashed using login as salt.</param>
        public CustomerDTO(string firstName, string lastName, string email, string login, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            SetPassword(password);
        }
        #endregion

        /// <summary>
        /// Sets password after hashing it with salt (login).
        /// </summary>
        /// <param name="password">Clear text password.</param>
        private void SetPassword(string password)
        {
            Password = EncryptionHelper.GenerateHash(password, Login);
        }
    }
}
