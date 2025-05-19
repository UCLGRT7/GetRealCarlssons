using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Data.FileRepositories;
using System.Windows;
using System.Windows.Navigation;


namespace CarlssonsWPF.ViewModel
{
    class CustomerSpecViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Customer SelectedCustomer { get; set; }

        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;


        public ObservableCollection<Customer> ustomers { get; set; } = new();
        public ObservableCollection<Project> projects { get; set; } = new();
        public ObservableCollection<Contract> contracts { get; set; } = new();

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



        public CustomerSpecViewModel(Customer selectedCustomer)
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

            //var result = MessageBox.Show($"Kunden '{SelectedCustomer.Name}' er opdateret!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

        }

            //var result = MessageBox.Show($"Kunden '{SelectedCustomer.Name}' er opdateret!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

        }


        public void ShowCustomerProjects()
        {
            var customerProjects = _projectRepository.GetByCustomerId(SelectedCustomer.Name.ToString());

            //int count = customerProjects?.Count() ?? 0;

            //MessageBox.Show(
            //  $"Der blev fundet {count} projekt(er) for kunden: {SelectedCustomer.Name}",
            //  "Projektstatus",
            //  MessageBoxButton.OK,
            //  MessageBoxImage.Information
            //);

            ProjectWithContractInfo.Clear(); 

            if (customerProjects != null)
            {
                foreach (var project in customerProjects)
                {
                    var contract = _contractRepository.GetByProjectId(project.CaseNumber).FirstOrDefault();

                    ProjectWithContractInfo.Add(new ProjectWithContractInfoDatagrid
                    {
                        CaseNumber = project.CaseNumber,
                        Deadline = project.Deadline,
                        Status = project.Status,
                        Price = contract?.Price ?? 0
                    });
                }
            }
        }
    }
}

 