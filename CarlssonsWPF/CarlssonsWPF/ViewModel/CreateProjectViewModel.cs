
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
        private double estimatedPrice;

        public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Project> projects { get; set; } = new ObservableCollection<Project>();
        public ObservableCollection<Contract> contracts { get; set; } = new ObservableCollection<Contract>();
        public ObservableCollection<Services> services { get; set; } = new ObservableCollection<Services>();

        // 5 ydelser fra brugeren


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
            // Initialiser 5 tomme ydelser
            for (int i = 0; i < 5; i++)
                services.Add(new Services());

            CreateProjectCommand = new RelayCommand(_ => CreateProject());
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
                Deadline = deadlineValue,
                Scope = scopeValue,
                Services = services.ToList(),
                EstimatedPrice = estimatedPrice,
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
            //    if (!existingServices.Any(es => es.ServiceType == s.ServiceType))
            //    {
            //        existingServices.Add(new Services { ServiceType = s.ServiceType });
            //    }
            //}
            //FileService.Save("Data/services.json", existingServices);
        }

    }
}
