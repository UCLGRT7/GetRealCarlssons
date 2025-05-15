using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface IContractRepository 
    {
        Contract GetByInvoiceNumber(int invoiceNumber);
        IEnumerable<Contract> GetAll();
        IEnumerable<Contract> GetByProjectId(string projectId);
        void Add(Contract contract);
        void Update(Contract contract);
        void Delete(int invoiceNumber);
        void SaveAllOnExit();

    }
}
