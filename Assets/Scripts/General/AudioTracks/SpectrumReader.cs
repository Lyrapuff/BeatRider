using General.Audio;
using General.Behaviours;
using UnityEngine;

namespace General.AudioTracks
{
    [RequireComponent(typeof(AudioPlayer))]
    public class SpectrumReader : ExtendedBehaviour, IAudioAnalyzer
    {
        public float SpeedMultiplier { get; set; } = 6f;
        public float Speed { get; private set; }
        public float PureSpeed { get; private set; }
        
        private AudioPlayer _player;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _player = GetComponent<AudioPlayer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_player.Track != null)
            {
                float average = _player.Track.AnalyzedAudio.Averages[GetIndexFromTime() / 1024 / _player.Track.AnalyzedAudio.StoreEvery];
                
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