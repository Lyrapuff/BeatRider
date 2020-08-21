using General.AudioTracks.Analyzing;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks
{
    public class AudioTrack : IAudioTrack
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Thumbnail { get; set; }
        public AudioClip AudioClip { get; set; }
        public AnalyzedAudio AnalyzedAudio { get; set; }
    }
}