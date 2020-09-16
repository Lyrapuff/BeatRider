using System;
using General.Behaviours;
using UnityEngine;

namespace Game.CPURoad
{
    [RequireComponent(typeof(RoadHeight))]
    public class RoadChunkGenerator : ExtendedBehaviour
    {
        [SerializeField] private Material[] _materials;

        private RoadHeight _roadHeight;
        private Color[] _roadBuffer;
        private int _chunkIndex;
        
        private void Start()
        {
            _roadHeight = GetComponent<RoadHeight>();
            
            GenerateForward();
        }

        private void Update()
        {
            if (_roadHeight.Offset > 512f * _chunkIndex)
            {
                GenerateForward();
            }

            foreach (Material material in _materials)
            {
                material.SetFloat("_Offset", _roadHeight.Offset - 512f * (_chunkIndex - 1));
            }
        }

        private void GenerateForward()
        {
            if (_roadBuffer != null)
            {
                Array.Copy(_roadBuffer, 512, _roadBuffer, 0, 512);
                
                for (int i = 512; i < 1024; i++)
                {
                    float height = _roadHeight.GetHeight(i);
                    _roadBuffer[i] = new Color(height, height ,height);
                }
            }
            else
            {
                _roadBuffer = new Color[1024];
                
                for (int i = 0; i < 1024; i++)
                {
                    float height = _roadHeight.GetHeight(i);
                    _roadBuffer[i] = new Color(height, height ,height);
                }
            }

            Texture2D chunkTexture = new Texture2D(1024, 1, TextureFormat.RGBA32, false, true);
            chunkTexture.filterMode = FilterMode.Point;
            chunkTexture.SetPixels(_roadBuffer);
            chunkTexture.Apply();
            
            foreach (Material material in _materials)
            {
                material.SetTexture("_RoadChunk", chunkTexture);
            }
            
            _chunkIndex++;
        }
    }
}