﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public class FileServiceRepository : IServiceRepository
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

        public ServiceEntry GetById(int id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<ServiceEntry> GetAll()
        {
            List<ServiceEntry> services = new List<ServiceEntry>();

            try
            {
                if (new FileInfo(FilePath).Length > 0)
                {
                    string jsonContent = File.ReadAllText(FilePath);

                    // If the file isn't empty, deserialize it
                    if (!string.IsNullOrWhiteSpace(jsonContent))
                    {
                        services = JsonSerializer.Deserialize<List<ServiceEntry>>(jsonContent) ?? new List<ServiceEntry>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading service file: {ex.Message}");
            }

            return services;
        }

        public IEnumerable<ServiceEntry> GetByServiceEntry(string serviceEntry)
        {
            return GetAll().Where(s => s.Name.Contains(s.Name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ServiceEntry> GetByComplexity(int complexity)
        {
            return GetAll().Where(s => s.Complexity == complexity);
        }

        public void Add(ServiceEntry service)
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

        public void Update(ServiceEntry service)
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

        private void SaveServicesToFile(IEnumerable<ServiceEntry> services)
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
