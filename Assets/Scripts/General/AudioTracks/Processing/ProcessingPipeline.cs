using System;
using System.Threading.Tasks;
using General.AudioTracks.Processing.Processors;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [Serializable]
    public class ProcessingPipeline
    {
        public Action<ProcessingContext> OnProcessed { get; set; }
        
        [SerializeField] private TrackProcessor[] _processors;

        private ProcessingContext _context;
        private int _processorIndex;

        public Task Process(IAudioTrack track, string path)
        {
            try
            {
                if (_processorIndex == 0)
                {
                    _context = new ProcessingContext();
                    _context.Path = path;

                    _processorIndex = 0;
                }

                if (_processorIndex < _processors.Length)
                {
                    TrackProcessor processor = _processors[_processorIndex];
                    processor.Context = _context;

                    processor.Process(track, success =>
                    {
                        if (success)
                        {
                            _processorIndex++;
                            Process(track, path);
                        }
                        else
                        {
                            Debug.Log("Error: " + _processorIndex);
                            // TODO: Error message
                        }
                    });
                }
                else
                {
                    _processorIndex = 0;
                    OnProcessed?.Invoke(_context);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return Task.CompletedTask;
        }
    }
}