using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace General.AudioTracks.Searching
{
    public interface ITrackSearcher<T> where T : ISearchResult
    {
        Task Search(string query, Action<List<T>> onSearchCompleted);
    }
}
