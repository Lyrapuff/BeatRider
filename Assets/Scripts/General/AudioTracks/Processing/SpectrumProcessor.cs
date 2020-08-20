using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using General.AudioTracks.Analyzing;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Spectrum loader")]
    public class SpectrumProcessor : TrackProcessor
    {
        public override void Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            string path = Context.Path;
            
            if (File.Exists(path + "spectrum.bytes"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                
                using (StreamReader reader = new StreamReader(path + "spectrum.bytes"))
                {
                    AnalyzedAudio analyzedAudio = formatter.Deserialize(reader.BaseStream) as AnalyzedAudio;

                    Context.AnalyzedAudio = analyzedAudio;
                    
                    OnProcessed?.Invoke(true);
                }
            }
            else
            {
                OnProcessed?.Invoke(true);
            }
        }
    }
}