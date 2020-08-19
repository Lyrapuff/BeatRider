using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using General.AudioTracks.Processing;
using General.Storage.Cache;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General.AudioTracks
{
    public class AudioPlaylist : MonoBehaviour
    {
        public Action OnInitialized { get; set; }
        
        [SerializeField] private ProcessingPipeline _pipeline;

        private List<AudioTrack> _tracks = new List<AudioTrack>();
        private AudioTrack _prepared;

        private void Awake()
        {
            IndexTracks();
            
            Prepare();
        }

        private void OnEnable()
        {
            _tracks = SessionCache.Instance.Get<List<AudioTrack>>("tracks") ?? _tracks;
        }

        private void OnDisable()
        {
            SessionCache.Instance.Set("tracks", _tracks);
        }

        private void IndexTracks()
        {
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
        }

        public AudioTrack GetRandom()
        {
            AudioTrack track = _prepared;
            Prepare();
            return track;
        }

        private void Prepare()
        {
            int count = _tracks.Count;
            
            if (count < 1)
            {
                return;
            }

            AudioTrack track = _tracks[Random.Range(0, count)];

            if (track != null)
            {
                _pipeline.Process(track);
                StartCoroutine(PrepareFinish(track));
            }
        }

        private IEnumerator PrepareFinish(AudioTrack track)
        {
            while (_pipeline.Status == ProcessingStatus.Processing)
            {
                if (_pipeline.Status == ProcessingStatus.Error)
                {
                    yield return null;
                }

                yield return new WaitForFixedUpdate();
            }

            ProcessingContext context = _pipeline.Context;

            AudioClip clip = AudioUtil.AssembleClip(context);

            track.AudioClip = clip;
            track.AnalyzedAudio = context.AnalyzedAudio;

            if (_prepared == null)
            {
                _prepared = track;
                OnInitialized?.Invoke();
                yield return null;
            }

            _prepared = track;
        }
    }
}