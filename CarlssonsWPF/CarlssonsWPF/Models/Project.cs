
using System;
using System.Collections.Generic;

namespace GetRealCarlssons.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string CaseNumber { get; set; }
        public string Address { get; set; }
        public string Deadline { get; set; }
        public int Scope { get; set; }
        public List<ServiceEntry> Services { get; set; } = new();
        public string OfferSent { get; set; }
        public string OfferApproved { get; set; }
        public double EstimatedPrice { get; set; }
        public string Price { get; set; }
        public string Paid { get; set; }

        public string Status => OfferApproved != null ? "Godkendt" : "Afventer";
    }

    public class ServiceEntry
    {
        public string Name { get; set; }
        public int Complexity { get; set; }
    }
}
