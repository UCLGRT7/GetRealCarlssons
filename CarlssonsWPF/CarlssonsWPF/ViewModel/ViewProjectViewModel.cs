using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;

namespace CarlssonsWPF.ViewModel
{
    public class ViewProjectViewModel(Project project) : BaseViewModel
    {
        public Project Project { get; } = project;

        // Kan tilføjes: ICommand til Rediger og Tilbage, evt. med navigation
    }
}
