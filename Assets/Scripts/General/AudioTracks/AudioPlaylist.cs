using System.Collections;
using System.Collections.Generic;
using System.IO;
using General.AudioTracks.Processing;
using General.Behaviours;
using UnityEngine;

namespace General.AudioTracks
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlaylist : SingletonBehaviour<AudioPlaylist>
    {
        public AudioTrack Track { get; private set; }
        
        [SerializeField] private ProcessingPipeline _pipeline;

        private AudioSource _audioSource;
        private List<AudioTrack> _tracks = new List<AudioTrack>();

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            
            string path = Application.persistentDataPath + "/playlist";

            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            DirectoryInfo playlistDirectory = new DirectoryInfo(path);
            
            foreach (DirectoryInfo trackDirectory in playlistDirectory.GetDirectories())
            {
                AudioTrack track = new AudioTrack
                {
                    Id = trackDirectory.Name
                };
                
                _tracks.Add(track);
            }
            
            PlayRandom();
        }

        private void PlayRandom()
        {
            if (_tracks.Count < 1)
            {
                return;
            }
            
            AudioTrack track = _tracks[Random.Range(0, _tracks.Count)];

            if (track.AudioClip == null)
            {
                track.Process(_pipeline);
                StartCoroutine(PlayAfterProcessing(track));
            }
        }

        private IEnumerator PlayAfterProcessing(AudioTrack track)
        {
            while (_pipeline.Status != ProcessingStatus.Success)
            {
                if (_pipeline.Status == ProcessingStatus.Error)
                {
                    yield return null;
                }
                
                yield return new WaitForFixedUpdate();
            }

            ProcessingContext context = _pipeline.Context;
                
            AudioClip clip = AudioClip.Create("test", context.Samples, 2, context.Frequency, false);
            clip.SetData(context.Wave, 0);
            
            track.AudioClip = clip;
            
            _audioSource.clip = track.AudioClip;
            _audioSource.Play();

            Track = track;
        }
    }
}