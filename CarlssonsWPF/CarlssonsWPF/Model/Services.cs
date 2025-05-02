using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlssonsWPF.Model
{
    public class Services
    {
        public string ServiceType { get; set; }
        public int Complexity { get; set; }

        //Liste til at se hvilke projekter der bruger de enkelte services
        public List<string> ProjectIds { get; set; } = new List<string>();

        public Services(string serviceType, int complexity)
        {
            ServiceType = serviceType;
            Complexity = complexity;
        }
    }
}
