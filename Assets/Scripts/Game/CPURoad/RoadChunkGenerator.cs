using System;
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
        
        private void Start()
        {
            _roadHeight = GetComponent<RoadHeight>();

            Vector4[] points = _roadHeight.GetPoints().Select(x => new Vector4(x.x, x.y, 0f, 0f)).ToArray();
            float length = _roadHeight.GetLength();
            
            foreach (Material material in _materials)
            {
                material.SetVectorArray("_Points", points);
                material.SetFloat("_Count", points.Length);
                material.SetFloat("_Length", length);
            }
            
            //GenerateForward();
        }

        private void Update()
        {
            foreach (Material material in _materials)
            {
                material.SetFloat("_Offset", _roadHeight.Offset);
            }
            
            return;
            
            if (_roadHeight.Offset > 500f * _chunkIndex)
            {
                GenerateForward();
            }

            foreach (Material material in _materials)
            {
                material.SetFloat("_Offset", _roadHeight.Offset - 500f * (_chunkIndex - 1));
            }
        }

        private void GenerateForward()
        {
            if (_roadBuffer != null)
            {
                Array.Copy(_roadBuffer, 500, _roadBuffer, 0, 500);
                
                for (int i = 500; i < 1000; i++)
                {
                    float height = _roadHeight.GetHeight(i);
                    _roadBuffer[i] = height;
                }
            }
            else
            {
                _roadBuffer = new float[1000];
                
                for (int i = 0; i < 1000; i++)
                {
                    float height = _roadHeight.GetHeight(i);
                    _roadBuffer[i] = height;
                }
            }
            
            foreach (Material material in _materials)
            {
                material.SetFloatArray("_RoadChunk", _roadBuffer);
            }
            
            _chunkIndex++;
        }
    }
}