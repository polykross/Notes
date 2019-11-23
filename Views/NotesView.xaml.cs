using System.Windows.Controls;
using Notes.Tools.Navigation;
using Notes.ViewModels;

namespace Notes.Views
{
    /// <summary>
    /// Interaction logic for NotesView.xaml
    /// </summary>
    public partial class NotesView : UserControl, INavigatable
    {
        public NotesView()
        {
            InitializeComponent();
            DataContext = new NotesViewModel();
        }
    }
}
