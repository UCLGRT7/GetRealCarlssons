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
    public class FileServiceRepository :  IServiceRepository
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");
        private static string servicesFilePath = Path.Combine(subFolder, "services.json");
        public string FilePath { get; set; }

        public FileServiceRepository(string? filePath = null)
        {
            FilePath = filePath ?? servicesFilePath;

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

        public Services GetById(int id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Services> GetAll()
        {
            List<Services> services = new List<Services>();

            try
            {
                if (new FileInfo(FilePath).Length > 0)
                {
                    string jsonContent = File.ReadAllText(FilePath);

                    // If the file isn't empty, deserialize it
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        services = JsonSerializer.Deserialize<List<Services>>(jsonContent) ?? new List<Services>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading service file: {ex.Message}");
            }

            return services;
        }

        public IEnumerable<Services> GetByServiceEntry(string serviceEntry)
        {
            return GetAll().Where(s => s.ServiceEntry.Contains(serviceEntry, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Services> GetByComplexity(int complexity)
        {
            return GetAll().Where(s => s.Complexity == complexity);
        }

        public void Add(Services service)
        {
            // If service already exists, update it instead
            if (GetById(service.Id) != null)
            {
                Update(service);
                return;
            }

            try
            {
                // Get all existing services
                var services = GetAll().ToList();

                // Add the new service
                services.Add(service);

                // Save all services back to file
                SaveServicesToFile(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding service: {ex.Message}");
            }
        }

        public void Update(Services service)
        {
            // Get all services
            var services = GetAll().ToList();

            // Find the index of the service to update
            var existingIndex = services.FindIndex(s => s.Id == service.Id);

            // Update the service if found
            if (existingIndex != -1)
            {
                services[existingIndex] = service;
                SaveServicesToFile(services);
            }
        }

        public void Delete(int id)
        {
            var services = GetAll().ToList();
            services.RemoveAll(s => s.Id == id);
            SaveServicesToFile(services);
        }

        public void SaveAllOnExit()
        {
            var services = GetAll().ToList();
            SaveServicesToFile(services);
        }

        private void SaveServicesToFile(IEnumerable<Services> services)
        {
            try
            {
                // Configure JSON serialization options
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // Makes the JSON file more readable
                };

                // Serialize the service list to JSON
                string jsonString = JsonSerializer.Serialize(services, options);

                // Write to file
                File.WriteAllText(FilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving services: {ex.Message}");
            }
        }
    }
}
