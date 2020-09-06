using System.Collections.Generic;
using System.IO;
using General.AudioTracks;
using UnityEngine;

namespace Menu.PCUI.AudioTracks
{
    [RequireComponent(typeof(TrackCarousel))]
    public class TrackHistory : MonoBehaviour
    {
        private TrackCarousel _trackCarousel;

        private void Awake()
        {
            _trackCarousel = GetComponent<TrackCarousel>();
            
            LoadHistory();
        }

        private void LoadHistory()
        {
            string path = Application.persistentDataPath + "/playlist";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<IAudioTrack> tracks = new List<IAudioTrack>();
            
            DirectoryInfo info = new DirectoryInfo(path);
            
            foreach (DirectoryInfo trackDirectory in info.GetDirectories())
            {
                IAudioTrack track = new AudioTrack();

                track.Title = "No title today, i guess?";
                track.Thumbnail = trackDirectory.FullName + @"\thumbnail.jpeg";
                track.Id = trackDirectory.Name;

                string titlePath = trackDirectory.FullName + @"\title";

                if (File.Exists(titlePath))
                {
                    track.Title = File.ReadAllText(titlePath);
                }
                
                tracks.Add(track);
            }
            
            _trackCarousel.LoadTracks(tracks);
        }
    }
}