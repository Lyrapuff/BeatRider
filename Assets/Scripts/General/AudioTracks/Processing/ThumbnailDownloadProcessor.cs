using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Thumbnail downloader")]
    public class ThumbnailDownloadProcessor : TrackProcessor
    {
        public override Task Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            string path = Context.Path;

            if (!File.Exists(path + "thumbnail.jpeg"))
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(new Uri(searchResult.ThumbnailPath), path + "thumbnail.jpeg");
                    }
                    catch
                    {
                        //ignore
                    }
                }
            }

            OnProcessed?.Invoke(true);
            
            return Task.CompletedTask;
        }
    }
}