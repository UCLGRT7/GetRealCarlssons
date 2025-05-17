using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    public class ViewProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;

        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();
        public ObservableCollection<Contract> Contracts { get; set; } = new ObservableCollection<Contract>();


        // Redigeringstilstand
        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                    OnPropertyChanged(nameof(IsEditing));
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }

        public bool IsReadOnly => !IsEditing;

        public string EditButtonText => IsEditing ? "Bekræft redigering" : "Redigér";
        public string BackButtonText => IsEditing ? "Annullér redigering" : "Tilbage";

         private Project _selectedProject;
        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }


        public ICommand ToggleCommand { get; }

        public ICommand AddServiceCommand { get; }
        public ICommand RemoveServiceCommand { get; }
        public ICommand CancelCommand { get; }

        // Commands
        public ICommand ToggleEditCommand => new RelayCommand(_ => ToggleEdit());
        public ICommand CancelEditCommand => new RelayCommand(_ => CancelEdit());
        private void AddService()
        {
            if (SelectedProject.Services.Count < 10)
            {
                SelectedProject.Services.Add(new Services { Name = "", Complexity = 0 });
                OnPropertyChanged(nameof(SelectedProject));
            }
        }



        public ViewProjectViewModel(Project project)
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();

            foreach (var c in _customerRepository.GetAll()) Customers.Add(c);
            foreach (var p in _projectRepository.GetAll()) Projects.Add(p);
            foreach (var con in _contractRepository.GetAll()) Contracts.Add(con);

            SelectedProject = project;
            AddServiceCommand = new RelayCommand(_ => AddService(), _ => SelectedProject.Services.Count < 10);
            RemoveServiceCommand = new RelayCommand(obj => RemoveService(obj));
            CancelCommand = new RelayCommand(_ => CancelEdit());
        }

        private void ToggleEdit()
        {
            IsEditing = !IsEditing;
        }

        private void CancelEdit()
        {
            IsEditing = false;
        }

        private void RemoveService(object? service)
        {
            if (service is Services entry)
            {
                SelectedProject.Services.Remove(entry);
                OnPropertyChanged(nameof(SelectedProject));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
