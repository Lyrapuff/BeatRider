using Game.Services;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.ObjectManagement.Pooling;
using General.ObjectManagement.Spawning;
using UnityEngine;

namespace Game.ObjectManagement
{
    [RequireComponent(typeof(IObjectPool))]
    [RequireComponent(typeof(IObjectSpawner))]
    public class TimedSpawner : ExtendedBehaviour
    {
        [SerializeField] private float _delay;

        private IObjectSpawner _spawner;
        private IAudioAnalyzer _audioAnalyzer;
        private IPause _pause;
        
        private float _time;

        private void Awake()
        {
            _pause = FindComponentOfInterface<IPause>();
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            _spawner = GetComponent<IObjectSpawner>();
        }

        private void Update()
        {
            if (_pause.Paused)
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