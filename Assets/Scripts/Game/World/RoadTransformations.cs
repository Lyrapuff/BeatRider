using System.Linq;
using UnityEngine;
using UnityEngine.Video;

namespace Game.World
{
    public class RoadTransformations : MonoBehaviour
    {
        [SerializeField] private int _size;
        [SerializeField] private float _scale;
        [SerializeField] private Material _material;

        private VideoPlayer _videoPlayer;
        private float _time;
        private int _offset;
        private float[] _heightMap;
        private float[] _sideMap;

        private Texture2D _heightTexture;
        private Texture2D _sideTexture;

        private void Awake()
        {
            _videoPlayer = FindObjectOfType<VideoPlayer>();
            _heightMap = new float[_size];

            Color[] colors = new Color[_size];
            
            for (int i = 0; i < _size; i ++)
            {
                float samplePoint = i / _scale;
                _heightMap[i] = Mathf.PerlinNoise(samplePoint, samplePoint);
                colors[i] = new Color(_heightMap[i], _heightMap[i], _heightMap[i]);
            }

            Texture2D heightMap = new Texture2D(_size, 1);
            
            heightMap.SetPixels(colors);
            heightMap.Apply();
            
            _sideMap = new float[_size];
            
            for (int i = 0; i < _size; i++)
            {
                float sampleCoord = i / 200f;
                _sideMap[i] = Mathf.PerlinNoise(sampleCoord, sampleCoord);
                colors[i] = new Color(_sideMap[i], _sideMap[i], _sideMap[i]);
            }
            
            Texture2D sideMap = new Texture2D(_size, 1);
            
            sideMap.SetPixels(colors);
            sideMap.Apply();

            _heightTexture = heightMap;
            _sideTexture = sideMap;
            
            _material.SetTexture("_HeightMap", heightMap);
            _material.SetTexture("_SideMap", sideMap);
            _material.SetFloat("_Size", _size);
        }

        public Vector3 GetPosition(float x)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);

            position.y = Mathf.Lerp(-20f, 20f, _heightTexture.GetPixel(Mathf.RoundToInt(x + _offset), 0).r);
            position.x = _sideTexture.GetPixel(Mathf.RoundToInt(x + _offset), 0).r * 70f;

            return position;
        }

        private void Update()
        {
            _time += Time.deltaTime * 50f;
            
            if (_time > 1f)
            {
                _time = 0f;
                _offset++;
                _material.SetInt("_Offset", _offset);
            }
        }
    }
}