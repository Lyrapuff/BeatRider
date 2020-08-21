using System.Collections.Generic;
using General.AudioTracks;
using UnityEngine;

namespace Menu.PCUI.AudioTracks
{
    public class TrackCarousel : MonoBehaviour
    {
        [SerializeField] private TrackElement _trackPrefab;
        
        private List<TrackElement> _tracks = new List<TrackElement>();

        public void LoadTracks(IEnumerable<IAudioTrack> tracks)
        {
            foreach (IAudioTrack track in tracks)
            {
                TrackElement instance = Instantiate(_trackPrefab, transform);
                instance.SetResult(track);
                
                _tracks.Add(instance);
            }
        }

        public void ClearTracks()
        {
            foreach (TrackElement trackElement in _tracks)
            {
                Destroy(trackElement.gameObject);
            }
            
            _tracks.Clear();
        }
    }
}