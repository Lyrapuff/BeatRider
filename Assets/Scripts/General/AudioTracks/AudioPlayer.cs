using UnityEngine;

namespace General.AudioTracks
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        public AudioTrack Track { get; private set; }
        
        [SerializeField] private AudioPlaylist _playlist;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _playlist.OnInitialized += HandleInitialized;
        }

        private void OnDisable()
        {
            _playlist.OnInitialized -= HandleInitialized;
        }

        private void HandleInitialized()
        {
            AudioTrack track = _playlist.GetRandom();
            
            _audioSource.clip = track.AudioClip;
            _audioSource.Play();

            Track = track;
        }
    }
}