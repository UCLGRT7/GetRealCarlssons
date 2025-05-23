﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Data.FileRepositories;


namespace CarlssonsWPF.ViewModel
{
    class CustomerSpecVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public IProjectRepository ProjectRepository => _projectRepository;
        public Customer SelectedCustomer { get; set; }
        public int SelectedCustomerIndex { get; set; }

        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;


        public ObservableCollection<ProjectWithContractInfoDatagrid> ProjectWithContractInfo { get; set; } = new();


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand UpdateCustomerCommand { get; }

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



        public CustomerSpecVM(Customer selectedCustomer)
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();


            SelectedCustomer = selectedCustomer;
            SelectedCustomerIndex = _customerRepository.GetAll().ToList().IndexOf(selectedCustomer);


            SelectedCustomer = selectedCustomer;

            Name = selectedCustomer.Name;
            Address = selectedCustomer.Address;
            Email = selectedCustomer.Email;
            PhoneNumber = selectedCustomer.PhoneNumber.ToString();
            PostalCode = selectedCustomer.PostalCode.ToString();
            City = selectedCustomer.City;

            UpdateCustomerCommand = new RelayCommand(_ => UpdateCustomer());

            ShowCustomerProjects();

        }

        public void UpdateCustomer()
        {
            if (SelectedCustomer != null)
            {
                SelectedCustomer.Name = Name;
                SelectedCustomer.Address = Address;
                SelectedCustomer.Email = Email;
                SelectedCustomer.PhoneNumber = int.Parse(PhoneNumber);
                SelectedCustomer.PostalCode = int.Parse(PostalCode);
                SelectedCustomer.City = City;

                _customerRepository.Update(SelectedCustomer);

            }
        }

        public void ShowCustomerProjects()
        {
            var customerProjects = _projectRepository.GetByCustomerId(SelectedCustomer.Name.ToString());

            ProjectWithContractInfo.Clear(); 

            if (customerProjects != null)
            {
                foreach (var project in customerProjects)
                {
                    var contract = _contractRepository.GetByProjectId(project.CaseNumber).FirstOrDefault();

                    ProjectWithContractInfo.Add(new ProjectWithContractInfoDatagrid
                    {
                        CaseNumber = project.CaseNumber,
                        Deadline = project.Deadline ?? DateTime.MinValue,
                        Status = project.Status,
                        Price = contract?.Price ?? 0
                    });
                }
            }
        }
    }
}

 