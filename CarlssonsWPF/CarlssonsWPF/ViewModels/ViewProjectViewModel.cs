
using GetRealCarlssons.Models;

namespace GetRealCarlssons.ViewModels
{
    public class ViewProjectViewModel : BaseViewModel
    {
        public Project Project { get; }

        public ViewProjectViewModel(Project project)
        {
            Project = project;
        }

        // Kan tilføjes: ICommand til Rediger og Tilbage, evt. med navigation
    }
}
