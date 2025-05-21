using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public class FileCustomerRepository :  ICustomerRepository
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");

//#if DEBUG
//        private static string customerFilePath = Path.Combine(subFolder, "customers_test.json");
//#else
        private static string customerFilePath = Path.Combine(subFolder, "customers.json");
//#endif

        public string FilePath { get; set; }

        public FileCustomerRepository(string? filePath = null)
        {
            FilePath = filePath ?? customerFilePath;

            // Create directory if it doesn't exist
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

        public Customer GetByName(string name)
        {
 
            return GetAll().FirstOrDefault(c => c.Name != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));  
        }

        public Customer GetByEmail(string email)
        {
            return GetAll().FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                if (new FileInfo(FilePath).Length > 0)
                {
                    string jsonContent = File.ReadAllText(FilePath);

                    // If the file isn't empty, deserialize it
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        customers = JsonSerializer.Deserialize<List<Customer>>(jsonContent) ?? new List<Customer>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading customer file: {ex.Message}");
            }

            return customers;
        }

        public void Add(Customer customer)
        {
            // If customer already exists, update it instead
            if (GetByName(customer.Name) != null)
            {
                Update(customer);
                return;
            }

            try
            {
                // Get all existing customers
                var customers = GetAll().ToList();

                // Add the new customer
                customers.Add(customer);

                // Save all customers back to file
                SaveCustomersToFile(customers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
            }
        }

        public void Update(Customer customer)
        {
            // Get all customers
            var customers = GetAll().ToList();

            // Find the index of the customer to update
            var existingIndex = customers.FindIndex(c => c.Id == customer.Id);

            // Update the customer if found
            if (existingIndex != -1)
            {
                customers[existingIndex] = customer;
                SaveCustomersToFile(customers);
            }
        }

        public void Delete(int invoiceNumber)
        {
            // Note: Interface specifies invoiceNumber, but Customer uses Name as ID.
            // Assuming this is a typo in the interface; not implementing as it doesn't align with Customer's identifier.
            throw new NotImplementedException("Delete by invoiceNumber is not applicable for Customer. Use Name instead.");
        }

        public void Delete(string name)
        {
            var customers = GetAll().ToList();
            customers.RemoveAll(c => c.Name == name);
            SaveCustomersToFile(customers);
        }

        public void SaveAllOnExit()
        {
            var customers = GetAll().ToList();
            SaveCustomersToFile(customers);
        }

        private void SaveCustomersToFile(IEnumerable<Customer> customers)
        {
            try
            {
                // Configure JSON serialization options
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // Makes the JSON file more readable
                };

                // Serialize the customer list to JSON
                string jsonString = JsonSerializer.Serialize(customers, options);

                // Write to file
                File.WriteAllText(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving customers: {ex.Message}");
            }
        }
    }
}
