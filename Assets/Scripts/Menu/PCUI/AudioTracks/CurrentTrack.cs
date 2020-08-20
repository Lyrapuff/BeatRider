using General.AudioTracks.Playing;
using UnityEngine;

namespace Menu.PCUI.AudioTracks
{
    [RequireComponent(typeof(TrackElement))]
    public class CurrentTrack : MonoBehaviour
    {
        private AudioPlayer _audioPlayer;
        private TrackElement _trackElement;

        private void Awake()
        {
            _audioPlayer = FindObjectOfType<AudioPlayer>();
            _trackElement = GetComponent<TrackElement>();
            
            _trackElement.SetResult(_audioPlayer.Track);
        }
    }
}