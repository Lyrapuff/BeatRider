using System.Collections.Generic;
using General.AudioTracks.Searching;
using UnityEngine;

namespace Menu.PCUI.AudioTracks.Searching
{
    public class SearchResults : MonoBehaviour
    {
        [SerializeField] private SearchInputField _searchField;
        [SerializeField] private TrackElement _trackPrefab;

        private void OnEnable()
        {
            _searchField.OnSearchStarted += DestroyTracks;
            _searchField.OnSearchCompleted += HandleSearchCompleted;
        }

        private void OnDisable()
        {
            _searchField.OnSearchStarted -= DestroyTracks;
            _searchField.OnSearchCompleted -= HandleSearchCompleted;
        }

        private void HandleSearchCompleted(List<ISearchResult> searchResults)
        {
            foreach (ISearchResult searchResult in searchResults)
            {
                TrackElement instance = Instantiate(_trackPrefab, transform);
                instance.SetResult(searchResult);
            }
        }

        private void DestroyTracks()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}