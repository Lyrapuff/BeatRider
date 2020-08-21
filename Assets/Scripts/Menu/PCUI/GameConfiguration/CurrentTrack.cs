using General.AudioTracks;
using General.Behaviours;
using General.Storage;
using Menu.PCUI.AudioTracks;
using UnityEngine;

namespace Menu.PCUI.GameConfiguration
{
    [RequireComponent(typeof(TrackElement))]
    public class CurrentTrack : ExtendedBehaviour
    {
        private IStorage _storage;
        private TrackElement _trackElement;

        private void Awake()
        {
            _storage = FindComponentOfInterface<IStorage>();
            _trackElement = GetComponent<TrackElement>();

            AudioTrack track = _storage.Get<AudioTrack>("SelectedTrack");
            _trackElement.SetResult(track);
        }
    }
}