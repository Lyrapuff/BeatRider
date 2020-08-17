using System;
using System.Collections.Generic;

namespace General.Audio
{
    [Serializable]
    public class AnalyzedAudioObject
    {
        public int StoreEvery = 4;
        public int Take = 5;
        public float Min;
        public float Max;
        public List<float> Averages = new List<float>();
    }
}