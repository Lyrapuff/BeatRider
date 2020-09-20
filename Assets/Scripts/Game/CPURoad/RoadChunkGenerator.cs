using System.Linq;
using General.Behaviours;
using UnityEngine;

namespace Game.CPURoad
{
    [RequireComponent(typeof(RoadHeight))]
    public class RoadChunkGenerator : ExtendedBehaviour
    {
        [SerializeField] private Material[] _materials;

        private RoadHeight _roadHeight;
        private float[] _roadBuffer;
        private int _chunkIndex;
        private Vector4[] _points;
        private float _length;

        private void Start()
        {
            _roadHeight = GetComponent<RoadHeight>();

            _points = _roadHeight.GetPoints().Select(x => new Vector4(x.x, x.y, 0f, 0f)).ToArray();
            _length = _roadHeight.GetLength();
            
            foreach (Material material in _materials)
            {
                material.SetFloat("_Count", _points.Length);
                material.SetFloat("_Length", _length);
                material.SetVectorArray("_Points", _points);
            }
        }

        private void Update()
        {
            foreach (Material material in _materials)
            {
                material.SetFloat("_Offset", _roadHeight.Offset);
            }
        }
    }
}