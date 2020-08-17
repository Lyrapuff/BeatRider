using System.Collections;
using General.Behaviours;
using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.World.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SmoothRestart : ExtendedBehaviour
    {
        [SerializeField] private float _animationDuration;

        private GameStatusService _gameStatus;
        private AudioSource _audioSource;

        private void Awake()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _gameStatus.OnChanged += HandleGameStatusChanged;
        }

        private void HandleGameStatusChanged(GameStatusChangeType changeType, object dataOverload)
        {
            if (changeType == GameStatusChangeType.Started)
            {
                StartCoroutine(RestartAnimation());
            }
        }

        private IEnumerator RestartAnimation()
        {
            yield return AnimateVolume(true);
            _audioSource.Stop();
            _audioSource.Play();
            yield return AnimateVolume(false);
        }

        private IEnumerator AnimateVolume(bool oneMinus)
        {
            float time = 0f;

            while (time <= _animationDuration)
            {
                float volume = Mathf.InverseLerp(0f, _animationDuration, time);
                volume = oneMinus ? 1f - volume : volume;
                _audioSource.volume = volume;
                
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _audioSource.volume = oneMinus ? 0f : 1f;
        }
    }
}