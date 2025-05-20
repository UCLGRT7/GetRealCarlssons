using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;

namespace CarlssonsWPF.ViewModel
{
    public class ViewProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IServiceRepository _serviceRepository;


        public ObservableCollection<Customer> Customers { get; set; } = new();
        public ObservableCollection<Project> Projects { get; set; } = new();
        public ObservableCollection<Contract> Contracts { get; set; } = new();
        public ObservableCollection<ServiceEntry> Services { get; set; } = new();


       

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
                    OnPropertyChanged(nameof(EditButtonText));
                    OnPropertyChanged(nameof(BackButtonText));
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
                OnPropertyChanged();
            }
        }

        public ICommand ToggleEditCommand { get; }
        public ICommand CancelCommand { get; }

        private void OnCancel()
        {
            // Navigér eller ryd data
        }
        public ICommand AddServiceCommand { get; }
        public ICommand RemoveServiceCommand { get; }

        public ViewProjectViewModel(Project selectedProject, ServiceEntry selectedService)
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();
            _serviceRepository = new FileServiceRepository();

            ServiceEntry.AvailableServices = Services
                .Where(s => s.Service != null)
                .Select(s => s.Service!)
                .ToList();

            foreach (var c in _customerRepository.GetAll()) Customers.Add(c);
            foreach (var p in _projectRepository.GetAll()) Projects.Add(p);
            foreach (var con in _contractRepository.GetAll()) Contracts.Add(con);
            foreach (var s in _serviceRepository.GetAll()) Services.Add(s);

            SelectedProject = selectedProject ?? throw new System.ArgumentNullException(nameof(selectedProject));
            var savedCustomerName = SelectedProject.CustomerName;
            SelectedProject.InitFromModel();
            SelectedProject.CustomerName = savedCustomerName;




            if (selectedService != null)
            {
                var existingService = Services.FirstOrDefault(s => s.Id == selectedService.Id);
                if (existingService != null)
                {
                    SelectedProject.Services.Add(existingService); // Tilføj den valgte service til projektet
                }
            }

            // Sørg for at der altid er 10 service-entries
            while (SelectedProject.Services.Count < 10)
            {
                SelectedProject.Services.Add(new ServiceEntry());
            }



            // Matcher eksisterende ServiceEntries med de reelle Service-objekter
            foreach (var entry in SelectedProject.Services)
            {
                var match = Services.FirstOrDefault(s => s.Id == entry.Id);
                if (match != null)
                {
                    // Tildel et Service objekt til entry.Service
                    entry.Service = new Service
                    {
                        Id = match.Id,
                        Name = match.Name
                    };
                }
            }

            // Debug
            System.Diagnostics.Debug.WriteLine($"Services count: {SelectedProject.Services.Count}");
            foreach (var s in SelectedProject.Services)
                System.Diagnostics.Debug.WriteLine($"Service: {s.Name}, Complexity: {s.Complexity}");

            AddServiceCommand = new RelayCommand(_ => AddService(), _ => SelectedProject.Services.Count < 10);
            RemoveServiceCommand = new RelayCommand(RemoveService);
            ToggleEditCommand = new RelayCommand(ToggleEdit);
            //CancelEditCommand = new RelayCommand(CancelEdit);

            {
                CancelCommand = new RelayCommand(OnCancel); // eller noget lignende
            }


        }

        private void ToggleEdit()
        {
            if (IsEditing)
            {
                // 🛠️ Opdater alle ServiceEntry-felter ud fra Id
                foreach (var entry in SelectedProject.Services)
                {
                    var match = Services.FirstOrDefault(s => s.Id == entry.Id);
                    if (match != null)
                    {
                        entry.Name = match.Name;
                        entry.Service = new Service
                        {
                            Id = match.Id,
                            Name = match.Name
                        };
                        entry.OnPropertyChanged(nameof(entry.Name));
                        entry.OnPropertyChanged(nameof(entry.Service));
                    }
                }

                // Gem ændringer
                _projectRepository.Update(SelectedProject);
            }

            // Sørg for at der altid er 10 linjer
            while (SelectedProject.Services.Count < 10)
            {
                SelectedProject.Services.Add(new ServiceEntry());
            }

            IsEditing = !IsEditing;
            OnPropertyChanged(nameof(SelectedProject));
        }


        private void CancelEdit()
        {
            IsEditing = false;
            SelectedProject.InitFromModel(); // Gendanner visning fra model-data
            OnPropertyChanged(nameof(SelectedProject));
        }

        private void AddService()
        {
            if (SelectedProject.Services.Count < 10)
            {
                SelectedProject.Services.Add(new ServiceEntry { Name = "", Complexity = 0 });

                OnPropertyChanged(nameof(SelectedProject));
            }


        }


        private void RemoveService(object? service)
        {
            if (service is ServiceEntry entry)
            {
                SelectedProject.Services.Remove(entry);
                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }

}
