using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    class AddCustomerViewModel : INotifyPropertyChanged
    {
        public Customer SelectedCustomer { get; set; }

        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;

        public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Project> projects { get; set; } = new ObservableCollection<Project>();
        public ObservableCollection<Contract> contracts { get; set; } = new ObservableCollection<Contract>();

        public event PropertyChangedEventHandler PropertyChanged;

        private string? _name;
        private string? _address;
        private string? _email;
        private string? _phoneNumber;
        private string? _postalCode;
        private string? _city;

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string? Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public string? PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }
        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AddCustomerViewModel()
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();

            // Indlæs eksisterende kunder
            foreach (var customer in _customerRepository.GetAll())
            {
                customers.Add(customer);
            }

        }

        public RelayCommand AddCustomerCommand => new RelayCommand(execute => AddCustomer());


        public void AddCustomer()
        {
            if (!int.TryParse(PhoneNumber, out int parsedPhone))
                parsedPhone = 0;

            if (!int.TryParse(PostalCode, out int parsedPostal))
                parsedPostal = 0;
            var customer = new Customer
            {
                Name = Name,
                Address = Address,
                PostalCode = parsedPostal,
                City = City,
                PhoneNumber = parsedPhone,
                Email = Email
            };

            _customerRepository.Add(customer); // Gem til fil
            customers.Add(customer);           // Vis i UI

            MessageBox.Show($"Kunde '{customer.Name}' tilføjet", "Success");
        }
        public void AddCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
            customers.Add(customer);
        }
    }
}
