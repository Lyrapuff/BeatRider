using General.AudioTracks;
using General.Behaviours;
using UnityEngine;

namespace Game.ObjectManagement.Spawning
{
    [RequireComponent(typeof(IObjectSpawner))]
    public class TimedSpawner : ExtendedBehaviour
    {
        [SerializeField] private float _interval;
        
        private IObjectSpawner _spawner;
        private SpectrumReader _spectrumReader;

        private float _time;

        private void Awake()
        {
            _spawner = GetComponent<IObjectSpawner>();
            _spectrumReader = FindObjectOfType<SpectrumReader>();
        }

        private void Update()
        {
            _time += Time.deltaTime * _spectrumReader.Speed;

            if (_time >= _interval)
            {
                _spawner.Spawn();
                _time = 0f;
            }
        }
    }
}