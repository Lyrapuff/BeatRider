using System.Collections;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.World
{
    public class ContinueWorldAnimation : ExtendedBehaviour
    {
        private IAudioAnalyzer _audioAnalyzer;
        private GameStatusService _gameStatus;
        
        private void Start()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();

            _gameStatus.OnChanged += HandleChanged;
        }

        private void HandleChanged(GameStatusChangeType changeType, object dataOverload)
        {
            if (changeType == GameStatusChangeType.Continued)
            {
                StartCoroutine(Animate());
            }
        }

        private IEnumerator Animate()
        {
            float time = 0f;
            float speed = _audioAnalyzer.SpeedMultiplier;
            float speedN = -_audioAnalyzer.SpeedMultiplier;

            while (time <= 1f)
            {
                _audioAnalyzer.SpeedMultiplier = Mathf.Lerp(speedN, 0f, time);
                
                time += Time.fixedDeltaTime * 0.7f;
                yield return new WaitForFixedUpdate();
            }

            _audioAnalyzer.SpeedMultiplier = speed;
        }
    }
}