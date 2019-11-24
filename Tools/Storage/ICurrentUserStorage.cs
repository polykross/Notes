using Notes.Models;

namespace Notes.Tools.Storage
{
    internal interface ICurrentUserStorage
    {
        /// <summary>
        /// Get logged in user (that was saved after previous app runs).
        /// </summary>
        /// <returns>Logged in user</returns>
        CustomerDTO GetCurrentUser();

        /// <summary>
        /// Save user to retrieve in the next runs of the app.
        /// </summary>
        /// <param name="user">user to save</param>
        void SaveCurrentUser(CustomerDTO user);
    }
}
