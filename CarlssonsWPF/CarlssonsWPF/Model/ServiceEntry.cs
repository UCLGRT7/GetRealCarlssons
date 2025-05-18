using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarlssonsWPF.Model
{
    // ServiceEntry er nu den centrale model, der indeholder både serviceinformationer og kompleksitet
    public class ServiceEntry : INotifyPropertyChanged
    {
        private int _complexity;

        // Id for service (fra tidligere Service.cs)
        public int Id { get; set; }

        // Navn på servicen
        public string? Name { get; set; }

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
        public Service? Service { get; set; }

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
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Klasse til at gemme service-informationer
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
