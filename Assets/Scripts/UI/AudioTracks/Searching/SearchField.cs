using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.AudioTracks;
using General.AudioTracks.Searching;
using General.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AudioTracks.Searching
{
    [RequireComponent(typeof(InputField))]
    [RequireComponent(typeof(ITrackSearcher))]
    public class SearchField : ExtendedBehaviour
    {
        public Action<List<AudioTrack>> OnSearchCompleted { get; set; }
        public Action OnSearchStarted { get; set; }
        
        private InputField _inputField;
        private ITrackSearcher _searcher;

        private void Awake()
        {
            _inputField = GetComponent<InputField>();
            _searcher = GetComponent<ITrackSearcher>();
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