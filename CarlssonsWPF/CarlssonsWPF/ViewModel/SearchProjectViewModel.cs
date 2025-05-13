
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CarlssonsWPF.Service;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Views;

namespace CarlssonsWPF.ViewModel
{
    public class SearchProjectViewModel : BaseViewModel
    {
        public string CaseNumber { get; set; }
        public string Address { get; set; }
        public string Scope { get; set; }
        public string Deadline { get; set; }

        public ObservableCollection<Services> Services { get; set; } = [];
        public ObservableCollection<Project> SearchResults { get; set; } = [];

        public Project SelectedProject { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OpenProjectCommand { get; set; }

        public SearchProjectViewModel()
        {
            for (int i = 0; i < 5; i++)
            {
                Services.Add(new Services());
            }

            SearchCommand = new RelayCommand(Search);
            CancelCommand = new RelayCommand(Cancel);
            OpenProjectCommand = new RelayCommand(OpenSelectedProject);
        }

        private void Cancel()
        {
            throw new NotImplementedException();
        }

        private void Search(string deadline)
        {
            Search(Deadline);
        }

        private void Search(DateTime deadline)
        {
            var projects = FileService.Load<Project>("Data/projects.json");
            var filtered = projects.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(CaseNumber))
                filtered = filtered.Where(p => p.CaseNumber == CaseNumber);

            if (!string.IsNullOrWhiteSpace(Address))
                filtered = filtered.Where(p => p.ProjectAddress.Contains(Address, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(Scope) && int.TryParse(Scope, out int s))
                filtered = filtered.Where(p => p.Scope == s);

            if (!string.IsNullOrWhiteSpace(Deadline))
                filtered = filtered.Where(p => p.Deadline == deadline);

            var serviceFilter = Services.Where(se => !string.IsNullOrWhiteSpace(se.ServiceType)).ToList();

            if (serviceFilter.Count != 0)
            {
                filtered = filtered.Where(p =>
                    serviceFilter.All(sf =>
                        p.Services.Any(ps => ps.ServiceType == sf.ServiceType && ps.Complexity == sf.Complexity)));
            }

            SearchResults.Clear();
            foreach (var p in filtered)
                SearchResults.Add(p);
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
