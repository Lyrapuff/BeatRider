using System;
using System.Collections.Generic;
using General.AudioTracks.Searching;
using TMPro;
using UnityEngine;

namespace Menu.PCUI.AudioTracks.Searching
{
    [RequireComponent(typeof(TMP_InputField))]
    public class SearchInputField : MonoBehaviour
    {
        public Action OnSearchStarted { get; set; }
        public Action<List<ISearchResult>> OnSearchCompleted { get; set; }
        
        [SerializeField] private TrackSearcher _searcher;

        private TMP_InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
        }

        private void OnEnable()
        {
            _inputField.onEndEdit.AddListener(HandleEndEdit);
        }

        private void OnDisable()
        {
            _inputField.onEndEdit.RemoveListener(HandleEndEdit);
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