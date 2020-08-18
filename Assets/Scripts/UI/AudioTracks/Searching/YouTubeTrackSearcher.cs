﻿using System;
using System.Collections.Generic;
using General.AudioTracks;
using General.AudioTracks.Searching;
using General.Behaviours;
using YoutubeExplode;
using YoutubeExplode.Models;
using Task = System.Threading.Tasks.Task;

namespace UI.AudioTracks.Searching
{
    public class YouTubeTrackSearcher : ExtendedBehaviour, ITrackSearcher<YoutubeSearchResult>
    {
        private IYoutubeClient _youtubeClient;
        
        private void Awake()
        {
            _youtubeClient = new YoutubeClient();
        }

        public async Task Search(string query, Action<List<YoutubeSearchResult>> onSearchCompleted)
        {
            List<YoutubeSearchResult> _tracks = new List<YoutubeSearchResult>();

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