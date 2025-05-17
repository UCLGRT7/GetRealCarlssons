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
        public string ProjectAddress { get; set; }
        public int? ProjectPostalCode { get; set; } // nullable
        public DateTime Deadline { get; set; }
        public double EstimatedPrice { get; set; }
        public double Price { get; set; }

        public int Scope { get; set; }
        public string CustomerName { get; set; } // Reference to Customer
        public string Status { get; set; }
        public DateTime LastModified { get; set; }

        public DateTime? OfferSent { get; set; }
        public DateTime? OfferApproved { get; set; }
        public DateTime? Paid { get; set; }







        [JsonIgnore]
        public List<Contract> Contracts { get; set; } = new List<Contract>();
        [JsonIgnore]
        public List<Services> Services { get; set; } = new List<Services>();

        public override string ToString()
        {
            return $"{CaseNumber},{ProjectAddress},{Deadline:yyyy-MM-dd},{EstimatedPrice},{Scope},{CustomerName},{Status},{LastModified}";
        }

        public static Project FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 7)
                throw new FormatException("Invalid project data format");
            return new Project
            {
                CaseNumber = parts[0],
                ProjectAddress = parts[1],
                Deadline = DateTime.Parse(parts[2]),
                EstimatedPrice = double.Parse(parts[3]),
                Scope = int.Parse(parts[4]),
                CustomerName = parts[5],
                Status = parts[6],
                LastModified = DateTime.Parse(parts[7]),
                ProjectPostalCode = int.Parse(parts[8]),
            };
        }
    }

}
