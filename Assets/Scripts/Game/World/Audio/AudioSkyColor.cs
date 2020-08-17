﻿using General.Audio;
using General.Behaviours;
using UnityEngine;

namespace Game.World.Audio
{
    [RequireComponent(typeof(IAudioAnalyzer))]
    public class AudioSkyColor : ExtendedBehaviour
    {
        [SerializeField] private Material _material;

        private IAudioAnalyzer _audioAnalyzer;
        
        private static readonly int Vector123Caafb7 = Shader.PropertyToID("Vector1_23CAAFB7");

        private void Awake()
        {
            _audioAnalyzer = GetComponent<IAudioAnalyzer>();
        }

        private void Update()
        {
            float value = _material.GetFloat(Vector123Caafb7);
            float speed = _audioAnalyzer.PureSpeed * 2f;
            _material.SetFloat(Vector123Caafb7, Mathf.Lerp(value, speed + 1f, 0.05f));
        }
    }
}