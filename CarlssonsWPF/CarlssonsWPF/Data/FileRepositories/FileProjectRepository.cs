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
    public class FileProjectRepository : FileRepository<Project>, IProjectRepository
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");
        private static string projectFilePath = Path.Combine(subFolder, "projects.json");
        public string FilePath { get; set; }

        public FileProjectRepository(string? filePath = null) : base("projects.json") 
        {
            FilePath = filePath ?? projectFilePath;

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

        public override Project GetById(object id)
        {
            string projectId = id.ToString();
            return _entities.FirstOrDefault(p => p.CaseNumber == projectId);
        }

        public Project GetByCaseNumber(string caseNumber)
        {
            return _entities.FirstOrDefault(p => p.CaseNumber == caseNumber);
        }

        public IEnumerable<Project> GetByAddress(string address)
        {
            return _entities.Where(p => p.AddressOfDelivery.Contains(address));
        }

        public IEnumerable<Project> GetByScale(int scale)
        {
            return _entities.Where(p => p.Scale == scale);
        }

        public IEnumerable<Project> GetByCustomerId(string customerId)
        {
            // This would require Project to have a CustomerId property
            // Adding this property to the model is recommended
            return _entities.Where(p => p.CustomerId == customerId);
        }

        public override void Delete(object id)
        {
            string projectId = id.ToString();
            var project = GetById(projectId);
            if (project != null)
            {
                _entities.Remove(project);
            }
        }

        public override void Update(Project entity)
        {
            var existingProject = GetById(entity.CaseNumber);
            if (existingProject != null)
            {
                int index = _entities.IndexOf(existingProject);
                _entities[index] = entity;
            }
        }
    }
}
