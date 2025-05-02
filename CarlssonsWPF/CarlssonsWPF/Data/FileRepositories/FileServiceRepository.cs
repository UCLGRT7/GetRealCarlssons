using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlssonsWPF.Model;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public class ServicesFileRepository : FileRepository<Services>, IServicesRepository
    {
        public ServicesFileRepository() : base("services.json") { }

        public override Services GetById(object id)
        {
            string serviceType = id.ToString();
            return _entities.FirstOrDefault(s => s.ServiceType == serviceType);
        }

        public IEnumerable<Services> GetByServiceType(string serviceType)
        {
            return _entities.Where(s => s.ServiceType.Contains(serviceType));
        }

        public IEnumerable<Services> GetByComplexity(int complexity)
        {
            return _entities.Where(s => s.Complexity == complexity);
        }

        public override void Delete(object id)
        {
            string serviceType = id.ToString();
            var service = GetById(serviceType);
            if (service != null)
            {
                _entities.Remove(service);
            }
        }

        public override void Update(Services entity)
        {
            var existingService = GetById(entity.ServiceType);
            if (existingService != null)
            {
                int index = _entities.IndexOf(existingService);
                _entities[index] = entity;
            }
        }
    }
}
