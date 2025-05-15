
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CarlssonsWPF.Model;
using CarlssonsWPF.Service;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Data.FileRepositories;


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


        // 5 ydelser fra brugeren
        public ObservableCollection<Services> Services { get; set; } = new();
        public string? SelectedCustomer { get; set; }
        public string? CaseNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? Deadline { get; set; }
        public int? Scope { get; set; }

        public DateTime? OfferSent { get; set; }
        public DateTime? OfferConfirmed { get; set; }
        public DateTime? PaymentRecieved { get; set; }
        public double Price { get; set; }
        public string? Paid { get; set; }

        public ICommand CreateProjectCommand { get; set; }
        public ICommand CancelCommand { get; set; }

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
            // Initialiser 5 ydelser og tilføj PropertyChanged-handler
            for (int i = 0; i < 5; i++)
            {
                var entry = new Services();
                UpdateEstimatedPrice(entry);
                services.Add(entry);
            }

            CreateProjectCommand = new RelayCommand(_ => CreateProject());
        }
        private void UpdateEstimatedPrice(Services entry)
        {
            if (Scope == null)
                return;

            int totalComplexity = services.Sum(s => s.Complexity);
            EstimatedPrice = Scope.Value * totalComplexity * P;
        }

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


        public void CreateProject()
        {


            // Sikrer stabile værdier, således at >>null<< ikke runtime crasher.
            int scopeValue = Scope ?? 0;
            DateTime deadlineValue = Deadline ?? DateTime.Today;
            DateTime offerSentValue = OfferSent ?? DateTime.MinValue;
            DateTime offerApprovedValue = OfferConfirmed ?? DateTime.MinValue;
            DateTime paymentReceivedValue = PaymentRecieved ?? DateTime.MinValue;

            var project = new Project
            {

                CustomerName = SelectedCustomer,
                CaseNumber = CaseNumber,
                ProjectAddress = Address,
                Deadline = ParsedDeadline ?? DateTime.Today,
                Scope = scopeValue,
                ServiceEntry = services.ToList(),
                EstimatedPrice = EstimatedPrice ?? 0,
                LastModified = DateTime.Now
            };

            _projectRepository.Add(project);
            projects.Add(project);


            var contract = new Contract
            {
                CaseNumber = CaseNumber,
                OfferSent = OfferSent,
                OfferApproved = OfferConfirmed,
                Paid = PaymentRecieved,
                Price = Price
            };



            _contractRepository.Add(contract);
            contracts.Add(contract);


            //foreach (var s in services)
            //{
            //    if (!existingServices.Any(es => es.ServiceEntry == s.ServiceType))
            //    {
            //        existingServices.Add(new Services { ServiceEntry = s.ServiceEntry });
            //    }
            //}
            //FileService.Save("Data/services.json", existingServices);
        }

    }
}
