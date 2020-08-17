using General.Audio;
using General.Behaviours;
using General.ObjectManagement.Pooling;
using General.ObjectManagement.Spawning;
using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.World.ObjectManagement
{
    [RequireComponent(typeof(IObjectPool))]
    [RequireComponent(typeof(IObjectSpawner))]
    public class TimedSpawner : ExtendedBehaviour
    {
        [SerializeField] private float _delay;

        private GameStatusService _gameStatus;
        private IObjectSpawner _spawner;
        private IAudioAnalyzer _audioAnalyzer;
        private float _time;

        private void Awake()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            _spawner = GetComponent<IObjectSpawner>();
        }

        private void Update()
        {
            if (_gameStatus.Status == GameStatusChangeType.Crushed)
            {
                return;
            }
            
            _time += Time.deltaTime * _audioAnalyzer.Speed;

            if (_time >= _delay)
            {
                _time = 0f;
                Spawn();
            }
        }

        private void Spawn()
        {
            _spawner.Spawn();
        }
    }
}