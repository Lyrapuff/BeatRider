using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.Services;
using Game.Services.Implementations;
using General.AudioTracks;
using General.AudioTracks.Searching;
using General.Behaviours;
using General.Services.GameStatus;
using TMPro;
using UI.PCUI.AudioTracks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AudioTracks.Searching
{
    public class SearchResults : ExtendedBehaviour
    {
        [SerializeField] private TrackElement _trackPrefab;
        [SerializeField] private SearchField _searchField;

        private SceneService _scene;
        private GameSettingsService _gameSettings;
        private GameStatusService _gameStatus;
        
        private void OnEnable()
        {
            _scene = Toolbox.Instance.GetService<SceneService>();
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            _gameSettings = Toolbox.Instance.GetService<GameSettingsService>();
            
            _searchField.OnSearchCompleted += SearchCompletedHandle;
            _searchField.OnSearchStarted += DeleteTracks;
        }

        private void OnDisable()
        {
            _searchField.OnSearchCompleted -= SearchCompletedHandle;
            _searchField.OnSearchStarted -= DeleteTracks;
        }

        private void SearchCompletedHandle(List<YoutubeSearchResult> tracks)
        {
            foreach (YoutubeSearchResult searchResult in tracks)
            {
                TrackElement instance = Instantiate(_trackPrefab, transform);
                instance.SetResult(searchResult);
                
                instance.GetComponent<TrackButton>().OnProcessed = audioTrack =>
                {
                    _gameSettings.Clip = audioTrack.AudioClip;
                    _gameSettings.AnalyzedAudio = audioTrack.AnalyzedAudio;
                    
                    _gameStatus.Change(GameStatusChangeType.Started, _gameSettings.Configuration);
                    Configuration.Instance = _gameSettings.Configuration;
                    _scene.LoadScene(1);
                };

                //StartCoroutine(SetImage(instance.transform.GetChild(1).GetComponent<Image>(), audioTrack.PreviewURL));
            }
        }

        private void DeleteTracks()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}