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
        private readonly FileCustomerRepository _customerRepository;
        private readonly FileProjectRepository _projectRepository;
        private readonly FileContractRepository _contractRepository;
        private readonly FileServiceRepository _servicesRepository;

        public FileUnitOfWork()
        {
            _customerRepository = new FileCustomerRepository();
            _projectRepository = new FileProjectRepository();
            _contractRepository = new FileContractRepository();
            _servicesRepository = new FileServiceRepository();
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

    }
}
