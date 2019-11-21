using System;
using Notes.Tools;

namespace Notes.Models
{
    [Serializable]
    class AuthorizationDTO
    {
        #region Properties
        public string Login { get; private set; }
        private string Password { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="login">User's login.</param>
        /// <param name="password">Clear text password, which will be hashed using login as salt.</param>
        public AuthorizationDTO(string login, string password)
        {
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
