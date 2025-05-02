using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;


namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface ICustomerRepository
    {

        Customer GetByName(string name);
        Customer GetByEmail(string email);
        IEnumerable<Customer> GetAll();
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int invoiceNumber);

    }
}
