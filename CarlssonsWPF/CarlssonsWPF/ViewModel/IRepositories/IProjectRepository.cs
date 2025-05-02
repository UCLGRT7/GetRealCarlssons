using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;

namespace CarlssonsWPF.ViewModel.IRepositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Project GetByCaseNumber(string caseNumber);
        IEnumerable<Project> GetByAddress(string address);
        IEnumerable<Project> GetByScale(int scale);
        IEnumerable<Project> GetByCustomerId(string customerId);
    }
}
