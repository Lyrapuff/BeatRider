using System;
using General.ObjectManagement.Spawning;
using UnityEngine;

namespace General.ObjectManagement.Pooling
{
    [Serializable]
    internal struct PoolObject
    {
        public ObjectType ObjectType => _objectType;
        public GameObject[] Prefabs => _prefabs;
        
        [SerializeField] private ObjectType _objectType;
        [SerializeField] private GameObject[] _prefabs;
    }
}