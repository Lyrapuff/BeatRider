using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;

namespace Game.CPURoad
{
    public class RoadHeight : ExtendedBehaviour
    {
        public float Offset { get; private set; }
        
        private IAudioAnalyzer _audioAnalyzer;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
        }

        private void Update()
        {
            Offset += Time.deltaTime * _audioAnalyzer.Speed * 20f;
        }

        public float GetHeight(Vector3 position)
        {
            return Mathf.PerlinNoise((position.z + Offset) / 211.42f, 0.53f) * 20f;
        }
    }
}