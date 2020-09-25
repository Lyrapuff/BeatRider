using System.Linq;
using General.AudioTracks.RoadGeneration;
using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace Game.CPURoad
{
    [RequireComponent(typeof(RoadHeight))]
    public class RoadChunkGenerator : ExtendedBehaviour
    {
        [SerializeField] private Material[] _materials;

        private Road _road;
        
        private static readonly int Count = Shader.PropertyToID("_Count");
        private static readonly int Length = Shader.PropertyToID("_Length");
        private static readonly int Points = Shader.PropertyToID("_Points");

        private void Awake()
        {
            IStorage storage = FindComponentOfInterface<IStorage>();
            _road = storage.Get<Road>("Game/Road");
            
            foreach (Material material in _materials)
            {
                material.SetFloat(Count, _road.Points.Count);
                material.SetFloat(Length, _road.Length);
                material.SetVectorArray(Points, _road.Points.Select(x => new Vector4(x.x, x.y, 0f, 0f)).ToArray());
            }
        }
    }
}