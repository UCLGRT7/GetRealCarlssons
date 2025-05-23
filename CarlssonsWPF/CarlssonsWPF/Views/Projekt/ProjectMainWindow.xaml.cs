using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Kunde;

namespace CarlssonsWPF.Views.Projekt
{
    public partial class ProjectMainWindow : Page
    {

        private ProjectMainPageVM _projektMainPageViewModel;

        private Frame _frame;


        public ProjectMainWindow(Frame frame) 
        {
            InitializeComponent();
            _frame = frame;
            _projektMainPageViewModel = new ProjectMainPageVM();
            DataContext = _projektMainPageViewModel;

            this.IsVisibleChanged += ProjektMainWindow_IsVisibleChanged;
        }


        private void startPage_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StartPage(_frame));
        }

        private void ProjektMainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible && _projektMainPageViewModel != null)
            {
                _projektMainPageViewModel.ReloadProjects();
            }
        }


        private void customer_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new CustomerMainPage(_frame));
        }

        private void reminders_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new RemindersWindow(_frame));
        }

        private void projectSearch_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new SearchProjectView(_frame));
        }

        private void createProject_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new CreateProjectView(_frame));
        }

        private void ProjectDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the row that was clicked
            var row = sender as DataGridRow;
            if (row?.DataContext is CombinedProjectData combined && !string.IsNullOrEmpty(combined.CaseNumber))
            {
                var projectRepo = new FileProjectRepository();
                var fullProject = projectRepo.GetByCaseNumber(combined.CaseNumber);

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
