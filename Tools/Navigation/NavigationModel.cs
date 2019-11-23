using System.Collections.Generic;
using Notes.Models;
using Notes.Views;

namespace Notes.Tools.Navigation
{
    internal class NavigationModel : INavigationModel
    {
        #region Fields
        private readonly IContentOwner _contentOwner;
        #endregion

        #region Properties
        protected IContentOwner ContentOwner
        {
            get => _contentOwner;
        }
        #endregion

        public NavigationModel(IContentOwner contentOwner)
        {
            _contentOwner = contentOwner;
        }

        public void Navigate(INavigatable view)
        {
            ContentOwner.ContentControl.Content = view;
        }

    }
}
