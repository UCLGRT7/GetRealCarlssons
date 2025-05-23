using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    class KundeSearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly ICustomerRepository _customerRepository;
        private List<Customer> allCustomers;

        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();


        public RelayCommand SearchCommand { get; }

        public KundeSearchViewModel()
        {
            _customerRepository = new FileCustomerRepository();
            allCustomers = _customerRepository.GetAll().ToList();

            foreach (var c in allCustomers)
                Customers.Add(c);

            SearchCommand = new RelayCommand(_ => SearchCustomer());

        }
        private String? _name;
        private String? _address;
        private String? _email;
        private String? _phoneNumber;
        private String? _postalCode;
        private String? _city;
        public String? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public String? Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public String? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public String? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public String? PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }
        public String? City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public void SearchCustomer()
        {
            var filtered = allCustomers.Where(customer =>
                (string.IsNullOrWhiteSpace(Name) || customer.Name?.ToLower().Contains(Name.ToLower().Trim()) == true) &&
                (string.IsNullOrWhiteSpace(Address) || customer.Address?.ToLower().Contains(Address.ToLower().Trim()) == true) &&
                (string.IsNullOrWhiteSpace(Email) || customer.Email?.ToLower().Contains(Email.ToLower().Trim()) == true) &&
                (string.IsNullOrWhiteSpace(PhoneNumber) || customer.PhoneNumber.ToString().Contains(PhoneNumber.Trim())) &&
                (string.IsNullOrWhiteSpace(PostalCode) || customer.PostalCode.ToString().Contains(PostalCode.Trim())) &&
                (string.IsNullOrWhiteSpace(City) || customer.City?.ToLower().Contains(City.ToLower().Trim()) == true)
            ).ToList();

            Customers.Clear();
            foreach (var match in filtered)
                Customers.Add(match);
        }

    }
}
