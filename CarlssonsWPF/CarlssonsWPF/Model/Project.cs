using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Project
    {
        private Contract contract;

        public string? CaseNumber { get; set; }
        public string? ProjectAddress { get; set; }
        public DateTime? Deadline { get; set; }
        public double? EstimatedPrice { get; set; }
        public int Scope {  get; set; }
        public string? CustomerName { get; set; }
        public string? Status { get; set; }
        public DateTime? LastModified { get; set; }
        public int? ProjectPostalCode { get; set; }


       
        [JsonIgnore]
        public List<Contract> Contracts { get; set; } = new List<Contract>();
        [JsonIgnore]
        public List<ServiceEntry> Services { get; set; } = new List<ServiceEntry>();

       
        private string? _deadlineInput;
        [JsonIgnore]
        public Contract Contract { get => contract; set => contract = value; }





        public override string ToString()
        {
            return $"{CaseNumber},{ProjectAddress},{Deadline:yyyy-MM-dd},{EstimatedPrice},{Scope},{CustomerName},{Status},{LastModified},{ProjectPostalCode}";
        }

        public static Project FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 10)
                throw new FormatException("Invalid project data format");

            var deadlineParsed = DateTime.TryParse(parts[2], out var deadline) ? deadline : (DateTime?)null;
            var lastModifiedParsed = DateTime.TryParse(parts[7], out var lastModified) ? lastModified : DateTime.Now;
            var postalCodeParsed = int.TryParse(parts[8], out var postalCode) ? postalCode : (int?)null;

            return new Project
            {
                //Id = Guid.NewGuid(),
                CaseNumber = parts[0],
                ProjectAddress = parts[1],
                Deadline = deadlineParsed,
                EstimatedPrice = double.Parse(parts[3]),
                Scope = int.Parse(parts[4]),
                CustomerName = parts[5],
                Status = parts[6],
                LastModified = lastModifiedParsed,
                ProjectPostalCode = postalCodeParsed
            };

            // Brug Input-properties til at sikre korrekt visning
            //project.DeadlineInput = project.Deadline?.ToString("dd/MM/yy") ?? "";

            // Hvis du tilføjer flere datoer i filen senere, gør ligesom ovenfor:
            // project.OfferSent = ...
            // project.OfferSentInput = ...

           
        }


    }

}
//private string? _caseNumber;
//public string? CaseNumber
//{
//    get => _caseNumber;
//    set { _caseNumber = value; OnPropertyChanged(); }
//}
//private string? _projectAddress;
//public string? ProjectAddress
//{
//    get => _projectAddress;
//    set { _projectAddress = value; OnPropertyChanged(); }
//}
//private int? _projectPostalCode;
//public int? ProjectPostalCode
//{
//    get => _projectPostalCode;
//    set { _projectPostalCode = value; OnPropertyChanged(); }
//}
//private DateTime? _deadline;
//[JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
//public DateTime? Deadline
//{
//    get => _deadline;
//    set { _deadline = value; OnPropertyChanged(); }
//}
//private double _estimatedPrice;
//public double EstimatedPrice
//{
//    get => _estimatedPrice;
//    set { _estimatedPrice = value; OnPropertyChanged(); }
//}
//private double _price;
//public double Price
//{
//    get => _price;
//    set { _price = value; OnPropertyChanged(); }
//}

//private int _scope;
//public int Scope
//{
//    get => _scope;
//    set { _scope = value; OnPropertyChanged(); }
//}
//private string? _customerName;
//public string? CustomerName
//{
//    get => _customerName;
//    set
//    {
//        _customerName = value;
//        System.Diagnostics.Debug.WriteLine($"CustomerName set to: {value}"); // Debug: udskriv den nye værdi
//        OnPropertyChanged();
//    }
//}

//private string? _status;
//public string? Status
//{
//    get => _status;
//    set { _status = value; OnPropertyChanged(); }
//}
//private DateTime _lastModified;
//public DateTime LastModified
//{
//    get => _lastModified;
//    set { _lastModified = value; OnPropertyChanged(); }
//}


//private DateTime? _offerSent;
//[JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
//public DateTime? OfferSent
//{
//    get => _offerSent;
//    set { _offerSent = value; OnPropertyChanged(); }
//}

//private DateTime? _offerApproved;
//[JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
//public DateTime? OfferApproved
//{
//    get => _offerApproved;
//    set { _offerApproved = value; OnPropertyChanged(); }
//}


//private DateTime? _paid;
//[JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
//public DateTime? Paid
//{
//    get => _paid;
//    set { _paid = value; OnPropertyChanged(); }
//}




