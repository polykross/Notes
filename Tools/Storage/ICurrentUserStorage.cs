using Notes.Models;

namespace Notes.Tools.Storage
{
    internal interface ICurrentUserStorage
    {
        CustomerDTO GetCurrentUser();
        void SaveCurrentUser(CustomerDTO user);
    }
}
