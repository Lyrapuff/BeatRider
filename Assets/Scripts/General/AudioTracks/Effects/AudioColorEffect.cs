using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General.AudioTracks.Effects
{
    public class AudioColorEffect : MonoBehaviour
    {
        public Action<Color> OnColorChanged { get; set; }
        public Color CurrentColor { get; private set; }
        
        [SerializeField] private float _threshold;
        [SerializeField] private Color[] _colors;
        
        private SpectrumReader _spectrum;
        private Color _currentColor;
        private float _lastSpeed;
        private float _lastChange;
        
        private static readonly int AudioColor = Shader.PropertyToID("_AudioColor");

        private void Awake()
        {
            _spectrum = FindObjectOfType<SpectrumReader>();
        }

        private void Start()
        {
            InitiateColorChange();
        }

        private void Update()
        {
            float speed = _spectrum.PureSpeed;
            float dSpeed = Mathf.Abs(speed - _lastSpeed);
            _lastSpeed = speed;
            
            if (dSpeed > _threshold && Time.time - _lastChange > 1f)
            {
                InitiateColorChange();
            }
        }
        
        private void InitiateColorChange()
        {
            Color color = _currentColor;

            while (color == _currentColor)
            {
                color = _colors[Random.Range(0, _colors.Length)];
            }

            StartCoroutine(ChangeColor(color));
            CurrentColor = color;
                
            _lastChange = Time.time;
        }
        
        private IEnumerator ChangeColor(Color color)
        {
            float time = 0f;

            while (time <= 1f)
            {
                Color lColor = Color.Lerp(_currentColor, color, time);
                lColor.a = 255f;
                
                OnColorChanged?.Invoke(lColor);
                
                time += Time.fixedDeltaTime * 3f;
                yield return new WaitForFixedUpdate();
            }
            
            _currentColor = color;
        }
    }
}