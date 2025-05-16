using System.Collections.ObjectModel;
using System.ComponentModel;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    public class ViewProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Project SelectedProject { get; set; }

        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;

        public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Project> projects { get; set; } = new ObservableCollection<Project>();
        public ObservableCollection<Contract> contracts { get; set; } = new ObservableCollection<Contract>();


        // Commands
        public ICommand ToggleEditCommand => new RelayCommand(_ => ToggleEdit());
        public ICommand CancelEditCommand => new RelayCommand(_ => CancelEdit());
        private void AddService()
        {
            if (SelectedProject.ServiceEntry.Count < 10)
            {
                SelectedProject.ServiceEntry.Add(new Services { Name = "", Complexity = 0 });
                OnPropertyChanged(nameof(SelectedProject));
            }
        }



        public ViewProjectViewModel(Project project)
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();


        }



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
