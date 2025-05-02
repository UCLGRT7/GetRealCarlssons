using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public class ContractFileRepository : FileRepository<Contract>, IContractRepository
    {
        public ContractFileRepository() : base("contracts.json") { }

        public override Contract GetById(object id)
        {
            int invoiceNumber = Convert.ToInt32(id);
            return _entities.FirstOrDefault(c => c.InvoiceNumber == invoiceNumber);
        }

        public Contract GetByInvoiceNumber(int invoiceNumber)
        {
            return _entities.FirstOrDefault(c => c.InvoiceNumber == invoiceNumber);
        }

        public IEnumerable<Contract> GetByProjectId(string projectId)
        {
            // This would require Contract to have a ProjectId property
            // Adding this property to the model is recommended
            return _entities.Where(c => c.ProjectId == projectId);
        }

        public override void Delete(object id)
        {
            int invoiceNumber = Convert.ToInt32(id);
            var contract = GetById(invoiceNumber);
            if (contract != null)
            {
                _entities.Remove(contract);
            }
        }

        public override void Update(Contract entity)
        {
            var existingContract = GetById(entity.InvoiceNumber);
            if (existingContract != null)
            {
                int index = _entities.IndexOf(existingContract);
                _entities[index] = entity;
            }
        }
    }
}
