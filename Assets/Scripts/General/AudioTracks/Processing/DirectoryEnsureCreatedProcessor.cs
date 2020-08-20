using System;
using System.IO;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Directory ensure created")]
    public class DirectoryEnsureCreatedProcessor : TrackProcessor
    {
        public override void Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            Context.Path += searchResult.Id + "/";

            if (!Directory.Exists(Context.Path))
            {
                Directory.CreateDirectory(Context.Path);
            }
            
            OnProcessed?.Invoke(true);
        }
    }
}