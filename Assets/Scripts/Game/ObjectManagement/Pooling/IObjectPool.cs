using UnityEngine;

namespace Game.ObjectManagement.Pooling
{
    public interface IObjectPool
    {
        GameObject Get(GameObject prefab);
    }
}