using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carlssons
{
    public class Project
    {
        public string CaseNumber { get; set; }
        public string AddressOfDelivery { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public double PriceEstimate { get; set; }
        public int Scale { get; set; }

        public Project(string caseNumber, string addressOfDelivery, DateTime dateOfDelivery, double priceEstimate, int scale)
        {
            CaseNumber = caseNumber;
            AddressOfDelivery = addressOfDelivery;
            DateOfDelivery = dateOfDelivery;
            PriceEstimate = priceEstimate;
            Scale = scale;
        }

        public void CreateProject()
        {
            
        }

        public void SearchCaseNumber()
        {
            
        }

        public void SearchAddress()
        {
            
        }

        public void SearchScale()
        {
            
        }

        public void EditDate()
        {
            
        }
    }
}
