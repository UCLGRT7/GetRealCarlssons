using System.Configuration;
using System.Data;
using System.Windows;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the repository to ensure folders and files are created
            ICustomerRepository customerRepo = new FileCustomerRepository();
            IContractRepository contractRepo = new FileContractRepository();
            IProjectRepository projectRepo = new FileProjectRepository();
            IServiceRepository servicesRepo = new FileServiceRepository();
        }

    }

}
