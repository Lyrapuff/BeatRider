using System;
using General.AudioTracks;
using UI.PCUI.AudioTracks.Processors;
using UnityEngine;

namespace UI.PCUI.AudioTracks
{
    [RequireComponent(typeof(TrackElement))]
    public class TrackButton : MonoBehaviour
    {
        public Action<AudioTrack> OnProcessed { get; set; }
        
        [SerializeField] private TrackProcessor[] _processors;

        private ProcessingContext _context;
        private TrackElement _trackElement;
        private int _processorIndex = -1;

        private void Awake()
        {
            _trackElement = GetComponent<TrackElement>();
        }

        public void Interact()
        {
            if (_processorIndex < 0)
            {
                _context = new ProcessingContext();
                _processorIndex = 0;
            }
            
            if (_processorIndex < _processors.Length)
            {
                TrackProcessor processor = _processors[_processorIndex];
                processor.Context = _context;
                processor.Process(_trackElement.SearchResult, success =>
                {
                    if (success)
                    {
                        _processorIndex++;
                        Interact();
                    }
                    else
                    {
                        Debug.Log("Error: " + _processorIndex);
                        // TODO: Error message
                    }
                });
            }
        }

        private void Update()
        {
            if (_processorIndex >= _processors.Length && _processorIndex >= 0)
            {
                _processorIndex = -1;
                
                AudioClip clip = AudioClip.Create("test", _context.Samples, 2, _context.Frequency, false);
                clip.SetData(_context.Wave, 0);

                AudioTrack track = new AudioTrack
                {
                    Title = _trackElement.SearchResult.Title,
                    Id = _trackElement.SearchResult.Id,
                    AudioClip = clip,
                    AnalyzedAudio = _context.AnalyzedAudio
                };
                
                OnProcessed?.Invoke(track);
            }
        }
    }
}