using General.AudioTracks;
using UnityEngine;

namespace Menu.PCUI.AudioTracks
{
    [RequireComponent(typeof(TrackElement))]
    public class TrackButton : MonoBehaviour
    {
        private TrackElement _trackElement;

        private void Awake()
        {
            _trackElement = GetComponent<TrackElement>();
        }

        public void Interact()
        {
            IAudioTrack searchResult = _trackElement.Track;
        }
    }
}