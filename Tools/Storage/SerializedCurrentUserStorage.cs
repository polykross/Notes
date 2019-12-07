using Notes.Models;
using Notes.Tools.Managers;
using System;
using System.IO;

namespace Notes.Tools.Storage
{
    class SerializedCurrentUserStorage : ICurrentUserStorage
    {
        /// <summary>
        /// Get logged in user (that was saved after previous app runs).
        /// </summary>
        /// <returns>Logged in user</returns>
        public CustomerDTO GetCurrentUser()
        {
            try
            {
                return SerializationManager.Deserialize<CustomerDTO>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Save user to retrieve in the next runs of the app.
        /// </summary>
        /// <param name="user">user to save</param>
        public void SaveCurrentUser(CustomerDTO user)
        {
            try
            {
                SerializationManager.Serialize<CustomerDTO>(user, FileFolderHelper.StorageFilePath);
            }
            catch (Exception) { }
        }
    }
}
