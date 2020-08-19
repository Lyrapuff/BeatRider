using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.World.Audio
{
    [RequireComponent(typeof(IAudioAnalyzer))]
    public class AudioSpeed : ExtendedBehaviour
    {
        public static float Speed;

        private GameSettingsService _gameSettings;
        private IAudioAnalyzer _audioAnalyzer;

        private void Awake()
        {
            _gameSettings = Toolbox.Instance.GetService<GameSettingsService>();
            
            _audioAnalyzer = GetComponent<IAudioAnalyzer>();
            
            SetSpeed();
        }

        private void SetSpeed()
        {
            Configuration configuration = _gameSettings.Configuration;
            
            float mul = 0f;
                
            switch (configuration?.GameSpeed)
            {
                case Configuration.GameSpeedType.Slow:
                    mul = 3f;
                    break;
                case Configuration.GameSpeedType.Normal:
                    mul = 6f;
                    break;
                case Configuration.GameSpeedType.Fast:
                    mul = 8f;
                    break;
            }

            Speed = mul;
                
            _audioAnalyzer.SpeedMultiplier = mul;
        }
    }
}