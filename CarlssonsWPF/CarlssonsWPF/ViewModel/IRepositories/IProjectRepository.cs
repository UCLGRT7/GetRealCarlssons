using System.Collections.Generic;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface IProjectRepository
    {
        Project GetByCaseNumber(string caseNumber);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetByCustomerId(string customerName);
        IEnumerable<Project> GetByAddress(string address);
        IEnumerable<Project> GetByScale(int scale);
        void Add(Project project);
        void Update(Project project);
        void Delete(string caseNumber);
        void SaveAllOnExit();

    }
}
