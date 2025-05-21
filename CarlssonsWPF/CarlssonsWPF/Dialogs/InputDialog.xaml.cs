using System.Linq;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using CarlssonsWPF.Model;
using System.Windows;



namespace CarlssonsWPF.Dialogs
{
    public partial class InputDialog : Window
    {
        public InputDialog()
        {
            InitializeComponent();
        }
        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            string newName = AddServiceInput.Text.Trim();
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Indtast en ydelse først.");
                return;
            }

            string filePath = "Data/services.json";
            List<Service> services = new();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                services = JsonSerializer.Deserialize<List<Service>>(json);
            }

            int newId = services.Any() ? services.Max(s => s.Id) + 1 : 1;

            var newService = new Service
            {
                Id = newId,
                Name = newName
            };

            services.Add(newService);

            string updatedJson = JsonSerializer.Serialize(services, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);

            // Opdater også den globale liste
            ServiceEntry.AvailableServices = services;

            MessageBox.Show("Ydelsen er gemt.");
            AddServiceInput.Clear();
        }
    }
}
