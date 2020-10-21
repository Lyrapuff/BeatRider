using System.Collections.Generic;
using UnityEngine;

namespace General.AudioTracks.RoadGeneration
{
    public struct Road
    {
        public List<Vector2> Points { get; set; }
        public float Length { get; set; }
        public List<float> Beats { get; set; }
    }
}