using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Contract
    {
        public int InvoiceNumber { get; set; }
        public DateTime OfferSent { get; set; }
        public DateTime PaymentReceived { get; set; }
        public DateTime OfferConfirmed { get; set; }
        public double Price { get; set; }
        public string CaseNumber { get; set; } // Reference to Project
        [JsonIgnore]
        public Project Project { get; set; }

        public override string ToString()
        {
            return $"{InvoiceNumber},{OfferSent:yyyy-MM-dd},{PaymentReceived:yyyy-MM-dd},{OfferConfirmed:yyyy-MM-dd},{Price},{CaseNumber}";
        }

        public static Contract FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 6)
                throw new FormatException("Invalid contract data format");
            return new Contract
            {
                InvoiceNumber = int.Parse(parts[0]),
                OfferSent = DateTime.Parse(parts[1]),
                PaymentReceived = DateTime.Parse(parts[2]),
                OfferConfirmed = DateTime.Parse(parts[3]),
                Price = double.Parse(parts[4]),
                CaseNumber = parts[5]
            };
        }
    }
}
