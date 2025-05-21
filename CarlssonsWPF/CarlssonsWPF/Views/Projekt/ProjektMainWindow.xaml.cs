using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views.Kunde;

namespace CarlssonsWPF.Views.Projekt
{

    public partial class ProjektMainWindow : Page
    {
        private ProjektMainPageViewModel _projektMainPageViewModel;
        private Frame _frame;

        public ProjektMainWindow(Frame frame) 
        {
            InitializeComponent();
            _frame = frame;
            _projektMainPageViewModel = new ProjektMainPageViewModel();
            DataContext = _projektMainPageViewModel;
        }

        private void startPage_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new StartPage(_frame));
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new KundeMainWindow(_frame));
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
            var dataGrid = sender as DataGrid;
            if (dataGrid?.SelectedItem is CombinedProjectData combined)
            {
            
                var projectRepo = new FileProjectRepository();
                var fullProject = projectRepo.GetByCaseNumber(combined.CaseNumber);

                if (fullProject != null)
                {
                    _frame.Navigate(new ViewProjectView(_frame, fullProject));
                }
                else
                {
                    MessageBox.Show("Kunne ikke finde det valgte projekt.");
                }
            }
        }

    }
}
