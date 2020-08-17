using System;
using System.Collections;
using General.Behaviours;
using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.World.Audio
{
    public class AudioCrushedAnimation : ExtendedBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        
        private GameStatusService _gameStatus;

        private void Awake()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
        }

        private void OnEnable()
        {
            _gameStatus.OnChanged += ChangedHandle;
        }

        private void OnDisable()
        {
            _gameStatus.OnChanged -= ChangedHandle;
        }

        private void ChangedHandle(GameStatusChangeType changeType, object dataOverload)
        {
            switch (changeType)
            {
                case GameStatusChangeType.Crushed:
                    StartCoroutine(Animate(1f, 0.4f, 0.9f));
                    break;
                case GameStatusChangeType.Ended:
                    StartCoroutine(Animate(0.4f, 1f, 1.6f));
                    break;
                case GameStatusChangeType.Continued:
                    StartCoroutine(Animate(0.4f, 1f, 0.8f));
                    break;
            }
        }

        private IEnumerator Animate(float from, float to, float speed)
        {
            float time = 0f;

            while (time <= 1f)
            {
                _mixer.SetFloat("Pitch", Mathf.Lerp(from, to, time));
                
                time += Time.deltaTime * speed;

                if (speed > 0.05f)
                {
                    speed *= 0.99f;
                }

                yield return new WaitForFixedUpdate();
            }
            
            _mixer.SetFloat("Pitch", to);
        }
    }
}