﻿using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;

namespace UI.PCUI.AudioTracks.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Thumbnail downloader")]
    public class ThumbnailDownloadProcessor : TrackProcessor
    {
        public override Task Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            string path = Context.Path;

            if (!File.Exists(path + "spectrum.bytes"))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(searchResult.Thumbnail), path + "thumbnail.jpeg");
                }
            }

            OnProcessed?.Invoke(true);
            
            return Task.CompletedTask;
        }
    }
}