using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Contract
    {
        public DateTime OfferSent { get; set; }
        public DateTime PaymentReceived { get; set; }
        public DateTime OfferConfirmed { get; set; }
        public double Price { get; set; }
        public int InvoiceNumber { get; set; }

        // Associationen mellem Contract og Project
        public string ProjectId { get; set; }

        public Contract(DateTime offerSent, DateTime paymentReceived, DateTime offerConfirmed,
                        double price, int invoiceNumber, string projectId)
        {
            OfferSent = offerSent;
            PaymentReceived = paymentReceived;
            OfferConfirmed = offerConfirmed;
            Price = price;
            InvoiceNumber = invoiceNumber;
            ProjectId = projectId;
        }
    }
}
