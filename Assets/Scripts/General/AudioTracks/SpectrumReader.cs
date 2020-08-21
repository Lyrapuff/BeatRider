using General.AudioTracks.Analyzing;
using General.Behaviours;

namespace General.AudioTracks
{
    public class SpectrumReader : ExtendedBehaviour, IAudioAnalyzer
    {
        public float SpeedMultiplier { get; set; } = 6f;
        public float Speed { get; private set; }
        public float PureSpeed { get; private set; }
        public float[] Band { get; private set;}

        /*
        
        private void Update()
        {
            if (_audioPlayer.Track != null)
            {
                int index = _audioPlayer.GetIndexFromTime() / 1024 / _audioPlayer.Track.AnalyzedAudio.StoreEvery;

                float average = _audioPlayer.Track.AnalyzedAudio.Averages[index];

                Band = _audioPlayer.Track.AnalyzedAudio.Bands[index];
                
                Speed = average * SpeedMultiplier + 1f;
                PureSpeed = average;
            }
        }
        
        */
    }
}