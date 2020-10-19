using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using General.AudioTracks.Analyzing;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Analyzer")]
    public class TrackAnalyzeProcessor : TrackProcessor
    {
        public override void Process(IAudioTrack track, Action<bool> OnProcessed)
        {
            string path = Context.Path;
            
            if (!File.Exists(path + "spectrum.bytes"))
            {
                AudioAnalyzer audioAnalyzer = new AudioAnalyzer();

                AnalyzedAudio analyzedAudio = audioAnalyzer.Analyze(Context.Wave, Context.Channels, Context.Samples, Context.Frequency);

                Context.AnalyzedAudio = analyzedAudio;
                
                OnProcessed?.Invoke(true);
                    
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream file = new FileStream(path + "/spectrum.bytes", FileMode.Create))
                {
                    formatter.Serialize(file, analyzedAudio);
                }
            }
            else
            {
                OnProcessed?.Invoke(true);
            }
        }
    }
}