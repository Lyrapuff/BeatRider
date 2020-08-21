using UnityEngine;

namespace Menu.PCUI.AudioTracks.Searching
{
    [RequireComponent(typeof(TrackCarousel))]
    public class SearchResults : MonoBehaviour
    {
        [SerializeField] private SearchInputField _searchField;

        private TrackCarousel _trackCarousel;

        private void Awake()
        {
            _trackCarousel = GetComponent<TrackCarousel>();
        }

        private void OnEnable()
        {
            _searchField.OnSearchStarted += _trackCarousel.ClearTracks;
            _searchField.OnSearchCompleted += _trackCarousel.LoadTracks;
        }

        private void OnDisable()
        {
            _searchField.OnSearchStarted -= _trackCarousel.ClearTracks;
            _searchField.OnSearchCompleted -= _trackCarousel.LoadTracks;
        }
    }
}