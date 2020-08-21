﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [Serializable]
    public class ProcessingPipeline
    {
        public ProcessingStatus Status { get; private set; } = ProcessingStatus.Idle;
        public ProcessingContext Context { get; private set; }

        [SerializeField] private TrackProcessor[] _processors;

        private static string _path = Application.persistentDataPath + "/playlist/";
        private int _processorIndex;

        public Task Process(AudioTrack track)
        {
            try
            {
                if (Status == ProcessingStatus.Idle)
                {
                    Status = ProcessingStatus.Processing;
                    Context = new ProcessingContext();
                    Context.Path = _path;
                    
                    _processorIndex = 0;
                }

                if (_processorIndex < _processors.Length)
                {
                    TrackProcessor processor = _processors[_processorIndex];
                    processor.Context = Context;

                    processor.Process(track, success =>
                    {
                        if (success)
                        {
                            _processorIndex++;
                            Process(track);
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
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return Task.CompletedTask;
        }

        public void Reset()
        {
            Status = ProcessingStatus.Idle;
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