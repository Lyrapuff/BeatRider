using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.AudioTracks.Searching
{
    public interface ITrackSearcher
    {
        Task Search(string query, Action<List<AudioTrack>> onSearchCompleted);
    }
}
