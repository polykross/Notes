using System.Collections.Generic;

namespace Notes.Tools.Navigation
{
    internal abstract class BaseNavigationModel : INavigationModel
    {
        #region Fields
        private readonly IContentOwner _contentOwner;
        private readonly Dictionary<ViewType, INavigatable> _viewsDictionary;
        #endregion

        #region Properties
        protected IContentOwner ContentOwner
        {
            get => _contentOwner;
        }

        protected Dictionary<ViewType, INavigatable> ViewsDictionary
        {
            get => _viewsDictionary;
        }
        #endregion

        protected BaseNavigationModel(IContentOwner contentOwner)
        {
            _contentOwner = contentOwner;
            _viewsDictionary = new Dictionary<ViewType, INavigatable>();
        }

        public void Navigate(ViewType viewType)
        {
            if (!ViewsDictionary.ContainsKey(viewType))
                InitializeView(viewType);
            ContentOwner.ContentControl.Content = ViewsDictionary[viewType];
        }

        protected abstract void InitializeView(ViewType viewType);

    }
}
