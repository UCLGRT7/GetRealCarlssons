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
        static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        static string folder = Path.Combine(projectPath, "Data");
        static string subFolder = Path.Combine(folder, "SavedFiles");
        

        protected readonly string _filePath;
        protected List<T> _entities;

        public FileRepository(string filePath)
        {
            _filePath = filePath;
            string directory = Path.GetDirectoryName(_filePath);
            string subFOlder = Path.Combine(subFolder, _filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create file if it doesn't exist
            if (!File.Exists(subFOlder))
            {
                // Create an empty JSON array
                File.WriteAllText(subFOlder, "[]");
            }
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
