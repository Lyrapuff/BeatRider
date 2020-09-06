using Game.ObjectManagement.Spawning.States;
using General.Behaviours;
using General.ObjectManagement.Pooling;
using General.ObjectManagement.Spawning;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.ObjectManagement.Spawning
{
    [RequireComponent(typeof(IObjectPool))]
    public class StateMachineSpawner : ExtendedBehaviour, IObjectSpawner
    {
        public SpawnerPattern[] Patterns => _patterns;
        
        [SerializeField] private SpawnerPattern[] _patterns;
        
        private IObjectPool _pool;
        private SpawnerState _state;

        private void Awake()
        {
            _pool = GetComponent<IObjectPool>();
            SetRandomState();
        }

        public void SetRandomState()
        {
            int random = Random.Range(0, 2);

            SpawnerState state = random == 0 ? (SpawnerState) new RandomSpawnerState(this) : new PatternSpawnerState(this);

            _state = state;
        }
        
        public void Spawn()
        {
            _state.Next();
        }

        public IObjectPool GetPool()
        {
            return _pool;
        }
    }
}