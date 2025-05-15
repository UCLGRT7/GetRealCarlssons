using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarlssonsWPF.Model
{
    public class Services
    {
        public int Id { get; set; }
        public string? ServiceEntry { get; set; }
        public int Complexity { get; set; }

        public override string ToString()
        {
            return $"{Id},{ServiceEntry},{Complexity}";
        }

        public static Services FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 3)
                throw new FormatException("Invalid service data format");
            return new Services
            {
                Id = int.Parse(parts[0]),
                ServiceEntry = parts[1],
                Complexity = int.Parse(parts[2])
            };
        }
    }

    public class ServiceEntry : INotifyPropertyChanged
    {
        private string _name = "";
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private int _complexity;
        public int Complexity
        {
            get => _complexity;
            set { _complexity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
