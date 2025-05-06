using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.ViewModel
{
    public class ProjektMainPageViewModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;

        public ObservableCollection<Customer> customers { get; set; } = new ObservableCollection<Customer>();
        public ObservableCollection<Project> projects { get; set; } = new ObservableCollection<Project>();
        public ObservableCollection<Contract> contracts { get; set; } = new ObservableCollection<Contract>();

        public ObservableCollection<object> combined { get; set; } = new ObservableCollection<object>();


        public ProjektMainPageViewModel()
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();
            PopulateCombinedList();
        }
        public void PopulateCombinedList()
        {
            combined.Clear();

            foreach (var customer in customers)
            {
                combined.Add(customer.Name);
            }

            foreach (var project in projects)
            {
                var matchingCustomer = customers.FirstOrDefault(customer => customer.Name == project.CustomerName);
                if (matchingCustomer != null)
                {
                    combined.Add(project.CaseNumber);
                }
            }

            foreach (var contract in contracts)
            {
                var matchingProject = projects.FirstOrDefault(project => project.CaseNumber == contract.CaseNumber);
                combined.Add(new { Type = "Contract", InvoiceNumber = contract.InvoiceNumber });
            }
        }



    }
}
