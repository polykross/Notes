using Notes.Tools.Navigation;

namespace Notes.Tools.Managers
{
    internal class NavigationManager
    {
        private static readonly object Locker = new object();
        private static NavigationManager _instance;

        /// <summary>
        /// Get a single instance of Notification Manager.
        /// </summary>
        internal static NavigationManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Locker)
                {
                    return _instance ?? (_instance = new NavigationManager());
                }
            }
        }

        private INavigationModel _navigationModel;

        private NavigationManager()
        {
        }

        internal void Initialize(INavigationModel navigationModel)
        {
            _navigationModel = navigationModel;
        }

        /// <summary>
        /// Navigate to a specific view.
        /// </summary>
        /// <param name="view">A view to navigate to.</param>
        internal void Navigate(INavigatable view)
        {
            _navigationModel.Navigate(view);
        }

    }
}
