using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General.AudioTracks;
using General.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AudioTracks.Searching
{
    public class SearchResults : ExtendedBehaviour
    {
        [SerializeField] private GameObject _trackPrefab;
        [SerializeField] private SearchField _searchField;

        private void OnEnable()
        {
            _searchField.OnSearchCompleted += SearchCompletedHandle;
            _searchField.OnSearchStarted += DeleteTracks;
        }

        private void OnDisable()
        {
            _searchField.OnSearchCompleted -= SearchCompletedHandle;
            _searchField.OnSearchStarted -= DeleteTracks;
        }

        private void SearchCompletedHandle(List<AudioTrack> tracks)
        {
            foreach (AudioTrack audioTrack in tracks)
            {
                GameObject instance = Instantiate(_trackPrefab, transform);
                instance.GetComponentInChildren<Text>().text = audioTrack.Title;
                instance.GetComponent<SearchTrackTest>().Track = audioTrack;

                StartCoroutine(SetImage(instance.transform.GetChild(1).GetComponent<Image>(), audioTrack.PreviewURL));
            }
        }

        private void DeleteTracks()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        private IEnumerator SetImage(Image image, string url)
        {
            WWW www = new WWW(url);
            
            yield return www;
            
            image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        }
    }
}