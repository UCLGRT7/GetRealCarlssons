using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarlssonsWPF.ViewModel.IRepositories;

namespace CarlssonsWPF.Data.FileRepositories
{
    public abstract class FileRepository<T> : IRepository<T> where T : class
    {

        protected readonly string _filePath;
        protected List<T> _entities;
        public string FilePath { get; set; }

        public FileRepository(string filePath)
        {
            //_filePath = filePath;
            
            LoadData();
        }

        protected void LoadData()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _entities = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            else
            {
                _entities = new List<T>();
            }
        }

        protected void SaveData()
        {
            string json = JsonSerializer.Serialize(_entities);
            File.WriteAllText(_filePath, json);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _entities;
        }

        public abstract T GetById(object id);

        public virtual void Add(T entity)
        {
            _entities.Add(entity);
        }

        public virtual void Update(T entity)
        {
            // Implementation depends on specific entity
        }

        public abstract void Delete(object id);

        public virtual void Save()
        {
            SaveData();
        }
    }
}
