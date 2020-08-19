using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace General.AudioTracks.Searching
{
    public abstract class TrackSearcher : ScriptableObject
    {
        public abstract Task Search(string query, Action<List<ISearchResult>> onSearchCompleted);
    }
}
