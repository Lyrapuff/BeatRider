using General.ObjectManagement.Spawning;
using UnityEngine;

namespace General.ObjectManagement.Pooling
{
    public interface IObjectPool
    {
        GameObject Create(ObjectType objectType);
    }
}