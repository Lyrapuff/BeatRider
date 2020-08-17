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
        public int Points { get; set; } = 0;
    }
}