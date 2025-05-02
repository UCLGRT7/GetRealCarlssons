using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public class CustomerFileRepository : FileRepository<Customer>, ICustomerRepository
    {
        public CustomerFileRepository() : base("customers.json") { }

        public override Customer GetById(object id)
        {
            string customerId = id.ToString();
            return _entities.FirstOrDefault(c => c.Name == customerId); 
        }

        public Customer GetByEmail(string email)
        {
            return _entities.FirstOrDefault(c => c.Email == email);
        }

        public IEnumerable<Customer> GetByName(string name)
        {
            return _entities.Where(c => c.Name.Contains(name));
        }

        public override void Delete(object id)
        {
            string customerId = id.ToString();
            var customer = GetById(customerId);
            if (customer != null)
            {
                _entities.Remove(customer);
            }
        }

        public override void Update(Customer entity)
        {
            var existingCustomer = GetById(entity.Name);
            if (existingCustomer != null)
            {
                int index = _entities.IndexOf(existingCustomer);
                _entities[index] = entity;
            }
        }
    }
}
