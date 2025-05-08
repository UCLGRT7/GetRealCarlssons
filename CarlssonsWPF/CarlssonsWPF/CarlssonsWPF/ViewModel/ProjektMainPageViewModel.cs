using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    public class ProjektMainPageViewModel
    {
        private readonly ProjectViewService _projectViewService;
        private ObservableCollection<CombinedProjectData> _combinedProjectData;

        //public ObservableCollection<CombinedProjectData> CombinedData { get; set; } = new ObservableCollection<CombinedProjectData>();

        public ProjektMainPageViewModel()
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

    }
}
