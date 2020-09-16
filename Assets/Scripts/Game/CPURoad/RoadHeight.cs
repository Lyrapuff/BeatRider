using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace Game.CPURoad
{
    public class RoadHeight : ExtendedBehaviour
    {
        public float Offset { get; private set; }
        
        private IAudioAnalyzer _audioAnalyzer;
        private float _seed;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
            
            IStorage storage = FindComponentOfInterface<IStorage>();
            _seed = storage.Get<float>("Game/Seed");
        }

        private void Update()
        {
            Offset += Time.deltaTime * _audioAnalyzer.Speed * 20f;
        }

        public float GetHeight(float point)
        {
            return Mathf.PerlinNoise((point + Offset + _seed) / 214.253f, 0.245f);
        }
        
        public float GetHeight(Vector3 position)
        {
            return GetHeight(position.z) * 20f;
        }
    }
}