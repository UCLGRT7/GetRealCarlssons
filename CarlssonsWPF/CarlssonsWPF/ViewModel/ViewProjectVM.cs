﻿using System;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CarlssonsWPF.ViewModel
{
    public class ViewProjectVM : INotifyPropertyChanged, IReloadableViewModel
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

        public ObservableCollection<string> StatusOptions { get; } = new ObservableCollection<string> { "Forsinket", "Igang", "Færdig", "Afventer" };

        public void LoadData()
        {
            // Genindlæs data her (f.eks. fra repository)
        }


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
        public string EditButtonText => IsEditing ? "Bekræft" : "Redigér";
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

        public ViewProjectVM(Project selectedProject, ServiceEntry selectedService)
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
            SelectedProject.Customer = Customers.FirstOrDefault(c => c.Name == SelectedProject.CustomerName);


            if (selectedService != null)
            {
                var existingService = Services.FirstOrDefault(s => s.Id == selectedService.Id);
                if (existingService != null)
                {
                    SelectedProject.Services.Add(existingService);
                }
            }

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

            AddServiceCommand = new RelayCommand(_ => AddService(), _ => SelectedProject.Services.Count < 10);
            RemoveServiceCommand = new RelayCommand(RemoveService);
            ToggleEditCommand = new RelayCommand(ToggleEdit);
            

        }
        private int _invoiceNumber;
        public int InvoiceNumber
        {
            get => _invoiceNumber;
            set
            {
                if (_invoiceNumber != value)
                {
                    _invoiceNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _offerApprovedInput;
        public string OfferApprovedInput
        {
            get => _offerApprovedInput;
            set
            {
                if (_offerApprovedInput != value)
                {
                    _offerApprovedInput = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _offerSentInput;
        public string OfferSentInput
        {
            get => _offerSentInput;
            set
            {
                if (_offerSentInput != value)
                {
                    _offerSentInput = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _paidInput;
        public string PaidInput
        {
            get => _paidInput;
            set
            {
                if (_paidInput != value)
                {
                    _paidInput = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? ParseDate(string input)
        {
            if (DateTime.TryParse(input, out var date))
                return date;
            return null;
        }
        private void ToggleEdit()
        {
            if (IsEditing)
            {
                // Opdater alle ServiceEntry-felter ud fra Id
                foreach (var entry in SelectedProject.Services)
                {
                    var match = Services.FirstOrDefault(s => s.Id == entry.Id);
                    if (match != null)
                    {
                        entry.Name = match.Name;
                        entry.Id = match.Id;

                        entry.Service = new Service
                        {
                            Id = match.Id,
                            Name = match.Name
                        };
                    }
                }
                var contract = _contractRepository.GetByProjectId(SelectedProject.CaseNumber).FirstOrDefault();
                if (contract != null)
                {
                    // Opdater kontraktens felter baseret på UI bindingsfelter
                    contract.InvoiceNumber = InvoiceNumber;
                    contract.OfferSent = ParseDate(OfferSentInput);
                    contract.OfferApproved = ParseDate(OfferApprovedInput);
                    contract.Paid = ParseDate(PaidInput);
                    contract.Price = SelectedProject.Price;

                    // Gem kontrakten
                    _contractRepository.Update(contract);

                    // Opdater Contracts ObservableCollection
                    var index = Contracts.IndexOf(contract);
                    if (index >= 0)
                    {
                        Contracts[index] = contract;
                        OnPropertyChanged(nameof(Contracts));
                    }
                }
                else
                {
                    // Hvis der ikke findes en kontrakt, kan du evt. oprette en ny her
                }
            

            // Opdater sidste redigeringstidspunkt
            SelectedProject.LastModified = DateTime.Now;

                // Gem ændringer til fil
                _projectRepository.Update(SelectedProject);
            }

            // 10 linjer i Services
            while (SelectedProject.Services.Count < 10)
            {
                SelectedProject.Services.Add(new ServiceEntry());
            }

            // Skift redigeringstilstand
            IsEditing = !IsEditing;

            // Notificér UI
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
