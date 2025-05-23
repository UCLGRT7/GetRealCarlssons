using System.Collections.ObjectModel;
using System.ComponentModel;
using CarlssonsWPF.Model;


namespace CarlssonsWPF.ViewModel
{
    public class ProjectMainPageVM : INotifyPropertyChanged
    {
        private readonly ProjectViewService _projectViewService;
        private ObservableCollection<CombinedProjectData> _combinedProjectData;

        public ProjectMainPageVM()
        {
            _projectViewService = new ProjectViewService();
            CombinedProjects = _projectViewService.GetCombinedProjectModels();
            
        }
        public ObservableCollection<CombinedProjectData> CombinedProjects
        {
            get => _combinedProjectData;
            set
            {
                _combinedProjectData = value;
                OnPropertyChanged(nameof(CombinedProjects));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ReloadProjects()
        {
            CombinedProjects = _projectViewService.GetCombinedProjectModels();
        }

    }
}
