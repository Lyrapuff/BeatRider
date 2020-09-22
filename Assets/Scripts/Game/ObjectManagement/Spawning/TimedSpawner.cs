using General.AudioTracks;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;

namespace Game.ObjectManagement.Spawning
{
    [RequireComponent(typeof(IObjectSpawner))]
    public class TimedSpawner : ExtendedBehaviour
    {
        [SerializeField] private float _interval;
        
        private IObjectSpawner _spawner;
        private IAudioAnalyzer _audioAnalyzer;

        private float _distance;

        private void Awake()
        {
            _spawner = GetComponent<IObjectSpawner>();
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
        }

        private void Update()
        {
            _distance += Time.deltaTime * _audioAnalyzer.Speed;

            if (_distance >= _interval)
            {
                _spawner.Spawn();
                _distance = 0f;
            }
        }
    }
}