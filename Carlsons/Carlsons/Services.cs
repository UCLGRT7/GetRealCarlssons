using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carlssons
{
    public class Services
    {
        public string ServiceType { get; set; }
        public int Complexity { get; set; }

        public Services(string serviceType, int complexity)
        {
            ServiceType = serviceType;
            Complexity = complexity;
        }

        public void SearchServices()
        {
            
        }

        public void CreateServices()
        {
            
        }
    }
}
