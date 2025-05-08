public class CombinedProjectData
{
    public string CaseNumber { get; set; }
    public string CustomerName { get; set; }
    public DateTime? LastModified { get; set; }
    public DateTime? DateOfDelivery { get; set; }
    public DateTime? PaymentReceivedDate { get; set; }
    public string Status { get; set; }
    public string OfferConfirmed { get; set; }
    public string IsPaymentReceived { get; set; }



    //public string IsPaymentReceivedMethod()
    //{
    //    if (PaymentReceivedDate.HasValue)
    //    {
    //        return IsPaymentReceived = "Ja";
    //    }
    //    else
    //    {
    //        return IsPaymentReceived = "Yes";
    //    }
    //}
}
