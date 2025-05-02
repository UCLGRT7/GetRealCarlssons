using System;
using System.Collections.Generic;

namespace CarlssonsWPF
{
    // Base interface that all repository interfaces will implement
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
        void Save();
    }

    // Customer repository interface
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
        IEnumerable<Customer> GetByName(string name);
    }

    // Project repository interface
    public interface IProjectRepository : IRepository<Project>
    {
        Project GetByCaseNumber(string caseNumber);
        IEnumerable<Project> GetByAddress(string address);
        IEnumerable<Project> GetByScale(int scale);
        IEnumerable<Project> GetByCustomerId(string customerId);
    }

    // Contract repository interface
    public interface IContractRepository : IRepository<Contract>
    {
        Contract GetByInvoiceNumber(int invoiceNumber);
        IEnumerable<Contract> GetByProjectId(string projectId);
    }

    // Services repository interface
    public interface IServicesRepository : IRepository<Services>
    {
        IEnumerable<Services> GetByServiceType(string serviceType);
        IEnumerable<Services> GetByComplexity(int complexity);
    }