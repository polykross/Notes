using System;
using Notes.Tools;

namespace Notes.Models
{
    [Serializable]
    internal class UserDTO
    {
        #region Properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Login { get; private set; }
        private string Password { get; set; }
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
        public UserDTO(string firstName, string lastName, string email, string login, string password)
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
