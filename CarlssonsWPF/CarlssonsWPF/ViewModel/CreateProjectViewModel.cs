using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Data.FileRepositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CarlssonsWPF.Dialogs;
using System.Text.Json;
using System.IO;
using CarlssonsWPF.Helpers;

namespace CarlssonsWPF.ViewModel
{
    public class CreateProjectViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IServiceRepository _serviceRepository;
        private ObservableCollection<ComplexityEntry> _complexityEntry;

        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();
        public ObservableCollection<Contract> Contracts { get; set; } = new ObservableCollection<Contract>();
        public ObservableCollection<ServiceEntry> Services { get; set; } = new ObservableCollection<ServiceEntry>();

        // Her er den korrekte ObservableCollection af SelectedServiceEntry
        public ObservableCollection<SelectedServiceEntry> SelectedServices { get; set; } = new ObservableCollection<SelectedServiceEntry>();



        public class SelectedServiceEntry : INotifyPropertyChanged
        {
            private ServiceEntry? _service;
            public ServiceEntry? Service
            {
                get => _service;
                set { _service = value; OnPropertyChanged(); }
            }

            private int? _complexity;
            public int? Complexity
            {
                get => _complexity;
                set { _complexity = value; OnPropertyChanged(); }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string? name = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public bool IsEditing { get; set; } = true;

        public ICommand CreateProjectCommand { get; set; }

        public ICommand CancelCommand { get; }

        public string NewServiceName { get; set; }

        public ICommand AddServiceEntry => new RelayCommand(() =>
        {
            // Vis dialog
            var dialog = new InputDialog();
            bool? result = dialog.ShowDialog();

            // Hvis bruger trykker OK
            if (result == true)
            {
                string name = dialog.ResponseText;

                // Tjek at navnet ikke er tomt
                if (!string.IsNullOrWhiteSpace(name))
                {
                    CommonCommands.AddServiceEntry(serviceRepository, name);
                }
                else
                {
                    MessageBox.Show("Navn kan ikke være tomt.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        });




        private Customer _customer;
        public Customer SelectedCustomer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged();

                // Opdater CustomerName med kun kundens navn som string
                if (SelectedProject != null && _customer != null)
                {
                   

                    // Opdater SelectedProject.CustomerName med kundens navn som string
                    SelectedProject.CustomerName = _customer.Name?.Trim();

                }
                else
                {
                }
            }
        }



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

        private string? _caseNumber;
        public string? CaseNumber
        {
            get => _caseNumber;
            set { _caseNumber = value; OnPropertyChanged(); }
        }

        private string? _address;
        public string? Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }


        private string? _projectPostalCode;
        public string? ProjectPostalCode
        {
            get => _projectPostalCode;
            set { _projectPostalCode = value; OnPropertyChanged(); }
        }

        private string? _offerSentInput;
        public string? OfferSentInput
        {
            get => _offerSentInput;
            set { _offerSentInput = value; OnPropertyChanged(); OfferSent = TryParseToDate(value); }
        }

        private DateTime? _deadline;
        public DateTime? Deadline
        {
            get => _deadline;
            set { if (_deadline != value) { _deadline = value; OnPropertyChanged(); } }
        }

        private DateTime? _offerSent;
        public DateTime? OfferSent
        {
            get => _offerSent;
            set { if (_offerSent != value) { _offerSent = value; OnPropertyChanged(); } }
        }

        public DateTime? OfferApproved { get; set; }
        public DateTime? Paid { get; set; }

        private string? _paidInput;
        public string? PaidInput
        {
            get => _paidInput;
            set { _paidInput = value; OnPropertyChanged(); Paid = TryParseToDate(value); }
        }

        private string? _offerApprovedInput;
        public string? OfferApprovedInput
        {
            get => _offerApprovedInput;
            set { _offerApprovedInput = value; OnPropertyChanged(); OfferApproved = TryParseToDate(value); }
        }

        private double _price;
        public double Price
        {
            get => _price;
            set { if (_price != value) { _price = value; OnPropertyChanged(); } }
        }

        private const int P = 100;
        private int _estimatedPrice;
        public int EstimatedPrice
        {
            get => _estimatedPrice;
            private set
            {
                if (_estimatedPrice != value)
                {
                    _estimatedPrice = value;
                    OnPropertyChanged(nameof(EstimatedPrice));
                }
            }
        }

        private int _scope;
        public int Scope
        {
            get => _scope;
            set
            {
                if (_scope != value)
                {
                    _scope = value;
                    OnPropertyChanged(nameof(_scope));
                    UpdateEstimatedPrice(); // Recalculate when scope changes
                }
            }
        }    
        

        private int _complexity;
        public int Complexity
        {
            get => _complexity;
            set
            {
                if (_complexity != value)
                {
                    _complexity = value;
                    OnPropertyChanged(nameof(_complexity));
                    UpdateEstimatedPrice(); // Recalculate when scope changes
                }
            }
        }

        public Action<Project>? NavigateToViewProject { get; set; }

        public CreateProjectViewModel()
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();
            _serviceRepository = new FileServiceRepository();

            CancelCommand = new RelayCommand(OnCancel);


            foreach (var customer in _customerRepository.GetAll()) Customers.Add(customer);
            foreach (var project in _projectRepository.GetAll()) Projects.Add(project);
            foreach (var contract in _contractRepository.GetAll()) Contracts.Add(contract);
            foreach (var service in _serviceRepository.GetAll()) Services.Add(service);

            UpdateEstimatedPrice();
            // Initialiser 10 tomme entries for SelectedServices og Complexities
            for (int i = 0; i < 10; i++)
            {
                var complexity = new ComplexityEntry();
                complexity.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(ComplexityEntry.Value))
                    {
                        OnPropertyChanged(nameof(complexity));
                        UpdateEstimatedPrice();
                    }
                };
                Complexities.Add(complexity);

                var selected = new SelectedServiceEntry();

                selected.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(SelectedServiceEntry.Service))
                        OnPropertyChanged(nameof(selected));
                };

