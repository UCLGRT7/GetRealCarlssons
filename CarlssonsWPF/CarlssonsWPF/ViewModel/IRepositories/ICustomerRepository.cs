using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;


namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
        IEnumerable<Customer> GetByName(string name);
    }
}
