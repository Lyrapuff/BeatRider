using UnityEngine;

namespace General.AudioTracks.Analyzing
{
    public class NullAudioAnalyzer : IAudioAnalyzer
    {
        public float SpeedMultiplier { get; set; }
        public float Speed { get; }
        public float PureSpeed { get; }
        public float[] Band { get; }
        public float GetSpeedAtPoint(float point)
        {
            throw new System.NotImplementedException();
        }

        public NullAudioAnalyzer()
        {
            Debug.LogError(typeof(NullAudioAnalyzer) + " is being used. Please, replace it with a real implementation.");
        }
    }
}