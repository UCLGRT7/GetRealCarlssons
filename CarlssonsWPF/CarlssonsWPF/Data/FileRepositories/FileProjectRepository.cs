using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public class ProjectFileRepository : FileRepository<Project>, IProjectRepository
    {
        public ProjectFileRepository() : base("projects.json") { }

        public override Project GetById(object id)
        {
            string projectId = id.ToString();
            return _entities.FirstOrDefault(p => p.CaseNumber == projectId);
        }

        public Project GetByCaseNumber(string caseNumber)
        {
            return _entities.FirstOrDefault(p => p.CaseNumber == caseNumber);
        }

        public IEnumerable<Project> GetByAddress(string address)
        {
            return _entities.Where(p => p.AddressOfDelivery.Contains(address));
        }

        public IEnumerable<Project> GetByScale(int scale)
        {
            return _entities.Where(p => p.Scale == scale);
        }

        public IEnumerable<Project> GetByCustomerId(string customerId)
        {
            // This would require Project to have a CustomerId property
            // Adding this property to the model is recommended
            return _entities.Where(p => p.CustomerId == customerId);
        }

        public override void Delete(object id)
        {
            string projectId = id.ToString();
            var project = GetById(projectId);
            if (project != null)
            {
                _entities.Remove(project);
            }
        }

        public override void Update(Project entity)
        {
            var existingProject = GetById(entity.CaseNumber);
            if (existingProject != null)
            {
                int index = _entities.IndexOf(existingProject);
                _entities[index] = entity;
            }
        }
    }
}
