using System.Windows.Controls;
using Notes.Tools.Navigation;
using Notes.ViewModels;

namespace Notes.Views
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl, INavigatable
    {
        internal SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
