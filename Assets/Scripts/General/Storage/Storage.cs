using System;
using System.Collections.Generic;
using General.Behaviours;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace General.Storage
{
    public class Storage : ExtendedBehaviour, IStorage
    {
        private Dictionary<string, StoredObject> _storage = new Dictionary<string, StoredObject>();

        private void Awake()
        {
            LoadFromDrive();
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
                    _storage = binaryFormatter.Deserialize(steam) as Dictionary<string, StoredObject>;
                }
            }
        }

        private void SaveToDrive()
        {
            string path = Application.persistentDataPath + "/storage.bytes";
            
            Dictionary<string, StoredObject> save = new Dictionary<string, StoredObject>();

            foreach (KeyValuePair<string,StoredObject> obj in _storage)
            {
                if (obj.Value.Persistant)
                {
                    save[obj.Key] = obj.Value;
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
            _storage[key] = new StoredObject(data, persistant);
        }

        public T Get<T>(string key) where T : class
        {
            if (_storage.ContainsKey(key))
            {
                return _storage[key].Data as T;
            }

            return null;
        }

        public T GetOrCreate<T>(string key) where T : class, new()
        {
            if (!_storage.ContainsKey(key))
            {
                Store(key, new T());
            }
            
            return Get<T>(key);
        }
    }

    [Serializable]
    public struct StoredObject
    {
        public object Data;
        public bool Persistant;

        public StoredObject(object data, bool persistant)
        {
            Data = data;
            Persistant = persistant;
        }
    }
}