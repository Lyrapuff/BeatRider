using System;
using System.Linq;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.Services.GameStatus;
using UnityEngine;

namespace Game.World.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RealtimeAudioAnalyzer : ExtendedBehaviour, IAudioAnalyzer
    {
        public float SpeedMultiplier { get; set; } = 0f;
        public float Speed => SpeedMultiplier;
        public float PureSpeed => SpeedMultiplier;
        public float[] Band { get; }

        [SerializeField] private int _skip;
        [SerializeField] private int _take;
        
        private IGameStatus _gameStatus;
        private AudioSource _audioSource;
        private float[] _spectrum = new float[512];
        private float[] _bands = new float[8];
        private float _internalMul;

        private void Awake()
        {
            _gameStatus = FindComponentOfInterface<IGameStatus, NullGameStatus>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _gameStatus.OnChanged += HandleGameStatusChanged;
        }

        private void HandleGameStatusChanged(GameStatusChangeType changeType, object dataOverload)
        {
            if (changeType == GameStatusChangeType.Crushed)
            {
                _internalMul = 0f;
            }
            else
            {
                _internalMul = 1f;
            }
        }

        private void Update()
        {
            if (!_audioSource.isPlaying)
            {
                return;
            }

            _audioSource.GetSpectrumData(_spectrum, 0, FFTWindow.Hanning);
            
            int count = 0;
            
            for (int i = 0; i < _bands.Length; i++)
            {
                float sampleCount = (int) Mathf.Pow(2, i + 1);

                float averageValue = 0f;
                
                for (int k = 0; k < sampleCount; k++)
                {
                    averageValue += _spectrum[count] * (count + 1);
                    count++;
                }

                averageValue /= count;

                _bands[i] = Mathf.Lerp( _bands[i], averageValue, 0.1f);
            }
        }
    }
}