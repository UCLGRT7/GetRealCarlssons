using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }

        [JsonIgnore]
        public List<Project> Projects { get; set; } = new List<Project>();

        public override string ToString()
        {
            return $"{Name},{Address},{Email},{PhoneNumber},{PostalCode},{City}";
        }

        public static Customer FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 6)
                throw new FormatException("Invalid customer data format");
            return new Customer
            {
                Name = parts[0],
                Address = parts[1],
                Email = parts[2],
                PhoneNumber = int.Parse(parts[3]),
                PostalCode = int.Parse(parts[4]),
                City = parts[5]
            };
        }
    }
}
