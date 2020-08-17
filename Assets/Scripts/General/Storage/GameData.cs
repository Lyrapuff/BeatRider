using System;
using System.Collections.Generic;
using General.AudioTracks;

namespace General.Storage
{
    [Serializable]
    public class GameData
    {
        public bool MusicEnabled { get; set; } = true;
        public bool VibrationEnabled { get; set; } = true;
        public List<AudioTrack> TrackHistory { get; set; } = new List<AudioTrack>();
        public int Points { get; set; } = 0;
    }
}