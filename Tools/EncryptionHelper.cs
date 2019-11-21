using System;
using System.Text;
using System.Security.Cryptography;

namespace Notes.Tools
{
    internal static class EncryptionHelper
    {
        /// <summary>
        /// Generate hash for input.
        /// </summary>
        /// <param name="input">String to hash.</param>
        /// <param name="salt">Salt to prepend to the input before hashing.</param>
        internal static string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(salt + input);
            SHA256Managed sha256ManagedString = new SHA256Managed();
            byte[] hash = sha256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
