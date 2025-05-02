using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        // Added to support the Customer relationship
        public string CustomerId { get; set; }

        // Added to support the Contract relationship
        public int ContractId { get; set; }

        // Liste til at Project kan bruge Services
        public List<string> ServiceTypes { get; set; } = new List<string>();

        public Project(string caseNumber, string addressOfDelivery, DateTime dateOfDelivery,
                       double priceEstimate, int scale, string customerId)
        {
            CaseNumber = caseNumber;
            AddressOfDelivery = addressOfDelivery;
            DateOfDelivery = dateOfDelivery;
            PriceEstimate = priceEstimate;
            Scale = scale;
            CustomerId = customerId;
        }
    }
}
