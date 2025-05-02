using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.ViewModel.IRepositories;
using CarlssonsWPF.Data.FileRepositories;

namespace CarlssonsWPF.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IProjectRepository Projects { get; }
        IContractRepository Contracts { get; }
        IServicesRepository Services { get; }

        void Complete();
    }
}
