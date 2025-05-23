﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CarlssonsWPF.Model
{
    // ServiceEntry er nu den centrale model, der indeholder både serviceinformationer og kompleksitet
    public class ServiceEntry
    {
        public static List<Service> AvailableServices { get; set; } = new List<Service>();

        public int Id { get; set; }
        public string? Name { get; set; }
        public int Complexity { get; set; }

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
                    Name = _service.Name; 
                    Id = _service.Id;     
                }
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
    }

    // Klasse til at gemme service-informationer
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
