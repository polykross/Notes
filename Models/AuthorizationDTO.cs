﻿using System;
using Notes.Tools;
using System.Runtime.Serialization;

namespace Notes.Models
{
    [DataContract]
    internal class AuthorizationDTO
    {
        #region Fields
        [DataMember(Name = "Login")]
        private string _login;
        [DataMember(Name = "Password")]
        private string _password;
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
