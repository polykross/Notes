using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Notes.Tools.Services;
using Notes.Tools.Managers;
using Notes.Tools.Navigation;
using Notes.ViewModels;
using Notes.Views;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            StationManager.Initialize(new StubNotesService());
            NavigationManager.Instance.Initialize(new NavigationModel(this));
            NavigationManager.Instance.Navigate(new SignInView());
            //NavigationManager.Instance.Navigate(new NotesView());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }
    }
}
