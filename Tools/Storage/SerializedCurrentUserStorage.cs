using Notes.Models;
using Notes.Tools.Managers;
using System;
using System.IO;

namespace Notes.Tools.Storage
{
    class SerializedCurrentUserStorage : ICurrentUserStorage
    {
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
