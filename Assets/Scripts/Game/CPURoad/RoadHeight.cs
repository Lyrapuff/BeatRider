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
        private float[] _road;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
            IStorage storage = FindComponentOfInterface<IStorage>();
            _road = storage.Get<float[]>("Game/Road");
        }

        private void Update()
        {
            Offset += Time.deltaTime * _audioAnalyzer.Speed * 20f;
            Debug.ClearDeveloperConsole();
            Debug.Log(Offset);
        }

        public float GetHeight(Vector3 position)
        {
            return Mathf.PerlinNoise((position.z + Offset) / 80f, 0.53f) * 10f;
        }
    }
}