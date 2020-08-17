using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.AudioTracks;
using General.AudioTracks.Searching;
using General.Behaviours;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AudioTracks.Searching
{
    [RequireComponent(typeof(ITrackSearcher<ISearchResult>))]
    public class SearchField : ExtendedBehaviour
    {
        public Action<List<YoutubeSearchResult>> OnSearchCompleted { get; set; }
        public Action OnSearchStarted { get; set; }
        
        private TMP_InputField _inputField;
        private ITrackSearcher<YoutubeSearchResult> _searcher;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _searcher = GetComponent<ITrackSearcher<YoutubeSearchResult>>();
        }

        private void OnEnable()
        {
            _inputField.onEndEdit.AddListener(HandleEndEdit);
        }

        private void OnDisable()
        {
            _inputField.onEndEdit.RemoveAllListeners();
        }

        private async void HandleEndEdit(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                OnSearchStarted?.Invoke();
            
                await _searcher.Search(text, OnSearchCompleted);
            }
        }
    }
}