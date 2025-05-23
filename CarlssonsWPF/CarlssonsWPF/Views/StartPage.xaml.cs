using System.Windows;
using System.Windows.Controls;
using CarlssonsWPF.Data.FileRepositories;
using System.Windows.Input;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Kunde;
using CarlssonsWPF.Views.Projekt;
using CarlssonsWPF.Model;

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

        private void StartPageDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the row that was clicked
            var row = sender as DataGridRow;
            if (row?.DataContext is RemindersData reminder && !string.IsNullOrEmpty(reminder.CaseNumber))
            {
                var projectRepo = new FileProjectRepository();
                var fullProject = projectRepo.GetByCaseNumber(reminder.CaseNumber);

                if (fullProject != null)
                {
                    _frame.Navigate(new ViewProjectView(_frame, fullProject));
                }
            }
            else
            {
                MessageBox.Show("Kunne ikke finde det valgte projekt", "Validering", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
