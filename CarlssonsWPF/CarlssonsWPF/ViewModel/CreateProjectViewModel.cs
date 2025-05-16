
using System;
using System.Windows; // for MessageBox
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CarlssonsWPF.Model;
using CarlssonsWPF.Service;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Data.FileRepositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CarlssonsWPF.ViewModel
{




    public class CreateProjectViewModel : BaseViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IServiceRepository _serviceRepository;

        public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Project> projects { get; set; } = new ObservableCollection<Project>();
        public ObservableCollection<Contract> contracts { get; set; } = new ObservableCollection<Contract>();
        public ObservableCollection<Services> services { get; set; } = new ObservableCollection<Services>();

        private const int P = 100; // Justeres til hvad end 1 Point skal koste i kroner.

        public ICommand AddServiceCommand { get; }

 




        // 5 ydelser fra brugeren
        public ObservableCollection<Services> Services { get; set; } = new();
        public string? SelectedCustomer { get; set; }
        public string? CaseNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? Deadline { get; set; }
        public int? Scope { get; set; }

        public DateTime? OfferSent { get; set; }
        public DateTime? OfferApproved { get; set; }
        public DateTime? Paid { get; set; }
        public double Price { get; set; }
        public Action<Project>? NavigateToViewProject { get; set; }
        public ICommand CreateProjectCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public double EstimatedPrice { get; set; }

        private void AddService()
        {
            var entry = new Services();
            Services.Add(entry);
            UpdateEstimatedPrice();
            CommandManager.InvalidateRequerySuggested();
        }

        private string? _offerSentInput;
        public string? OfferSentInput
        {
            get => _offerSentInput;
            set
            {
                _offerSentInput = value;
                OnPropertyChanged();
                OfferSent = TryParseToDate(value);
            }
        }

        private string? _offerApprovedInput;
        public string? OfferApprovedInput
        {
            get => _offerApprovedInput;
            set
            {
                _offerApprovedInput = value;
                OnPropertyChanged();
                OfferApproved = TryParseToDate(value);
            }
        }

        private string? _paidInput;
        public string? PaidInput
        {
            get => _paidInput;
            set
            {
                _paidInput = value;
                OnPropertyChanged();
                Paid = TryParseToDate(value);
            }
        }


        public CreateProjectViewModel()
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();
            _serviceRepository = new FileServiceRepository();

            foreach (var customer in _customerRepository.GetAll())
            {
                customers.Add(customer);
            }
            foreach (var project in _projectRepository.GetAll())
            {
                projects.Add(project);
            }
            foreach (var contract in _contractRepository.GetAll())
            {
                contracts.Add(contract);
            }
            foreach (var service in _serviceRepository.GetAll())
            {
                services.Add(service);
            }
            Services.CollectionChanged += (s, e) => UpdateEstimatedPrice();

            for (int i = 0; i < 5; i++)
            {
                var entry = new Services();
                Services.Add(entry);
            }

            // Knapkommando
            AddServiceCommand = new RelayCommand(_ => AddService(), _ => Services.Count < 10);

            CreateProjectCommand = new RelayCommand(_ => CreateProject());


        }
        private void UpdateEstimatedPrice()
        {
            if (Scope == null) return;

            int totalComplexity = Services.Sum(s => s.Complexity);
            EstimatedPrice = Scope.Value * totalComplexity * P;
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
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        private DateTime? TryParseToDate(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            input = input.Trim();

            // 6-cifret form: ddMMyy → 24/03/95
            if (input.Length == 6)
            {
                var parsed = DateTime.TryParseExact(input, "ddMMyy", null, System.Globalization.DateTimeStyles.None, out var date);
                return parsed ? date : null;
            }

            // 8-cifret form: ddMMyyyy → 24/03/1995
            if (input.Length == 8)
            {
                var parsed = DateTime.TryParseExact(input, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out var date);
                return parsed ? date : null;
            }

            // Sidste chance: normal DateTime.Parse
            if (DateTime.TryParse(input, out var fallbackDate))
                return fallbackDate;

            return null;
        }

        public DateTime? ParsedDeadline { get; private set; }

        private string? _deadlineInput;
        public string? DeadlineInput
        {
            get => _deadlineInput;
            set
            {
                _deadlineInput = value;
                OnPropertyChanged();
                ParsedDeadline = TryParseToDate(value);
            }
        }


        public void CreateProject()
        {
            try
            {


                // Sikrer stabile værdier, således at >>null<< ikke runtime crasher.
                int scopeValue = Scope ?? 0;
                DateTime deadlineValue = Deadline ?? DateTime.Today;
                DateTime offerSentValue = OfferSent ?? DateTime.MinValue;
                DateTime offerApprovedValue = OfferApproved ?? DateTime.MinValue;
                DateTime paymentReceivedValue = Paid ?? DateTime.MinValue;

                var project = new Project
                {

                    CustomerName = SelectedCustomer,
                    CaseNumber = CaseNumber,
                    ProjectAddress = Address,
                    Deadline = ParsedDeadline ?? DateTime.Today,
                    Scope = scopeValue,
                    Services = Services.Select((s, index) => new Services
                    {
                        Id = index + 1,
                        Name = s.Name,
                        Complexity = s.Complexity
                    }).ToList(),
                    EstimatedPrice = EstimatedPrice,

                    LastModified = DateTime.Now
                };

                _projectRepository.Add(project);
                projects.Add(project);


                var contract = new Contract
                {
                    CaseNumber = CaseNumber,
                    OfferSent = OfferSent,
                    OfferApproved = OfferApproved,
                    Paid = Paid,
                    Price = Price
                };



                _contractRepository.Add(contract);
                contracts.Add(contract);

                NavigateToViewProject?.Invoke(project);


                MessageBox.Show("Projektet er oprettet!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl under oprettelse:\n{ex.Message}", "Teknisk fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


    }

}




                //foreach (var s in services)
                //{
                //    if (!existingServices.Any(es => es.Name == s.ServiceType))
                //    {
                //        existingServices.Add(new Services { Name = s.Name });
                //    }
                //}
                //FileService.Save("Data/services.json", existingServices);




