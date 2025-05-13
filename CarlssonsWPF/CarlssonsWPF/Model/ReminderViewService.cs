using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Data.FileRepositories;
using System.Collections.ObjectModel;

namespace CarlssonsWPF.Model
{
    public class ReminderViewService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;

        public ReminderViewService()
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();
        }

        public ObservableCollection<RemindersData> GetRemindersData()
        {
            var remindersList = new ObservableCollection<RemindersData>();

            var projects = _projectRepository.GetAll();

            foreach (var project in projects)
            {
                var customer = _customerRepository.GetByName(project.CustomerName);
                var contract = _contractRepository.GetByProjectId(project.CaseNumber).FirstOrDefault();

                var viewModel = new RemindersData
                {
                    CaseNumber = project.CaseNumber,
                    CustomerName = project.CustomerName,
                    OfferSentDate = contract?.OfferSent,
                    DateOfDelivery = project.Deadline,
                    OfferConfirmed = contract != null && contract.OfferConfirmed.HasValue ? "Yes" : "No",
                    IsPaymentRecieved = contract != null && contract.PaymentReceivedDate.HasValue ? "Yes" : "No"
                };

                remindersList.Add(viewModel);
            }
            return remindersList;
        }
    }


}
