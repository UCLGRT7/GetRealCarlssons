using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface IContractRepository : IRepository<Contract>
    {
        Contract GetByInvoiceNumber(int invoiceNumber);
        IEnumerable<Contract> GetByProjectId(string projectId);
    }
}
