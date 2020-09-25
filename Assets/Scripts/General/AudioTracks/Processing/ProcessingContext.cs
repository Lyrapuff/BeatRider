using General.AudioTracks.Analyzing;
using General.AudioTracks.RoadGeneration;

namespace General.AudioTracks.Processing
{
    public class ProcessingContext
    {
        public string Path { get; set; }
        public float[] Wave { get; set; }
        public int Channels { get; set; }
        public int Samples { get; set; }
        public int Frequency { get; set; }
        public AnalyzedAudio AnalyzedAudio { get; set; }
        public Road Road { get; set; }
    }
}