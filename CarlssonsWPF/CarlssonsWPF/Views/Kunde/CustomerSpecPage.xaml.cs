using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Views.Projekt;

namespace CarlssonsWPF.Views.Kunde
{

    public partial class CustomerSpecPage : Page
    {
        public IProjectRepository _projectRepository;
        private Frame _frame;
        public CustomerSpecPage(Frame frame, Customer selectedCustomer)
        {
            InitializeComponent();
            _frame = frame;
            DataContext = new CustomerSpecVM(selectedCustomer);

        }
        private void BackFromCustomerSpec_click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new CustomerMainPage(_frame));

        }

        private void CustomerSpecDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CustomerSpecDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CustomerSpecDataGrid.SelectedItem is ProjectWithContractInfoDatagrid selectedProjectInfo)
            {
                // Get the actual Project from the repository using the CaseNumber
                if (DataContext is CustomerSpecVM viewModel)
                {
                    var project = viewModel.ProjectRepository.GetByCaseNumber(selectedProjectInfo.CaseNumber);
                    if (project != null)
                    {
                        _frame.Navigate(new ViewProjectView(_frame, project));
                    }
                }
            }
        }
    }
}