                SelectedServices.Add(selected);
            }

            CreateProjectCommand = new RelayCommand(_ => CreateProject());
            SelectedProject = new Project();
            SelectedProject.Contract = new Contract();

            //Complexities.CollectionChanged += (s, e) => UpdateEstimatedPrice();
            Services.CollectionChanged += (s, e) => UpdateEstimatedPrice();
            SelectedServices.CollectionChanged += (s, e) => UpdateEstimatedPrice();

        }

       
        private void UpdateEstimatedPrice()
        {
            int totalComplexity = 0;
            foreach (var complexity in Complexities)
            {
                totalComplexity += complexity.Value;
            }

            EstimatedPrice = Scope * totalComplexity * P;
            System.Diagnostics.Debug.WriteLine($"Calculation: {Scope} * {totalComplexity} * {P} = {EstimatedPrice}");
        }

        private void Complexity_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ComplexityEntry.Value))
                UpdateEstimatedPrice();
        }

        public void CreateProject()
        {

            try
            {

                if (SelectedCustomer == null)
                {
                    MessageBox.Show("Du skal vælge en kunde før du opretter projektet.", "Validering", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                //int scopeValue = Scope;
                var servicesWithComplexity = SelectedServices.Select((selectedService, index) =>
                {
                    if (selectedService?.Service == null) return null;

                    int complexity = 0;
                    if (index < Complexities.Count)
                    {
                        int? complexityValue = Complexities[index].Value; // Assuming Value is nullable
                        if (complexityValue.HasValue)
                        {
                            complexity = complexityValue.Value;
                        }
                    }

                // ✅ Brug SelectedProject direkte
                var newProject = new Project
                {
                    Id = Guid.NewGuid(),
                    CustomerName = SelectedCustomer?.Name?.Trim(),
                    CaseNumber = CaseNumber,
                    ProjectAddress = Address,
                    ProjectPostalCode = int.TryParse(ProjectPostalCode, out int pc) ? pc : (int?)null,
                    Scope = Scope ?? 0,
                    EstimatedPrice = EstimatedPrice,
                    Price = Price,
                    LastModified = DateTime.Now,
                    Services = new ObservableCollection<ServiceEntry>(servicesWithComplexity),
                    Deadline = Deadline,
                    OfferSent = OfferSent,
                    OfferApproved = OfferApproved,
                    Paid = Paid
                };

<<<<<<< HEAD
                // 🛠 GEM projektet
                _projectRepository.Add(newProject);
                Projects.Add(newProject); // Hvis du ønsker det vist med det samme

                // ✅ Brug SelectedProject direkte
                SelectedProject.CustomerName = SelectedCustomer?.Name?.Trim();
                SelectedProject.CaseNumber = CaseNumber;
                SelectedProject.ProjectAddress = Address;
                SelectedProject.ProjectPostalCode = int.TryParse(ProjectPostalCode, out int pc) ? pc : (int?)null;
                SelectedProject.Scope = Scope;
                SelectedProject.EstimatedPrice = EstimatedPrice;
                SelectedProject.Contract.Price = Price;
                SelectedProject.LastModified = DateTime.Now;
                SelectedProject.Services = new List<ServiceEntry>(servicesWithComplexity);
                SelectedProject.Deadline = Deadline;
                SelectedProject.Contract.OfferSent = OfferSent;
                SelectedProject.Contract.OfferApproved = OfferApproved;
                SelectedProject.Contract.Paid = Paid;
=======
                // Naviger videre
                NavigateToViewProject?.Invoke(newProject);
>>>>>>> parent of 24065dc (Implemented Status + various fixes.)

                var contract = new Contract
                {
                    CaseNumber = CaseNumber,
                    OfferSent = OfferSent,
                    OfferApproved = OfferApproved,
                    Paid = Paid,
                    Price = Price
                };

                _contractRepository.Add(contract);
                Contracts.Add(contract);

                MessageBox.Show("Projektet er oprettet!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under oprettelse:\n{ex.Message}", "Teknisk fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private DateTime? TryParseToDate(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            input = input.Trim();

            string[] formats = { "dd/MM/yy", "dd/MM/yyyy", "ddMMyy", "ddMMyyyy" };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out var dt))
                    return dt;
            }

            if (DateTime.TryParse(input, out var fallback))
                return fallback;

            return null;
        }

        private void OnCancel()
        {
            // Navigering eller ryd logik
        }

        private string? _deadlineInput;
        public string? DeadlineInput
        {
            get => _deadlineInput;
            set { _deadlineInput = value; OnPropertyChanged(); Deadline = TryParseToDate(value); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}
