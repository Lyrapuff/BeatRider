using System.Collections.Generic;
using System.Linq;
using General.Behaviours;
using UnityEngine;

namespace Game.ObjectManagement.Pooling
{
    public class ObjectPool : ExtendedBehaviour, IObjectPool
    {
        private Dictionary<GameObject, List<GameObject>> _instances = new Dictionary<GameObject, List<GameObject>>();
        
        public GameObject Get(GameObject prefab)
        {
            if (!_instances.ContainsKey(prefab))
            {
                _instances[prefab] = new List<GameObject>();
            }

            GameObject instance = _instances[prefab].FirstOrDefault(x => !x.activeSelf);

            if (instance == null)
            {
                instance = Instantiate(prefab, transform);
                _instances[prefab].Add(instance);
            }
            else
            {
                instance.SetActive(true);
            }

            return instance;
        }
    }
}