using System.Collections.Generic;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface IServiceRepository 
    {
        ServiceEntry GetById(int id);
        IEnumerable<ServiceEntry> GetAll();
        IEnumerable<ServiceEntry> GetByServiceEntry(string serviceEntry);
        IEnumerable<ServiceEntry> GetByComplexity(int complexity);
        void Add(ServiceEntry service);
        void Update(ServiceEntry service);
        void Delete(int id);
        void SaveAllOnExit();

    }
}
