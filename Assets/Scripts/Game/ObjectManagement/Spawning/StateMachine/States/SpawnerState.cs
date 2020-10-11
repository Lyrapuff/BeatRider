using Game.ObjectManagement.Pooling;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.ObjectManagement.Spawning.StateMachine.States
{
    public abstract class SpawnerState : ScriptableObject
    {
        public int Length => _length;

        public AssetReference[] References;
        
        [SerializeField] private int _length;
        
        public abstract void Spawn(IObjectPool pool);
    }
}