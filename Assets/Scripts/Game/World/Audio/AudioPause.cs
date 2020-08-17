using System;
using General.Behaviours;
using General.Services.Pause;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;
using UnityEngine.Video;

namespace Game.World.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPause : ExtendedBehaviour
    {
        private PauseService _pause;
        private AudioSource _audioSource;

        private void Awake()
        {
            _pause = Toolbox.Instance.GetService<PauseService>();
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