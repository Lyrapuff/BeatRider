using Game.ObjectManagement.Pooling;
using Game.ObjectManagement.Spawning.Scriptable.Modules;
using General.Behaviours;
using UnityEngine;

namespace Game.ObjectManagement.Spawning.Scriptable
{
    public class ScriptableSpawner : ExtendedBehaviour, IObjectSpawner
    {
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private SpawnModule _spawnModule;
        
        public void Spawn()
        {
            _spawnModule.Spawn(_pool);
        }
    }
}