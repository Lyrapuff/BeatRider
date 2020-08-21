using System.Collections.Generic;
using System.Linq;
using General.Behaviours;

namespace General.AudioTracks
{
    public class Playlist : ExtendedBehaviour, IPlaylist
    {
        private List<IAudioTrack> _tracks = new List<IAudioTrack>();

        public void Add(IAudioTrack track)
        {
            if (_tracks.All(t => t.Id != track.Id))
            {
                _tracks.Add(track);
            }
        }

        public IAudioTrack Get(IAudioTrack track)
        {
            return Get(track.Id);
        }
        
        public IAudioTrack Get(string id)
        {
            return _tracks.FirstOrDefault(t => t.Id == id);
        }
    }
}