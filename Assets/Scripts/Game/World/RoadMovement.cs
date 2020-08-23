using Game.Services;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;

namespace Game.World
{
    public class RoadMovement : ExtendedBehaviour
    {
        public float Offset { get; private set; }

        [SerializeField] private Material[] _materials;

        private IAudioAnalyzer _audioAnalyzer;
        private IPause _pause;
        
        private static readonly int OffsetId = Shader.PropertyToID("Vector1_4DA2A866");

        private void Awake()
        {
            _pause = FindComponentOfInterface<IPause>();
            
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
        }

        private void Update()
        {
            if (_pause.Paused)
            {
                return;
            }
            
            Offset += _audioAnalyzer.Speed * Time.deltaTime;

            foreach (Material material in _materials)
            {
                material.SetFloat(OffsetId, Offset);
            }
        }
    }
}