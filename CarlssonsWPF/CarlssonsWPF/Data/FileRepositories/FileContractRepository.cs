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
    public class FileContractRepository :  IContractRepository
    {


        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");


        private static string contractFilePath = Path.Combine(subFolder, "contracts.json");

        public string FilePath { get; set; }


        public FileContractRepository(string? filePath = null)
        {
            FilePath = filePath ?? contractFilePath;

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

        public Contract GetByInvoiceNumber(int invoiceNumber)
        {
            return GetAll().FirstOrDefault(c => c.InvoiceNumber == invoiceNumber);
        }

        public IEnumerable<Contract> GetAll()
        {
            List<Contract> contracts = new List<Contract>();

            try
            {
                if (new FileInfo(FilePath).Length > 0)
                {
                    string jsonContent = File.ReadAllText(FilePath);

                    // If the file isn't empty, deserialize it
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        contracts = JsonSerializer.Deserialize<List<Contract>>(jsonContent) ?? new List<Contract>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading contract file: {ex.Message}");
            }

            return contracts;
        }

        public IEnumerable<Contract> GetByProjectId(string projectId)
        {
            return GetAll().Where(c => c.CaseNumber == projectId);
        }

        public void Add(Contract contract)
        {
            // If contract already exists, update it instead
            if (GetByInvoiceNumber(contract.InvoiceNumber) != null)
            {
                Update(contract);
                return;
            }

            try
            {
                // Get all existing contracts
                var contracts = GetAll().ToList();

                // Add the new contract
                contracts.Add(contract);

                // Save all contracts back to file
                SaveContractsToFile(contracts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding contract: {ex.Message}");
            }
        }

        public void Update(Contract contract)
        {
            // Get all contracts
            var contracts = GetAll().ToList();

            // Find the index of the contract to update
            var existingIndex = contracts.FindIndex(c => c.Id == contract.Id);

            // Update the contract if found
            if (existingIndex != -1)
            {
                contracts[existingIndex] = contract;
                SaveContractsToFile(contracts);
            }
        }

        public void Delete(int invoiceNumber)
        {
            var contracts = GetAll().ToList();
            contracts.RemoveAll(c => c.InvoiceNumber == invoiceNumber);
            SaveContractsToFile(contracts);
        }

        public void SaveAllOnExit()
        {
            var contracts = GetAll().ToList();
            SaveContractsToFile(contracts);
        }

        private void SaveContractsToFile(IEnumerable<Contract> contracts)
        {
            try
            {
                // Configure JSON serialization options
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // Makes the JSON file more readable
                };

                // Serialize the contract list to JSON
                string jsonString = JsonSerializer.Serialize(contracts, options);

                // Write to file
                File.WriteAllText(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving contracts: {ex.Message}");
            }
        }
    }
}
