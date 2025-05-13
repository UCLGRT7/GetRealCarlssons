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
    public class FileProjectRepository :  IProjectRepository
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");
        private static string projectFilePath = Path.Combine(subFolder, "projects.json");
        public string FilePath { get; set; }

        public FileProjectRepository(string? filePath = null)
        {
            FilePath = filePath ?? projectFilePath;

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

        public Project GetByCaseNumber(string caseNumber)
        {
            return GetAll().FirstOrDefault(p => p.CaseNumber == caseNumber);
        }

        public IEnumerable<Project> GetAll()
        {
            List<Project> projects = new List<Project>();

            try
            {
                if (new FileInfo(FilePath).Length > 0)
                {
                    string jsonContent = File.ReadAllText(FilePath);

                    // If the file isn't empty, deserialize it
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        projects = JsonSerializer.Deserialize<List<Project>>(jsonContent) ?? new List<Project>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading project file: {ex.Message}");
            }

            return projects;
        }

        public IEnumerable<Project> GetByCustomerId(string customerName)
        {
            return GetAll().Where(p => p.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Project> GetByAddress(string address)
        {
            return GetAll().Where(p => p.ProjectAddress.Contains(address, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Project> GetByScale(int scale)
        {
            return GetAll().Where(p => p.Scope == scale);
        }

        public void Add(Project project)
        {
            // If project already exists, update it instead
            if (GetByCaseNumber(project.CaseNumber) != null)
            {
                Update(project);
                return;
            }

            try
            {
                // Get all existing projects
                var projects = GetAll().ToList();

                // Add the new project
                project.LastModified = DateTime.Now;
                projects.Add(project);

                // Save all projects back to file
                SaveProjectsToFile(projects);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding project: {ex.Message}");
            }
        }

        public void Update(Project project)
        {
            // Get all projects
            var projects = GetAll().ToList();

            // Find the index of the project to update
            var existingIndex = projects.FindIndex(p => p.CaseNumber == project.CaseNumber);

            // Update the project if found
            if (existingIndex != -1)
            {
                projects[existingIndex] = project;
                project.LastModified = DateTime.Now;
                SaveProjectsToFile(projects);
            }
        }

        public void Delete(string caseNumber)
        {
            var projects = GetAll().ToList();
            projects.RemoveAll(p => p.CaseNumber == caseNumber);
            SaveProjectsToFile(projects);
        }

        private void SaveProjectsToFile(IEnumerable<Project> projects)
        {
            try
            {
                // Configure JSON serialization options
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // Makes the JSON file more readable
                };

                // Serialize the project list to JSON
                string jsonString = JsonSerializer.Serialize(projects, options);

                // Write to file
                File.WriteAllText(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving projects: {ex.Message}");
            }
        }
    }
}
