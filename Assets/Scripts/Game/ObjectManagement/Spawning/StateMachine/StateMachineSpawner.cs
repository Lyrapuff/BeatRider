using Game.ObjectManagement.Pooling;
using Game.ObjectManagement.Spawning.StateMachine.States;
using General.Behaviours;
using UnityEngine;

namespace Game.ObjectManagement.Spawning.StateMachine
{
    [RequireComponent(typeof(IObjectPool))]
    public class StateMachineSpawner : ExtendedBehaviour, IObjectSpawner
    {
        [SerializeField] private SpawnerState[] _states;

        private IObjectPool _pool;
        private SpawnerState _state;
        private int _iterations;

        private void Awake()
        {
            _pool = GetComponent<IObjectPool>();
            _state = _states[Random.Range(0, _states.Length)];
        }
        
        public void Spawn()
        {
            if (_iterations <= _state.Length)
            {
                _state.Spawn(_pool);
                _iterations++;
            }
            else
            {
                _state = _states[Random.Range(0, _states.Length)];
                _iterations = 0;
            }
        }
    }
}