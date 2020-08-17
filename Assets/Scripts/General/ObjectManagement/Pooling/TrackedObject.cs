using General.ObjectManagement.Spawning;
using UnityEngine;

namespace General.ObjectManagement.Pooling
{
    internal struct TrackedObject
    {
        public ObjectType ObjectType;
        public GameObject Instance;
    }
}