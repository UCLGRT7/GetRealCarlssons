using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Project
    {
        public string CaseNumber { get; set; }
        public string AddressOfDelivery { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public double PriceEstimate { get; set; }
        public int Scale { get; set; }
        public string CustomerName { get; set; } // Reference to Customer
        public string Status { get; set; }
        [JsonIgnore]
        public List<Contract> Contracts { get; set; } = new List<Contract>();
        [JsonIgnore]
        public List<Services> Services { get; set; } = new List<Services>();

        public override string ToString()
        {
            return $"{CaseNumber},{AddressOfDelivery},{DateOfDelivery:yyyy-MM-dd},{PriceEstimate},{Scale},{CustomerName}";
        }

        public static Project FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 6)
                throw new FormatException("Invalid project data format");
            return new Project
            {
                CaseNumber = parts[0],
                AddressOfDelivery = parts[1],
                DateOfDelivery = DateTime.Parse(parts[2]),
                PriceEstimate = double.Parse(parts[3]),
                Scale = int.Parse(parts[4]),
                CustomerName = parts[5],
                Status = parts[6]
            };
        }
    }
}
