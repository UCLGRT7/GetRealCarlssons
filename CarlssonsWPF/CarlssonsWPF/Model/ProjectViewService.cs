using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Model
{
    public class ProjectViewService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IContractRepository _contractRepository;

        public ProjectViewService()
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();
        }

        public ObservableCollection<CombinedProjectData> GetCombinedProjectModels()
        {
            var combinedList = new ObservableCollection<CombinedProjectData>();

            var projects = _projectRepository.GetAll();

            foreach (var project in projects)
            {
                var customer = _customerRepository.GetByName(project.CustomerName);
                var contract = _contractRepository.GetByProjectId(project.CaseNumber).FirstOrDefault();

                DateTime lastEdited = DateTime.MinValue;
                if (contract != null)
                {
                    var dates = new[]
                    {
                contract.OfferSent,
                contract.OfferApproved,
                contract.Paid
                    }
                    .Where(d => d.HasValue)
                    .Select(d => d.Value);

                    lastEdited = dates.Any() ? dates.Max() : DateTime.MinValue;
                }

                var viewModel = new CombinedProjectData
                {
                    CaseNumber = project.CaseNumber,
                    CustomerName = project.CustomerName,
                    LastModified = lastEdited,
                    Deadline = project.Deadline,
                    Status = project.Status,
                    OfferApproved = contract != null && contract.OfferApproved.HasValue ? "Yes" : "No",
                    IsPaymentRecieved = contract != null && contract.Paid.HasValue ? "Yes" : "No"
                };

                combinedList.Add(viewModel);
            }
            return combinedList;
        }
    }
}
