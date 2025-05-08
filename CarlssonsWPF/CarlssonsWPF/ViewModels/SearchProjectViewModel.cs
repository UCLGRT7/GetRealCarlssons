
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GetRealCarlssons.Models;
using GetRealCarlssons.Services;
using GetRealCarlssons.Views;

namespace GetRealCarlssons.ViewModels
{
    public class SearchProjectViewModel : BaseViewModel
    {
        public string CaseNumber { get; set; }
        public string Address { get; set; }
        public string Scope { get; set; }
        public string Deadline { get; set; }

        public ObservableCollection<ServiceEntry> Services { get; set; } = new();
        public ObservableCollection<Project> SearchResults { get; set; } = new();

        public Project SelectedProject { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OpenProjectCommand { get; set; }

        public SearchProjectViewModel()
        {
            for (int i = 0; i < 5; i++)
            {
                Services.Add(new ServiceEntry());
            }

            SearchCommand = new RelayCommand(Search);
            CancelCommand = new RelayCommand(Cancel);
            OpenProjectCommand = new RelayCommand(OpenSelectedProject);
        }

        private void Search()
        {
            var projects = FileService.Load<Project>("Data/projects.json");
            var filtered = projects.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(CaseNumber))
                filtered = filtered.Where(p => p.CaseNumber == CaseNumber);

            if (!string.IsNullOrWhiteSpace(Address))
                filtered = filtered.Where(p => p.Address.ToLower().Contains(Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(Scope) && int.TryParse(Scope, out int s))
                filtered = filtered.Where(p => p.Scope == s);

            if (!string.IsNullOrWhiteSpace(Deadline))
                filtered = filtered.Where(p => p.Deadline == Deadline);

            var serviceFilter = Services.Where(se => !string.IsNullOrWhiteSpace(se.Name)).ToList();

            if (serviceFilter.Any())
            {
                filtered = filtered.Where(p =>
                    serviceFilter.All(sf =>
                        p.Services.Any(ps => ps.Name == sf.Name && ps.Complexity == sf.Complexity)));
            }

            SearchResults.Clear();
            foreach (var p in filtered)
                SearchResults.Add(p);
        }

        private void Cancel()
        {
            // Navigation tilbage til start
        }

        private void OpenSelectedProject()
        {
            if (SelectedProject != null)
            {
                var view = new ViewProjectView
                {
                    DataContext = new ViewProjectViewModel(SelectedProject)
                };
                view.Show();
            }
        }
    }
}
