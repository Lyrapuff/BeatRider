using Game.Services;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;

namespace Entities.Movement
{
    public class ForwardMovement : ExtendedBehaviour
    {
        [SerializeField] private float _speed;

        private IAudioAnalyzer _audioAnalyzer;
        private IPause _pause;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            
            _pause = FindComponentOfInterface<IPause>();
        }

        private void Update()
        {
            if (_pause.Paused)
            {
                return;
            }
            
            transform.position += -Vector3.forward * (_audioAnalyzer.Speed * Time.deltaTime);
        }
    }
}