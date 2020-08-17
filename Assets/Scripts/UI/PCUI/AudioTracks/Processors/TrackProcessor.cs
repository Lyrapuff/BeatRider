using System;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace UI.PCUI.AudioTracks.Processors
{
    public abstract class TrackProcessor : ScriptableObject
    {
        public ProcessingContext Context { get; set; }
        
        public abstract Task Process(ISearchResult searchResult, Action<bool> OnProcessed);
    }
}