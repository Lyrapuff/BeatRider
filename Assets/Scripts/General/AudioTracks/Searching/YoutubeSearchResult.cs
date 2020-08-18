using System;

namespace General.AudioTracks.Searching
{
    [Serializable]
    public class YoutubeSearchResult : ISearchResult
    {
        public string Title { get; set; }
        public string ThumbnailPath { get; set; }
        public string VideoURL { get; set; }
        public string Id { get; set; }
    }
}