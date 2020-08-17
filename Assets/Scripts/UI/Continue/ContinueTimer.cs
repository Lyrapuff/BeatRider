using System;
using System.Collections;
using General.Behaviours;
using General.Services.GameStatus;
using General.UI.CanvasManagement;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace UI.Continue
{
    public class ContinueTimer : ExtendedBehaviour
    {
        public Action<float> OnTick { get; set; }

        [SerializeField] private float _duration;

        private GameStatusService _gameStatus;
        private ICanvasSwitcher _canvasSwitcher;
        private Coroutine _timer;
        
        private void OnEnable()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            
            _canvasSwitcher = FindComponentOfInterface<ICanvasSwitcher, NullCanvasSwitcher>();
            
            _timer = StartCoroutine(Timer());
        }

        private void OnDisable()
        {
            if (_timer != null)
            {
                StopCoroutine(_timer);
            }
        }

        private IEnumerator Timer()
        {
            float time = 0f;

            while (time < _duration)
            {
                OnTick?.Invoke(time / _duration);
                time += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            
            _gameStatus.Change(GameStatusChangeType.Ended, null);
            _canvasSwitcher.Open("GameOver");
        }
    }
}