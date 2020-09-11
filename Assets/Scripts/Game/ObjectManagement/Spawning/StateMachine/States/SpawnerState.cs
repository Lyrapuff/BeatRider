using Game.ObjectManagement.Pooling;
using UnityEngine;

namespace Game.ObjectManagement.Spawning.StateMachine.States
{
    public abstract class SpawnerState : ScriptableObject
    {
        public int Length => _length;

        protected GameObject[] Prefabs => _prefabs;
        
        [SerializeField] private int _length;
        [SerializeField] private GameObject[] _prefabs;
        
        public abstract void Spawn(IObjectPool pool);
    }
}