using Game.Services;
using General.Behaviours;
using UnityEngine;

namespace Game.World.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPause : ExtendedBehaviour
    {
        private IPause _pause;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _pause = FindComponentOfInterface<IPause>();
            
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _pause.OnPaused += PausedHandle;
            _pause.OnUnpaused += UnpausedHandle;
        }

        private void OnDisable()
        {
            _pause.OnPaused -= PausedHandle;
            _pause.OnUnpaused -= UnpausedHandle;
        }

        private void PausedHandle()
        {
            _audioSource.Pause();
        }
        
        private void UnpausedHandle()
        {
            _audioSource.Play();
        }
    }
}