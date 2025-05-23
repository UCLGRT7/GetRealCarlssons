using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CarlssonsWPF.Model
{
    public class Customer
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }


        public override string ToString()
        {
            return $"{Id}{Name},{Address},{Email},{PhoneNumber},{PostalCode},{City}";
        }

        public static Customer FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 7)
                throw new FormatException("Invalid customer data format");
            return new Customer
            {
                Id = parts[0] != null ? Guid.Parse(parts[0]) : Guid.NewGuid(),
                Name = parts[1],
                Address = parts[2],
                Email = parts[3],
                PhoneNumber = int.Parse(parts[4]),
                PostalCode = int.Parse(parts[5]),
                City = parts[6]
            };
        }
    }
}
