using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public class FileCustomerRepository : FileRepository<Customer>, ICustomerRepository
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");
        private static string customersFilePath = Path.Combine(subFolder, "customers.json");
        public string FilePath { get; set; }

        public FileCustomerRepository(string? filePath = null) : base("customers.json")
        {
            FilePath = filePath ?? customersFilePath;

            string directory = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create file if it doesn't exist
            if (!File.Exists(FilePath))
            {
                // Create an empty JSON array
                File.WriteAllText(FilePath, "[]");
            }
        }

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
