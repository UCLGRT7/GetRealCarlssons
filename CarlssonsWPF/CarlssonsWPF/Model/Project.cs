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
    public class Project : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        [JsonIgnore]
        public Customer Customer { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Guid Id { get; set; } = Guid.NewGuid();

        private string? _caseNumber;
        public string? CaseNumber
        {
            get => _caseNumber;
            set { _caseNumber = value; OnPropertyChanged(); }
        }
        private string? _projectAddress;
        public string? ProjectAddress
        {
            get => _projectAddress;
            set { _projectAddress = value; OnPropertyChanged(); }
        }
        private int? _projectPostalCode;
        public int? ProjectPostalCode
        {
            get => _projectPostalCode;
            set { _projectPostalCode = value; OnPropertyChanged(); }
        }
        private DateTime? _deadline;
        [JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
        public DateTime? Deadline
        {
            get => _deadline;
            set { _deadline = value; OnPropertyChanged(); }
        }
        private double _estimatedPrice;
        public double EstimatedPrice
        {
            get => _estimatedPrice;
            set { _estimatedPrice = value; OnPropertyChanged(); }
        }
        private double _price;
        public double Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        private int _scope;
        public int Scope
        {
            get => _scope;
            set { _scope = value; OnPropertyChanged(); }
        }
        private string? _customerName;
        public string? CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
                System.Diagnostics.Debug.WriteLine($"CustomerName set to: {value}"); // Debug: udskriv den nye værdi
                OnPropertyChanged();
            }
        }

        private string? _status;
        public string? Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }
        private DateTime _lastModified;
        public DateTime LastModified
        {
            get => _lastModified;
            set { _lastModified = value; OnPropertyChanged(); }
        }


        private DateTime? _offerSent;
        [JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
        public DateTime? OfferSent
        {
            get => _offerSent;
            set { _offerSent = value; OnPropertyChanged(); }
        }


        private DateTime? _offerApproved;
        [JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
        public DateTime? OfferApproved
        {
            get => _offerApproved;
            set { _offerApproved = value; OnPropertyChanged(); }
        }


        private DateTime? _paid;
        [JsonConverter(typeof(CarlssonsWPF.Helpers.DateOnlyConverter))]
        public DateTime? Paid
        {
            get => _paid;
            set { _paid = value; OnPropertyChanged(); }
        }
        [JsonIgnore]
        public List<Contract> Contracts { get; set; } = new List<Contract>();
        [JsonIgnore]
        private ObservableCollection<ServiceEntry>? _services;
        
        public ObservableCollection<ServiceEntry> Services
        {
            get => _services ??= new ObservableCollection<ServiceEntry>();
            set
            {
                _services = value;
                OnPropertyChanged();
            }
        }

       
        private string? _deadlineInput;
        [JsonIgnore]
        public string? DeadlineInput
        {
            get => _deadlineInput;
            set
            {
                _deadlineInput = value;
                OnPropertyChanged();

                if (!string.IsNullOrWhiteSpace(_deadlineInput) && _deadlineInput.Length >= 8)
                {
                    if (DateTime.TryParseExact(_deadlineInput, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out var parsed))
                    {
                        Deadline = parsed;
                    }
                }
            }
        }

        [JsonIgnore]
        private string? _offerSentInput;
        public string? OfferSentInput
        {
            get => _offerSentInput;
            set
            {
                _offerSentInput = value;
                OnPropertyChanged();

                // Forsøg kun at parse når hele datoformatet er opfyldt
                if (!string.IsNullOrWhiteSpace(_offerSentInput) && _offerSentInput.Length >= 8)
                {
                    if (DateTime.TryParseExact(_offerSentInput, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out var parsed))
                    {
                        OfferSent = parsed;
                    }
                }
            }
        }


        
        private string? _offerApprovedInput;
        [JsonIgnore]
        public string? OfferApprovedInput
        {
            get => _offerApprovedInput;
            set
            {
                _offerApprovedInput = value;
                OnPropertyChanged();

                if (!string.IsNullOrWhiteSpace(_offerApprovedInput) && _offerApprovedInput.Length >= 8)
                {
                    if (DateTime.TryParseExact(_offerApprovedInput, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out var parsed))
                    {
                        OfferApproved = parsed;
                    }
                }
            }
        }

        
        private string? _paidInput;
        [JsonIgnore]
        public string? PaidInput
        {
            get => _paidInput;
            set
            {
                _paidInput = value;
                OnPropertyChanged();

                if (!string.IsNullOrWhiteSpace(_paidInput) && _paidInput.Length >= 8)
                {
                    if (DateTime.TryParseExact(_paidInput, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out var parsed))
                    {
                        Paid = parsed;
                    }
                }
            }
        }



        public override string ToString()
        {
            return $"{CaseNumber},{ProjectAddress},{Deadline:yyyy-MM-dd},{EstimatedPrice},{Scope},{CustomerName},{Status},{LastModified}";
        }

        public static Project FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 10)
                throw new FormatException("Invalid project data format");

            var deadlineParsed = DateTime.TryParse(parts[2], out var deadline) ? deadline : (DateTime?)null;
            var lastModifiedParsed = DateTime.TryParse(parts[7], out var lastModified) ? lastModified : DateTime.Now;
            var postalCodeParsed = int.TryParse(parts[8], out var postalCode) ? postalCode : (int?)null;

            var project = new Project
            {
                Id = Guid.NewGuid(),
                CaseNumber = parts[0],
                ProjectAddress = parts[1],
                Deadline = deadlineParsed,
                EstimatedPrice = double.TryParse(parts[3], out var est) ? est : 0,
                Scope = int.TryParse(parts[4], out var scope) ? scope : 0,
                CustomerName = parts[5],
                Status = parts[6],
                LastModified = lastModifiedParsed,
                ProjectPostalCode = postalCodeParsed
            };

            // Brug Input-properties til at sikre korrekt visning
            project.DeadlineInput = project.Deadline?.ToString("dd/MM/yy") ?? "";

            // Hvis du tilføjer flere datoer i filen senere, gør ligesom ovenfor:
            // project.OfferSent = ...
            // project.OfferSentInput = ...

            return project;
        }
        public void InitFromModel()
        {
            DeadlineInput = Deadline?.ToString("dd/MM/yy") ?? "";
            OfferSentInput = OfferSent?.ToString("dd/MM/yy") ?? "";
            OfferApprovedInput = OfferApproved?.ToString("dd/MM/yy") ?? "";
            PaidInput = Paid?.ToString("dd/MM/yy") ?? "";

            // Services er allerede ObservableCollection takket være property, men hvis du har en Liste et andet sted,
            // kan du konvertere sådan her:

            {
                if (Services == null)
                    Services = new ObservableCollection<ServiceEntry>();
                else if (!(Services is ObservableCollection<ServiceEntry>))
                    Services = new ObservableCollection<ServiceEntry>(Services);
            }
        }

    }

}
