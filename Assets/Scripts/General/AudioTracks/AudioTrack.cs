﻿using General.Audio;
using General.AudioTracks.Processing;
using General.AudioTracks.Searching;
using UnityEngine;

namespace General.AudioTracks
{
    public class AudioTrack : ISearchResult
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string ThumbnailPath { get; set; }
        public AudioClip AudioClip { get; set; }
        public AnalyzedAudio AnalyzedAudio { get; set; }
        public Sprite Thumbnail { get; set; }

        public void Process(ProcessingPipeline pipeline)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return;
            }
            
            pipeline.Process(this);
        }
    }
}