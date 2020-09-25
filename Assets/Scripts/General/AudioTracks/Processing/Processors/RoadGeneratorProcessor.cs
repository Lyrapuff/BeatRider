using System;
using General.AudioTracks.RoadGeneration;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Road generator")]
    public class RoadGeneratorProcessor : TrackProcessor
    {
        public override void Process(IAudioTrack track, Action<bool> OnProcessed)
        {
            if (Context.AnalyzedAudio == null)
            {
                Debug.LogError("RoadGeneratorProcessor should be called after audio was analyzed.", this);
                OnProcessed?.Invoke(false);
                return;
            }

            RoadGenerator roadGenerator = new RoadGenerator();
            Road road = roadGenerator.Generate(Context.AnalyzedAudio, (float) Context.Samples / Context.Frequency,
                Context.Samples);

            Context.Road = road;
                
            OnProcessed?.Invoke(true);
        }
    }
}