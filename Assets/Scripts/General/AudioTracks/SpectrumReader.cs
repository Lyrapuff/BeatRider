using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace General.AudioTracks
{
    [RequireComponent(typeof(AudioSource))]
    public class SpectrumReader : ExtendedBehaviour, IAudioAnalyzer
    {
        public float SpeedMultiplier { get; set; } = 6f;
        public float Speed { get; private set; }
        public float PureSpeed { get; private set; }
        public float[] Band { get; private set;}

        private AudioSource _audioSource;
        private AudioTrack _track;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            IStorage storage = FindComponentOfInterface<IStorage>();
            _track = storage.Get<AudioTrack>("Game/Track");

            _audioSource.clip = _track?.AudioClip;
            _audioSource.Play();
        }
        
        private void Update()
        {
            if (_track != null)
            {
                int index = GetIndexFromTime(_audioSource.time) / 1024 / _track.AnalyzedAudio.StoreEvery;

                float average = _track.AnalyzedAudio.Averages[index];

                Band = _track.AnalyzedAudio.Bands[index];
                
                Speed = average * SpeedMultiplier + 1f;
                
                PureSpeed = average;
            }
        }
        
        public int GetIndexFromTime(float time = 0f)
        {
            float lengthPerSample = _audioSource.clip.length / _audioSource.clip.samples;
            return Mathf.FloorToInt (time / lengthPerSample);
        }
    }
}