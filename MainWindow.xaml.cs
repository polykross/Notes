using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Notes.Tools.Services;
using Notes.Tools.Managers;
using Notes.Tools.Navigation;
using Notes.ViewModels;
using Notes.Views;
using Notes.Tools.Storage;

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
            StationManager.Initialize(new NotesService(), new SerializedCurrentUserStorage());
            NavigationManager.Instance.Initialize(new NavigationModel(this));
            if (StationManager.CurrentUser != null)
            {
                NavigationManager.Instance.Navigate(new NotesView());
            }
            else
            {
                NavigationManager.Instance.Navigate(new SignInView());
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }
    }
}
