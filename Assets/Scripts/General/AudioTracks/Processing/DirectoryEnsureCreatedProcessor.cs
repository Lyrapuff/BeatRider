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
        public override Task Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            string path = Application.persistentDataPath + "/playlist/" + searchResult.Id + "/";
            
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Context.Path = path;
            
            OnProcessed?.Invoke(true);
            
            return Task.CompletedTask;
        }
    }
}