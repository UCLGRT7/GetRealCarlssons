using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Data.FileRepositories;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.UnitOfWork
{
    public class FileUnitOfWork : IUnitOfWork
    {
        private readonly CustomerFileRepository _customerRepository;
        private readonly ProjectFileRepository _projectRepository;
        private readonly ContractFileRepository _contractRepository;
        private readonly ServicesFileRepository _servicesRepository;

        public FileUnitOfWork()
        {
            _customerRepository = new CustomerFileRepository();
            _projectRepository = new ProjectFileRepository();
            _contractRepository = new ContractFileRepository();
            _servicesRepository = new ServicesFileRepository();
        }

        public ICustomerRepository Customers => _customerRepository;
        public IProjectRepository Projects => _projectRepository;
        public IContractRepository Contracts => _contractRepository;
        public IServicesRepository Services => _servicesRepository;

        public void Complete()
        {
            _customerRepository.Save();
            _projectRepository.Save();
            _contractRepository.Save();
            _servicesRepository.Save();
        }

        public void Dispose()
        {
            // Release resources if needed
        }
    }
}
