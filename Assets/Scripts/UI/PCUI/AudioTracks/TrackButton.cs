﻿using System;
using General.AudioTracks;
using General.AudioTracks.Processing;
using UnityEngine;

namespace UI.PCUI.AudioTracks
{
    [RequireComponent(typeof(TrackElement))]
    public class TrackButton : MonoBehaviour
    {
        public Action<AudioTrack> OnProcessed { get; set; }

        [SerializeField] private ProcessingPipeline _pipeline;

        private TrackElement _trackElement;

        private void Awake()
        {
            _trackElement = GetComponent<TrackElement>();
        }

        public void Interact()
        {
            _pipeline.Process(_trackElement.SearchResult);
        }

        private void Update()
        {
            if (_pipeline.Status == ProcessingStatus.Success)
            {
                ProcessingContext context = _pipeline.Context;

                AudioClip clip = AudioUtil.AssembleClip(context);

                AudioTrack track = new AudioTrack
                {
                    Title = _trackElement.SearchResult.Title,
                    Id = _trackElement.SearchResult.Id,
                    AudioClip = clip,
                    AnalyzedAudio = context.AnalyzedAudio
                };
                
                OnProcessed?.Invoke(track);
            }
        }
    }
}