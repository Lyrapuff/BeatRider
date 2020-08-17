using System;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private Material _material;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_audioSource.isPlaying)
            {
                _material.SetInt("_Offset", GetIndexFromTime(_audioSource.time) / 1024 / 2);
            }
        }
        
        private int GetIndexFromTime(float time) 
        {
            float lengthPerSample = _audioSource.clip.length / _audioSource.clip.samples;
            return Mathf.FloorToInt (time / lengthPerSample);
        }
    }
}