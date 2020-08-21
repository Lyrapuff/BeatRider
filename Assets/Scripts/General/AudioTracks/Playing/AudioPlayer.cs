using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using General.AudioTracks.Processing;
using UnityEngine;

namespace General.AudioTracks.Playing
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        public AudioTrack Track { get; private set; }

        [SerializeField] private ProcessingPipeline _preparingPipeline;

        private AudioSource _audioSource;
        private readonly List<AudioTrack> _playlist = new List<AudioTrack>();

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            LoadPlaylist();
        }

        private void LoadPlaylist()
        {
            string path = Application.persistentDataPath + "/playlist";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            DirectoryInfo directory = new DirectoryInfo(path);
            
            foreach (DirectoryInfo playlistDirectory in directory.GetDirectories())
            {
                AudioTrack track = new AudioTrack
                {
                    Id = playlistDirectory.Name
                };
                
                _playlist.Add(track);
            }
        }

        private IEnumerator PlayAfterPreparing(AudioTrack track)
        {
            while (_preparingPipeline.Status == ProcessingStatus.Processing)
            {
                if (_preparingPipeline.Status == ProcessingStatus.Error)
                {
                    yield return null;
                }
                
                yield return new WaitForFixedUpdate();
            }

            _preparingPipeline.Reset();
            
            track.AnalyzedAudio = _preparingPipeline.Context.AnalyzedAudio;
            track.AudioClip = AudioUtil.AssembleClip(_preparingPipeline.Context);

            Track = track;
            Play();
        }

        public void Prepare(AudioTrack track)
        {
            Task.Run(() => _preparingPipeline.Process(track)).ConfigureAwait(false);
        }

        public void PrepareAndPlay(AudioTrack track)
        {
            Prepare(track);
            StartCoroutine(PlayAfterPreparing(track));
        }

        public void AddAndPlay(AudioTrack track)
        {
            Add(track);
            Track = track;
            Play();
        }
        
        public void Add(AudioTrack track)
        {
            _playlist.Add(track);
        }

        public void Play()
        {
            if (Track == null)
            {
                return;
            }
            
            if(Track.AudioClip == null)
            {
                return;
            }

            _audioSource.clip = Track.AudioClip;
            _audioSource.Play();
        }

        public void PlayRandom()
        {
            AudioTrack track = _playlist[UnityEngine.Random.Range(0, _playlist.Count)];

            if (track.AudioClip == null)
            {
                PrepareAndPlay(track);
            }
            else
            {
                Track = track;
                Play();
            }
        }

        public void EnsurePlaying()
        {
            if (Track == null)
            {
                PlayRandom();
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