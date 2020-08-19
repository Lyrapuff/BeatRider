using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using General.AudioTracks.Analyzing;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Analyzer")]
    public class TrackAnalyzeProcessor : TrackProcessor
    {
        public override Task Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            string path = Context.Path;
            
            if (!File.Exists(path + "spectrum.bytes"))
            {
                AudioAnalyzer audioAnalyzer = new AudioAnalyzer();

                audioAnalyzer.Analyze(Context.Wave, Context.Channels, Context.Samples, analyzedAudio =>
                {
                    Context.AnalyzedAudio = analyzedAudio;
                    
                    OnProcessed?.Invoke(true);
                    
                    BinaryFormatter formatter = new BinaryFormatter();

                    using (FileStream file = new FileStream(path + "/spectrum.bytes", FileMode.Create))
                    {
                        formatter.Serialize(file, analyzedAudio);
                    }
                });
            }
            else
            {
                OnProcessed?.Invoke(true);
            }
            
            return Task.CompletedTask;
        }
    }
}