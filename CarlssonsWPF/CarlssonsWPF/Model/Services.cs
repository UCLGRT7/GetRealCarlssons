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
        public string? Name { get; set; }
        public int Complexity { get; set; }

        public override string ToString()
        {
            return $"{Id},{Name},{Complexity}";
        }

        public static Services FromString(string input)
        {
            string[] parts = input.Split(',');
            if (parts.Length < 3)
                throw new FormatException("Invalid service data format");
            return new Services
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Complexity = int.Parse(parts[2])
            };
        }
    }
}
