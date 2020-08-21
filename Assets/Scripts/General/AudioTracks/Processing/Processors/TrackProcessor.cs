using System;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    public abstract class TrackProcessor : ScriptableObject
    {
        public ProcessingContext Context { get; set; }
        
        public abstract void Process(IAudioTrack track, Action<bool> OnProcessed);
    }
}