using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface IServicesRepository : IRepository<Services>
    {
        IEnumerable<Services> GetByServiceType(string serviceType);
        IEnumerable<Services> GetByComplexity(int complexity);
    }
}
