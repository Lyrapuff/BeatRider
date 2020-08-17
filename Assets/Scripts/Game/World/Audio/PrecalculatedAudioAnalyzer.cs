﻿using System.Collections.Generic;
using General.Audio;
using General.Behaviours;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.World.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PrecalculatedAudioAnalyzer : ExtendedBehaviour, IAudioAnalyzer
    {
        public List<float> Averages { get; set; }
        public float SpeedMultiplier { get; set; } = 6f;
        public float Speed { get; private set; }
        public float PureSpeed { get; private set; }

        private GameSettingsService _gameSettings;
        private AudioSource _audioSource;

        private void Awake()
        {
            _gameSettings = Toolbox.Instance.GetService<GameSettingsService>();
            
            _audioSource = GetComponent<AudioSource>();

            Averages = _gameSettings.AnalyzedAudio.Averages;
            _audioSource.clip = _gameSettings.Clip;
            _audioSource.Play();
        }
        
        private void Update()
        {
            if (Averages != null)
            {
                float average = Averages[GetIndexFromTime() / 1024 / _gameSettings.AnalyzedAudio.StoreEvery];
                
                Speed = average * SpeedMultiplier + 1f;
                PureSpeed = average;
            }
        }
        
        public int GetIndexFromTime()
        {
            float time = _audioSource.time;
            float lengthPerSample = _audioSource.clip.length / _audioSource.clip.samples;
            return Mathf.FloorToInt (time / lengthPerSample);
        }
    }
}