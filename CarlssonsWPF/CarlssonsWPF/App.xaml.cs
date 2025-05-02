using System.Configuration;
using System.Data;
using System.Windows;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Data.UnitOfWork;

namespace CarlssonsWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private FileUnitOfWork _unitOfWork;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the repository to ensure folders and files are created
            _unitOfWork = new FileUnitOfWork();

            // Additional startup logic can go here if needed
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _unitOfWork.Complete();
            base.OnExit(e);
        }
    }

}
