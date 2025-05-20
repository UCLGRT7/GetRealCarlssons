using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace CarlssonsWPF.Model
{
    // ServiceEntry er nu den centrale model, der indeholder både serviceinformationer og kompleksitet
    public class ServiceEntry : INotifyPropertyChanged
    {
        public static List<Service> AvailableServices { get; set; } = new List<Service>();

        private int _complexity;

        // Id for service (fra tidligere Service.cs)
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;

                    // Find matchende service fra den globale liste og opdater
                    var service = AvailableServices.FirstOrDefault(s => s.Id == _id);
                    if (service != null)
                    {
                        Name = service.Name;
                        Service = service;
                    }

                    OnPropertyChanged();
                }
            }
        }


        // Navn på servicen
        public string Name { get; set; }

        // Komplexitet af servicen
        public int Complexity
        {
            get => _complexity;
            set
            {
                if (_complexity != value)
                {
                    _complexity = value;
                    OnPropertyChanged();
                }
            }
        }

        // Optional reference til en Service, hvis det er nødvendigt
        // Hvis du ikke længere behøver 'Service', kan du fjerne dette felt
       
        private Service? _service;
        [JsonIgnore]
        public Service? Service
        {
            get => _service;
            set
            {
                _service = value;
                if (_service != null)
                {
                    Name = _service.Name; // <-- Dette sikrer korrekt navngivning
                    Id = _service.Id;     // (valgfrit, men sikrer konsistens)
                }
                OnPropertyChanged();
            }
        }

        // ToString metode for nemt at konvertere ServiceEntry til en streng
        public override string ToString()
        {
            return $"{Id},{Name},{Complexity}";
        }

        // Methode til at oprette en ServiceEntry fra en streng (kan bruges til filindlæsning)
        public static ServiceEntry FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 3)
                throw new FormatException("Invalid service data format");

            return new ServiceEntry
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Complexity = int.Parse(parts[2])
            };
        }

        // INotifyPropertyChanged implementering for at give besked om ændringer
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Klasse til at gemme service-informationer
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
