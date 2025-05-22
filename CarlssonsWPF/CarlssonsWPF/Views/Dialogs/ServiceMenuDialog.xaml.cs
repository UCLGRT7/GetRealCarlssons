using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.Views.Dialogs
{
    public partial class ServiceMenuDialog : Window
    {
        private List<ServiceEntry> services = new();


        private readonly string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory[..AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")],
            "Data", "SavedFiles", "services.json"
        );



        public ServiceMenuDialog()
        {
            InitializeComponent();
            LoadServices();
        }

        private void LoadServices()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                services = JsonSerializer.Deserialize<List<ServiceEntry>>(json) ?? new();
                ServiceComboBox.ItemsSource = services;
                ServiceComboBox.SelectedIndex = -1;
            }

            ServiceComboBox.SelectionChanged += (s, e) =>
            {
                DeleteButton.IsEnabled = ServiceComboBox.SelectedItem != null;
            };
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceComboBox.SelectedItem is ServiceEntry selected)
            {
                var result = MessageBox.Show($"Bekræft sletning af \"{selected.Name}\"", "Sletning af Ydelse", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    services.RemoveAll(s => s.Name == selected.Name);
                    SaveServices();
                    RefreshComboBox();

                    MessageBox.Show($"\"{selected.Name}\" er slettet.", "Ydelse slettet");
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string newName = NewServiceTextBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Indtast et navn på ydelsen.");
                return;
            }

            if (services.Any(s => s.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ydelsen findes allerede.");
                return;
            }

            int newId = services.Count > 0 ? services.Max(s => s.Id) + 1 : 1;
            services.Add(new ServiceEntry { Id = newId, Name = newName });

            SaveServices();
            RefreshComboBox();

            MessageBox.Show($"\"{newName}\" er tilføjet.", "Ydelse tilføjet");

            NewServiceTextBox.Clear();
        }


        private void SaveServices()
        {
            string updatedJson = JsonSerializer.Serialize(services, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);
        }




        private void RefreshComboBox()
        {
            ServiceComboBox.ItemsSource = null;
            ServiceComboBox.ItemsSource = services;
            ServiceComboBox.SelectedIndex = -1;
            DeleteButton.IsEnabled = false;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
