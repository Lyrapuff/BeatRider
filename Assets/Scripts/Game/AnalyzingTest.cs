using System.Collections.Generic;
using System.Linq;
using General.Audio;
using UnityEngine;

namespace Game
{
    public class AnalyzingTest : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _scale;
        [SerializeField] private Material _material;
        [Header("Output")]
        [SerializeField] private Texture2D _heightMap;

        private AudioSource _audioSource;
        private List<float> _averages = new List<float>();
        private bool _done;
        
        private void Awake()
        {
            _audioSource = FindObjectOfType<AudioSource>();
            
            TestAudioAnalyzer testAudioAnalyzer = new TestAudioAnalyzer();
            /*
            testAudioAnalyzer.Analyze(_audioClip, result =>
            {
                _averages = result.Averages;
                Debug.Log($"size: " + result.Averages.Count);
                Debug.Log($"min: " + result.Averages.Min());
                Debug.Log($"max: " + result.Averages.Max());
            });
            */
        }

        private void Update()
        {
            if (!_done && _averages.Count != 0)
            {
                Texture2D heightMap = new Texture2D(_averages.Count, 1);
                
                float[] heights = new float[_averages.Count];

                for (int i = 0; i < _averages.Count; i ++)
                {
                    float samplePoint = i / _scale;
                    heights[i] = Mathf.PerlinNoise(samplePoint, samplePoint);
                }

                float min = heights.Min();
                float max = heights.Max();

                Color[] colors = new Color[_averages.Count];
                
                for (int i = 0; i < _averages.Count; i++)
                {
                    heights[i] = Mathf.InverseLerp(min, max, heights[i]);
                    colors[i] = new Color(heights[i], heights[i], heights[i]);
                }

                heightMap.SetPixels(colors);
                heightMap.Apply();

                _heightMap = heightMap;
                
                Texture2D sideMap = new Texture2D(_averages.Count, 1);

                for (int i = 0; i < _averages.Count; i++)
                {
                    float sampleCoord = i / 200f;
                    float color = Mathf.PerlinNoise(sampleCoord, sampleCoord);
                    colors[i] = new Color(color, color, color);
                }
                
                sideMap.SetPixels(colors);
                sideMap.Apply();
                
                _material.SetTexture("_HeightMap", _heightMap);
                _material.SetTexture("_SideMap", sideMap);
                _material.SetFloat("_Size", _averages.Count);
                
                _done = true;
                _audioSource.Play();
            }
        }
    }
}