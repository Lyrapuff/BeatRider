using UnityEngine;

namespace General.Audio
{
    public class NullAudioAnalyzer : IAudioAnalyzer
    {
        public float SpeedMultiplier { get; set; }
        public float Speed { get; }
        public float PureSpeed { get; }

        public NullAudioAnalyzer()
        {
            Debug.LogError(typeof(NullAudioAnalyzer) + " is being used. Please, replace it with a real implementation.");
        }
    }
}