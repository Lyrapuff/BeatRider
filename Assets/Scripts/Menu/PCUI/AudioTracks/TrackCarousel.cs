using System;
using System.Collections.Generic;
using General.AudioTracks;
using UnityEngine;

namespace Menu.PCUI.AudioTracks
{
    public class TrackCarousel : MonoBehaviour
    {
        public Action<TrackElement> OnSelected { get; set; }
        
        [SerializeField] private TrackElement _trackPrefab;

        private List<TrackElement> _tracks = new List<TrackElement>();
        private TrackElement _selected;
        
        public void LoadTracks(IEnumerable<IAudioTrack> tracks)
        {
            foreach (IAudioTrack track in tracks)
            {
                TrackElement instance = Instantiate(_trackPrefab, transform);
                instance.SetResult(track);
                
                instance.OnSelected += HandleSelected;

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

        private void HandleSelected(TrackElement trackElement)
        {
            _selected = trackElement;
            OnSelected?.Invoke(_selected);
        }
    }
}