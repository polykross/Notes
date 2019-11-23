using System.Windows.Controls;

namespace Notes.Views
{
    /// <summary>
    /// Interaction logic for NoteView.xaml
    /// </summary>
    public partial class NoteView : UserControl, INavigatable
    {
        internal NoteView()
        {
            InitializeComponent();
            DataContext = new NoteViewModel();
        }

        internal NoteView(NoteDTO note)
        {
            InitializeComponent();
            DataContext = new NoteViewModel(note);
        }
    }
}
