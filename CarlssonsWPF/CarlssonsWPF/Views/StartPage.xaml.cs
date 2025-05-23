using System.Windows;
using System.Windows.Controls;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Kunde;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views
{

    public partial class StartPage : Page
    {
  
        private Frame _frame;
        private StartPageViewModel _viewModel;
        public StartPage(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            _viewModel = new StartPageViewModel();
            DataContext = _viewModel;
        }
        private void project_Click(object sender, RoutedEventArgs e)
        {

            _frame.Navigate(new ProjektMainWindow(_frame));

        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {

            _frame.Navigate(new KundeMainWindow(_frame));
        }

        private void reminder_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new RemindersWindow(_frame));
        }
    }
}
