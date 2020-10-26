using General.AudioTracks.Analyzing;
using General.AudioTracks.RoadGeneration;
using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace Game.ObjectManagement.Spawning
{
    [RequireComponent(typeof(IObjectSpawner))]
    public class AudioSpawner : ExtendedBehaviour
    {
        private IAudioAnalyzer _audioAnalyzer;
        private IObjectSpawner _spawner;
        
        private Road _road;
        private float _offset = 300f;
        private int _indexOffset;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
            _spawner = GetComponent<IObjectSpawner>();

            IStorage storage = FindComponentOfInterface<IStorage>();
            _road = storage.Get<Road>("Game/Road");
        }

        private void Update()
        {
            _offset += _audioAnalyzer.Speed * Time.deltaTime;

            float beat = _road.Beats[_indexOffset];

            if (_offset >= beat)
            {
                _indexOffset++;
                _spawner.Spawn();
            }
        }
    }
}