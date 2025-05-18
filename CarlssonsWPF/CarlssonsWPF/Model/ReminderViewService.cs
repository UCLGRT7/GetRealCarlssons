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

                if (contract == null || !contract.OfferApproved.HasValue || !contract.Paid.HasValue)
                {
                    var viewModel = new RemindersData
                    {
                        CaseNumber = project.CaseNumber,
                        CustomerName = project.CustomerName,
                        OfferSentDate = contract?.OfferSent,
                        Deadline = project.Deadline,
                        OfferApproved = contract != null && contract.OfferApproved.HasValue ? "Yes" : "No",
                        IsPaymentRecieved = contract != null && contract.Paid.HasValue ? "Yes" : "No"
                    };

                    remindersList.Add(viewModel);
                }
            }
            return remindersList;
        }


        public ObservableCollection<RemindersData> GetExeededby14Days()
        {
            var exceededList = new ObservableCollection<RemindersData>();

            var projects = _projectRepository.GetAll();

            foreach (var project in projects)
            {
                var customer = _customerRepository.GetByName(project.CustomerName);
                var contract = _contractRepository.GetByProjectId(project.CaseNumber).FirstOrDefault();

                if (contract?.OfferSent.HasValue == true)
                {
                    TimeSpan timeSinceOffer = DateTime.Now - contract.OfferSent.Value;

                    if ((!contract.OfferApproved.HasValue || !contract.Paid.HasValue) && timeSinceOffer.TotalDays > 14)
                    {
                        var viewModel = new RemindersData
                        {
                            CaseNumber = project.CaseNumber,
                            CustomerName = project.CustomerName,
                            OfferSentDate = contract.OfferSent,
                            DaysPassed = (int)timeSinceOffer.TotalDays,
                            OfferApproved = contract.OfferApproved.HasValue ? "Yes" : "No",
                            IsPaymentRecieved = contract.Paid.HasValue ? "Yes" : "No"
                        };

                        exceededList.Add(viewModel);
                    }
                }
            }
            return exceededList;
        }
    }


}
