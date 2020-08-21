using System;
using System.IO;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Directory ensure created")]
    public class DirectoryEnsureCreatedProcessor : TrackProcessor
    {
        public override void Process(IAudioTrack track, Action<bool> OnProcessed)
        {
            Context.Path += track.Id + "/";

            if (!Directory.Exists(Context.Path))
            {
                Directory.CreateDirectory(Context.Path);
            }
            
            OnProcessed?.Invoke(true);
        }
    }
}