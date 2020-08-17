using System.Collections.Generic;
using System.Linq;
using General.Behaviours;
using General.ObjectManagement.Spawning;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General.ObjectManagement.Pooling
{
    public class ObjectPool : ExtendedBehaviour, IObjectPool
    {
        [SerializeField] private PoolObject[] _poolObjects;
        
        private readonly List<TrackedObject> _pool = new List<TrackedObject>();

        public GameObject Create(ObjectType objectType)
        {
            GameObject instance = _pool
                .FirstOrDefault(obj => obj.ObjectType == objectType && !obj.Instance.activeSelf).Instance;

            if (instance != null)
            {
                instance.gameObject.SetActive(true);
                instance.transform.rotation = transform.rotation;
            }
            else
            {
                PoolObject poolObject = _poolObjects.FirstOrDefault(obj => obj.ObjectType == objectType);
                GameObject prefab = poolObject.Prefabs[Random.Range(0, poolObject.Prefabs.Length)];
                instance = Instantiate(prefab, transform);
                
                _pool.Add(new TrackedObject
                {
                    ObjectType = objectType,
                    Instance = instance
                });
            }

            return instance;
        }
    }
}
