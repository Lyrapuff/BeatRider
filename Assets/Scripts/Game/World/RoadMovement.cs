using System;
using General.Audio;
using General.Behaviours;
using General.Services.GameStatus;
using General.Services.Pause;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;
using UnityEngine.VFX;

namespace Game.World
{
    public class RoadMovement : ExtendedBehaviour
    {
        public float Offset { get; private set; }

        [SerializeField] private Material[] _materials;

        private IAudioAnalyzer _audioAnalyzer;
        private GameStatusService _gameStatus;
        private PauseService _pause;
        
        private static readonly int OffsetId = Shader.PropertyToID("Vector1_4DA2A866");

        private void Awake()
        {
            _pause = Toolbox.Instance.GetService<PauseService>();
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
        }

        private void Update()
        {
            if (_pause.Paused || _gameStatus.Status == GameStatusChangeType.Crushed)
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