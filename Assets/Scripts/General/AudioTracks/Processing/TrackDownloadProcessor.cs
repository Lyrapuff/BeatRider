using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using UnityEngine;
using YouTubeSearch;

namespace General.AudioTracks.Processing
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Audio downloader")]
    public class TrackDownloadProcessor : TrackProcessor
    {
        public override void Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            string url = $"https://www.youtube.com/watch?v={searchResult.Id}";
            
            string path = Context.Path;

            if (!File.Exists(path + "audio.m4a"))
            {
                IEnumerable<VideoInfo> infos = DownloadUrlResolver.GetDownloadUrls(url);

                VideoInfo info = infos
                    .Where(i => i.VideoType != VideoType.Mp4)
                    .OrderByDescending(i => i.AudioBitrate)
                    .FirstOrDefault();

                if (info == null)
                {
                    OnProcessed?.Invoke(false);
                    return;
                }

                if (info.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(info);
                }

                VideoDownloader downloader = new VideoDownloader();
                downloader.DownloadFile(info.DownloadUrl, "audio", false, path, info.VideoExtension);

                downloader.OnDownloaded = () =>
                {
                    OnProcessed?.Invoke(true);
                };
            }
            else
            {
                OnProcessed?.Invoke(true);
            }
        }
    }
}