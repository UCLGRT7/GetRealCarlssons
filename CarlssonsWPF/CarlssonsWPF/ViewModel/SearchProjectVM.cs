
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CarlssonsWPF.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CarlssonsWPF.Helpers;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.ViewModel.IRepositories;
using System.Collections.Generic;

namespace CarlssonsWPF.ViewModel
{
    public class SearchProjectVM : INotifyPropertyChanged
    {
        private string? _caseNumber;
        private string? _projectAdress;
        private string? _deadline;
        private string? _scope;

        private readonly IProjectRepository _projectRepository;
        private List<Project> allProjects;
        public ObservableCollection <Project> Projects { get; set; } = new ObservableCollection<Project>();

        public ObservableCollection<ServiceEntry> Services { get; set; } = [];
        public ObservableCollection<Project> SearchResults { get; set; } = [];

        public Project SelectedProject { get; set; }

        public RelayCommand SearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OpenProjectCommand { get; set; }

        public SearchProjectVM()
        {
            for (int i = 0; i < 5; i++)
            {
                Services.Add(new ServiceEntry());
            }

            _projectRepository = new FileProjectRepository();
            allProjects = _projectRepository.GetAll().ToList();

            foreach (var project in allProjects)
            {
                Projects.Add(project);
            }

            CancelCommand = new RelayCommand(Cancel);

            SearchCommand = new RelayCommand(_ => SearchProject());

        }
        private void Cancel()
        {
            NavigationHelper.ExecuteGoBack();
        }

        public string? CaseNumber
        {
            get => _caseNumber;
            set
            {
                _caseNumber = value;
                OnPropertyChanged(nameof(CaseNumber));
            }
        }
        public string? ProjectAddress
        {
            get => _projectAdress;
            set
            {
                _projectAdress = value;
                OnPropertyChanged(nameof(ProjectAddress));
            }
        }
        public string? Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }
        public string? Scope
        {
            get => _scope;
            set
            {
                _scope = value;
                OnPropertyChanged(nameof(Scope));
            }
        }
        public void SearchProject()
        {
            var filtered = allProjects.Where(project =>
                (string.IsNullOrWhiteSpace(CaseNumber) || project.CaseNumber?.ToLower().Contains(CaseNumber.ToLower().Trim()) == true) &&
                (string.IsNullOrWhiteSpace(ProjectAddress) || project.ProjectAddress?.ToLower().Contains(ProjectAddress.ToLower().Trim()) == true) &&
                (string.IsNullOrWhiteSpace(Deadline) || project.Deadline.ToString()?.ToLower().Contains(Deadline.Trim()) == true) &&
                (string.IsNullOrWhiteSpace(Scope) || project.Scope.ToString().Contains(Scope.Trim()))
            ).ToList();

            // Update the Projects collection but DO NOT clear allProjects
            Projects.Clear();
            foreach (var match in filtered)
                Projects.Add(match);

            // Also clear and update SearchResults collection
            SearchResults.Clear();
            foreach (var match in filtered)
                SearchResults.Add(match);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}
