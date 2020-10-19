using System;
using System.Collections.Generic;

namespace General.AudioTracks.Analyzing
{
    [Serializable]
    public class AnalyzedAudio
    {
        public int Skip = 0;
        public int Take = 5;
        public int StoreEvery = 4;
        public List<float[]> Bands = new List<float[]>();
        public List<float> Averages = new List<float>();
        public List<float> Beats = new List<float>();
    }
}