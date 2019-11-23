using System.Windows.Controls;
using Notes.Tools.Navigation;
using Notes.ViewModels;

namespace Notes.Views
{
    public partial class SignUpView : UserControl, INavigatable
    {
        internal SignUpView()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }
    }
}