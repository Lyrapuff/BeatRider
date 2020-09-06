using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Title store")]
    public class TitleEnsureStoredProcessor : TrackProcessor
    {
        public override void Process(IAudioTrack track, Action<bool> OnProcessed)
        {
            string fileName = Context.Path + "title";

            if (!File.Exists(fileName))
            {
                using (FileStream file = File.Create(fileName))
                {
                    byte[] data = Encoding.UTF8.GetBytes(track.Title);
                    file.Write(data, 0, data.Length);
                }
            }
            
            OnProcessed?.Invoke(true);
        }
    }
}