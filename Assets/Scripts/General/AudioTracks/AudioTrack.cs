using General.Audio;
using UnityEngine;

namespace General.AudioTracks
{
    public class AudioTrack
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public AudioClip AudioClip { get; set; }
        public AnalyzedAudio AnalyzedAudio { get; set; }
        public Sprite Thumbnail { get; set; }
        
        
    }
}