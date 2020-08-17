using System;
using General.Behaviours;
using UnityEngine;
using UnityEngine.Video;

namespace Game
{
    public class RepetitiveGame : ExtendedBehaviour
    {
        public static Action<int> OnRepeat { get; set; }
        
        private AudioSource _audioSource;
        private int _count = 1;

        private void Awake()
        {
            _audioSource = FindObjectOfType<AudioSource>();
        }

        private void Update()
        {
            if (_audioSource.timeSamples >= _audioSource.clip.samples)
            {
                EndReached();
            }
        }

        private void EndReached()
        {
            _audioSource.Play();
            _count++;
            
            OnRepeat?.Invoke(_count);
        }
    }
}