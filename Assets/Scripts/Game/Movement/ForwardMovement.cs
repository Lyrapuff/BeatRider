using General.Audio;
using General.Behaviours;
using General.Services.GameStatus;
using General.Services.Pause;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.Movement
{
    public class ForwardMovement : ExtendedBehaviour
    {
        [SerializeField] private float _speed;

        private IAudioAnalyzer _audioAnalyzer;
        private GameStatusService _gameStatus;
        private PauseService _pause;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            
            _pause = Toolbox.Instance.GetService<PauseService>();
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
        }

        private void Update()
        {
            if (_pause.Paused || _gameStatus.Status == GameStatusChangeType.Crushed)
            {
                return;
            }
            
            float speed = _audioAnalyzer.Speed * 22f + _speed;
            transform.position += -Vector3.forward * (speed * Time.deltaTime);
        }
    }
}