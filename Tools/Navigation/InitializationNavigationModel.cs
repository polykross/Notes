using System;
using Notes.Views;

namespace Notes.Tools.Navigation
{
    internal class InitializationNavigationModel : NavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
        }

        //protected override void InitializeView(ViewType viewType)
        //{
        //    switch (viewType)
        //    {
        //        case ViewType.SignIn:
        //            ViewsDictionary.Add(viewType, new SignInView());
        //            break;
        //        case ViewType.SignUp:
        //            ViewsDictionary.Add(viewType, new SignUpView());
        //            break;
        //        case ViewType.Main:
        //            ViewsDictionary.Add(viewType, new NotesView());
        //            break;
        //        case ViewType.AddNote:
        //            ViewsDictionary.Add(viewType, new NoteView());
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
        //    }
        //}
    }
}
