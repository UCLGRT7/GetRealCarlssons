﻿using System;
using System.Text.Json.Serialization;

namespace CarlssonsWPF.Model
{
    public class Contract
    {
        private Project project;

        public Guid Id { get; set; } = Guid.NewGuid();
        public int InvoiceNumber { get; set; }
        public DateTime? OfferSent { get; set; }
        public DateTime? Paid { get; set; }
        public DateTime? OfferApproved { get; set; }
        public double Price { get; set; }

        

        public string CaseNumber { get; set; } // Reference to Project


        [JsonIgnore]
        public Project Project { get => project; set => project = value; }

        public override string ToString()
        {
            return $"{InvoiceNumber},{OfferSent:yyyy-MM-dd},{Paid:yyyy-MM-dd},{OfferApproved:yyyy-MM-dd},{Price},{CaseNumber}";
        }

        public static Contract FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 6)
                throw new FormatException("Invalid contract data format");
            return new Contract
            {
                Id = parts[0] != null ? Guid.Parse(parts[0]) : Guid.NewGuid(),
                InvoiceNumber = int.Parse(parts[0]),
                OfferSent = DateTime.Parse(parts[1]),
                Paid = DateTime.Parse(parts[2]),
                OfferApproved = DateTime.Parse(parts[3]),
                Price = double.Parse(parts[4]),
                CaseNumber = parts[5]
            };
        }

    }
}
