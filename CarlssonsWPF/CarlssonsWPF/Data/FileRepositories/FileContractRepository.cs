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
    public class FileContractRepository : FileRepository<Contract>, IContractRepository
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");
        private static string contractFilePath = Path.Combine(subFolder, "contracts.json");
        public string FilePath { get; set; }


        public FileContractRepository(string? filePath = null) : base("contracts.json") 
        {
            FilePath = filePath ?? contractFilePath;

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
