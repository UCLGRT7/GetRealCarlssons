using System;

namespace CarlssonsWPF.Model
{
    public class RemindersData
    {
        public string CaseNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime? OfferSentDate { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public string OfferApproved { get; set; }
        public string IsPaymentRecieved { get; set; }
        public int DaysPassed { get; set; }

    }
}
