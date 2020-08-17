using System;
using System.Collections.Generic;
using General.AudioTracks;
using General.Behaviours;
using UI.AudioTracks.Searching;
using UnityEngine;

namespace UI.AudioTracks
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Loading : ExtendedBehaviour
    {
        [SerializeField] private SearchField _searchField;
        
        private CanvasGroup _canvasGroup;
        private bool _searching;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _searchField.OnSearchCompleted += SearchCompletedHandle;
            _searchField.OnSearchStarted += SearchStartedHandle;
        }

        private void OnDisable()
        {
            _searchField.OnSearchCompleted -= SearchCompletedHandle;
            _searchField.OnSearchStarted -= SearchStartedHandle;
        }

        private void Update()
        {
            if (_searching && _canvasGroup.alpha != 1f)
            {
                _canvasGroup.alpha = 1f;
            }

            if (!_searching && _canvasGroup.alpha != 0f)
            {
                _canvasGroup.alpha = 0f;
            }
        }

        private void SearchStartedHandle()
        {
            _searching = true;
        }

        private void SearchCompletedHandle(List<AudioTrack> tracks)
        {
            _searching = false;
        }
    }
}