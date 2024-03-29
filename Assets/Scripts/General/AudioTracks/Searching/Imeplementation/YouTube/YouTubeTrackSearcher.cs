﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using General.AudioTracks.Searching;
using General.AudioTracks.Searching.Implementation.YouTube;
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
            
            string pattern = @".*\?v=(.*)";
            
            foreach (Match m in Regex.Matches(query, pattern))
            {
                Video video = await _youtubeClient.GetVideoAsync(m.Groups[1].Value);
                
                _tracks.Add(new YoutubeSearchResult
                {
                    Title = video.Title,
                    Thumbnail = video.Thumbnails.StandardResUrl,
                    VideoURL = video.GetUrl(),
                    Id = video.Id
                });

                onSearchCompleted?.Invoke(_tracks);
                
                return;
            }

            IReadOnlyList<Video> videos = await _youtubeClient.SearchVideosAsync(query, 1);

            foreach (Video video in videos)
            {
                _tracks.Add(new YoutubeSearchResult
                {
                    Title = video.Title,
                    Thumbnail = video.Thumbnails.StandardResUrl,
                    VideoURL = video.GetUrl(),
                    Id = video.Id
                });
            }

            onSearchCompleted?.Invoke(_tracks);
        }
    }
}