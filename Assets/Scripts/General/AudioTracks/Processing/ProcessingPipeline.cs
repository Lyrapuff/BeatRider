using System;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [Serializable]
    public class ProcessingPipeline
    {
        public ProcessingStatus Status { get; private set; } = ProcessingStatus.Idle;
        public ProcessingContext Context { get; private set; } = new ProcessingContext();

        [SerializeField] private TrackProcessor[] _processors;

        private int _processorIndex;

        public void Process(ISearchResult searchResult)
        {
            if (Status == ProcessingStatus.Idle)
            {
                Status = ProcessingStatus.Processing;
                _processorIndex = 0;
            }
            
            if (_processorIndex < _processors.Length)
            {
                TrackProcessor processor = _processors[_processorIndex];

                processor.Context = Context;

                processor.Process(searchResult, success =>
                {
                    if (success)
                    {
                        _processorIndex++;
                        Process(searchResult);
                    }
                    else
                    {
                        Status = ProcessingStatus.Error;
                        Debug.Log("Error: " + _processorIndex);
                        // TODO: Error message
                    }
                });
            }
            else
            {
                Status = ProcessingStatus.Success;
            }
        }
    }

    public enum ProcessingStatus
    {
        Idle,
        Processing,
        Success,
        Error
    }
}