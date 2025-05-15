using System.Configuration;
using System.Data;
using System.Windows;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.ViewModel.IRepositories;
using System.ComponentModel;

namespace CarlssonsWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContractRepository _contractRepository;
        private ICustomerRepository _customerRepository;
        private IProjectRepository _projectRepository;
        private IServiceRepository _serviceRepository;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the repository to ensure folders and files are created
            _customerRepository = new FileCustomerRepository();
            _contractRepository = new FileContractRepository();
            _projectRepository = new FileProjectRepository();
            _serviceRepository = new FileServiceRepository();

            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            SaveUnsavedChanges();
        }

        private void SaveUnsavedChanges()
        {
            _contractRepository?.SaveAllOnExit();
            _customerRepository?.SaveAllOnExit();
            _projectRepository?.SaveAllOnExit();
            _serviceRepository?.SaveAllOnExit();
        }
    }

}
