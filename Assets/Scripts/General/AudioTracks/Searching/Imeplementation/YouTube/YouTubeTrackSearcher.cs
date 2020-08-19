using System;
using System.Collections.Generic;
using General.AudioTracks;
using General.AudioTracks.Searching;
using General.AudioTracks.Searching.Implementation.YouTube;
using General.Behaviours;
using UnityEngine;
using YoutubeExplode;
using YoutubeExplode.Models;
using Task = System.Threading.Tasks.Task;

namespace UI.AudioTracks.Searching.Implementation.YouTube
{
    [CreateAssetMenu(menuName = "Tracks/Searchers/YouTube")]
    public class YouTubeTrackSearcher : TrackSearcher
    {
        private IYoutubeClient _youtubeClient = new YoutubeClient();

        public override async Task Search(string query, Action<List<ISearchResult>> onSearchCompleted)
        {
            List<ISearchResult> _tracks = new List<ISearchResult>();

            IReadOnlyList<Video> videos = await _youtubeClient.SearchVideosAsync(query, 1);

            foreach (Video video in videos)
            {
                _tracks.Add(new YoutubeSearchResult
                {
                    Title = video.Title,
                    ThumbnailPath = video.Thumbnails.StandardResUrl,
                    VideoURL = video.GetUrl(),
                    Id = video.Id
                });
            }

            onSearchCompleted?.Invoke(_tracks);
        }
    }
}