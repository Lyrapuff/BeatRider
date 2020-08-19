using General.AudioTracks;
using General.AudioTracks.Playing;
using General.AudioTracks.Searching;
using UnityEngine;

namespace Menu.PCUI.AudioTracks
{
    [RequireComponent(typeof(TrackElement))]
    public class TrackButton : MonoBehaviour
    {
        private TrackElement _trackElement;
        private AudioPlayer _audioPlayer;

        private void Awake()
        {
            _trackElement = GetComponent<TrackElement>();
            _audioPlayer = FindObjectOfType<AudioPlayer>();
        }

        public void Interact()
        {
            ISearchResult searchResult = _trackElement.SearchResult;
            
            AudioTrack track = new AudioTrack
            {
                Id = searchResult.Id,
                Title = searchResult.Title,
                ThumbnailPath = searchResult.ThumbnailPath
            };
            
            _audioPlayer.PrepareAndPlay(track);
        }
    }
}