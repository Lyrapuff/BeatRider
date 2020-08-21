using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Thumbnail downloader")]
    public class ThumbnailDownloadProcessor : TrackProcessor
    {
        public override void Process(IAudioTrack track, Action<bool> OnProcessed)
        {
            string path = Context.Path;

            if (!File.Exists(path + "thumbnail.jpeg"))
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(new Uri(track.Thumbnail), path + "thumbnail.jpeg");
                    }
                    catch
                    {
                        //ignore
                    }
                }
            }

            OnProcessed?.Invoke(true);
        }
    }
}