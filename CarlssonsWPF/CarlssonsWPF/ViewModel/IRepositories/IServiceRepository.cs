using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface IServiceRepository 
    {
        Services GetById(int id);
        IEnumerable<Services> GetAll();
        IEnumerable<Services> GetByServiceType(string serviceType);
        IEnumerable<Services> GetByComplexity(int complexity);
        void Add(Services service);
        void Update(Services service);
        void Delete(int id);
        void SaveAllOnExit();

    }
}
