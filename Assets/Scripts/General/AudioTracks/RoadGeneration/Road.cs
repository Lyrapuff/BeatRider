using System.Collections.Generic;
using UnityEngine;

namespace General.AudioTracks.RoadGeneration
{
    public class Road
    {
        public List<Vector2> Points { get; set; } = new List<Vector2>();
        public float Length { get; set; }
    }
}