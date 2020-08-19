using General.AudioTracks.Processing;
using UnityEngine;

namespace General.AudioTracks
{
    public static class AudioUtil
    {
        public static AudioClip AssembleClip(float[] wave, int samples, int frequency)
        {
            AudioClip clip = AudioClip.Create("test", samples, 2, frequency, false);
            clip.SetData(wave, 0);
            return clip;
        }
        
        public static AudioClip AssembleClip(ProcessingContext context)
        {
            return AssembleClip(context.Wave, context.Samples, context.Frequency);
        }
    }
}