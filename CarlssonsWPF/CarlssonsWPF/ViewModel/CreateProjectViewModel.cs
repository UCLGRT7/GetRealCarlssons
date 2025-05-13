
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CarlssonsWPF.Model;
using CarlssonsWPF.Service;
using CarlssonsWPF.ViewModel;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel
{
    public class CreateProjectViewModel : BaseViewModel
    {
        public ObservableCollection<string> Customers { get; set; }
        public ObservableCollection<Services> Services { get; set; } = new ObservableCollection<Services>();

        public string SelectedCustomer { get; set; }
        public string CaseNumber { get; set; }
        public string Address { get; set; }
        public DateTime Deadline { get; set; }
        public int Scope { get; set; }

        public string OfferSent { get; set; }
        public string OfferApproved { get; set; }
        public double Price { get; set; }
        public string Paid { get; set; }

        public ICommand CreateProjectCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateProjectViewModel()
        {
            Customers = new ObservableCollection<string>(
                FileService.Load<Customer>("Data/customers.json").Select(c => c.Name)
            );

            for (int i = 0; i < 5; i++)
            {
                Services.Add(new Services());
            }

            CreateProjectCommand = new RelayCommand(CreateProject);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void CreateProject()
        {
            var project = new Project
            {
                CustomerName = SelectedCustomer,
                CaseNumber = CaseNumber,
                ProjectAddress = Address,
                Deadline = Deadline,
                Scope = Scope,
                Services = Services.ToList(),
                EstimatedPrice = Scope * Services.Sum(s => s.Complexity),
                Price = Price,
                Paid = Paid
            };

            var projects = FileService.Load<Project>("Data/projects.json");
            projects.Add(project);
            FileService.Save("Data/projects.json", projects);

            var contract = new Contract
            {
                CaseNumber = CaseNumber,
                OfferSent = DateTime.TryParse(OfferSent, out var offerSentDate) ? offerSentDate : (DateTime?)null,
                OfferConfirmed = DateTime.TryParse(OfferApproved, out var offerConfirmedDate) ? offerConfirmedDate : (DateTime?)null,
                PaymentReceivedDate = DateTime.TryParse(Paid, out var paidDate) ? paidDate : (DateTime?)null,
                Price = project.Price
            };


            var existingServices = FileService.Load<Services>("Data/services.json");
            foreach (var s in Services)
            {
                if (!existingServices.Any(es => es.ServiceType == s.ServiceType))
                {
                    existingServices.Add(new Services { ServiceType = s.ServiceType });
                }
            }
            FileService.Save("Data/services.json", existingServices);
        }

        private void Cancel()
        {
            // Navigation tilbage til MainViewModel (kan implementeres via event eller navigation service)
        }
    }
}
