using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Services
    {
        public int Id { get; set; }
        public string ServiceType { get; set; }
        public int Complexity { get; set; }

        public override string ToString()
        {
            return $"{Id},{ServiceType},{Complexity}";
        }

        public static Services FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 3)
                throw new FormatException("Invalid service data format");
            return new Services
            {
                Id = int.Parse(parts[0]),
                ServiceType = parts[1],
                Complexity = int.Parse(parts[2])
            };
        }
    }
}
