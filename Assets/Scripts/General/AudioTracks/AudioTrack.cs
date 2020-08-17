using System;

namespace General.AudioTracks
{
    [Serializable]
    public class AudioTrack
    {
        public string Title { get; set; }
        public string PreviewURL { get; set; }
        public string VideoURL { get; set; }
        public string Id { get; set; }
    }
}