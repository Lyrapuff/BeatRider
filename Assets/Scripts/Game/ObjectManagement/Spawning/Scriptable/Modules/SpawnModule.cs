using Game.ObjectManagement.Pooling;
using UnityEngine;

namespace Game.ObjectManagement.Spawning.Scriptable.Modules
{
    public abstract class SpawnModule : ScriptableObject
    {
        public abstract void Spawn(IObjectPool pool);
    }
}