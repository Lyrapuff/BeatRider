using System;
using System.Collections.Generic;
using General.Behaviours;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace General.Storage
{
    public class Storage : ExtendedBehaviour, IStorage
    {
        public List<StoredObject> storage;
    
        private List<StoredObject> _storage = new List<StoredObject>();

        private void Awake()
        {
            LoadFromDrive();
        }

        private void Update()
        {
            storage = _storage;
        }

        private void OnApplicationQuit()
        {
            SaveToDrive();
        }

        private void LoadFromDrive()
        {
            string path = Application.persistentDataPath + "/storage.bytes";

            if (File.Exists(path))
            {
                using (FileStream steam = File.OpenRead(path))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    _storage = binaryFormatter.Deserialize(steam) as List<StoredObject> ?? new List<StoredObject>();
                }
            }
        }

        private void SaveToDrive()
        {
            string path = Application.persistentDataPath + "/storage.bytes";
            
            List<StoredObject> save = new List<StoredObject>();

            foreach (StoredObject obj in _storage)
            {
                if (obj.Persistant)
                {
                    save.Add(obj);
                }
            }

            using (FileStream stream = File.OpenWrite(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, save);
            }
        }
        
        public void Store(string key, object data, bool persistant = false)
        {
            _storage.Add(new StoredObject(key, data, persistant));
        }

        public T Get<T>(string key)
        {
            StoredObject obj = _storage.FirstOrDefault(x => x.Key == key);
            
            if (obj != null)
            {
                return (T)obj.Data;
            }

            return default;
        }

        public T GetOrCreate<T>(string key) where T : class, new()
        {
            StoredObject obj = _storage.FirstOrDefault(x => x.Key == key);

            if (obj == null)
            {
                Store(key, new T());
            }
            
            return Get<T>(key);
        }
    }

    [Serializable]
    public class StoredObject
    {
        public string Key;
        public object Data;
        public bool Persistant;

        public StoredObject(string key, object data, bool persistant)
        {
            Key = key;
            Data = data;
            Persistant = persistant;
        }
    }
}