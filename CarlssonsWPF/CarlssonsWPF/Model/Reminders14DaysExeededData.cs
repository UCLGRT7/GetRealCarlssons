using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Reminders14DaysExeededData
    {
        public string CaseNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime? OfferSentDate { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public string OfferConfirmed { get; set; }
        public string IsPaymentRecieved { get; set; }
        public int DaysPassed { get; set; }





    }
}
