using System.Collections;
using Game.Services;
using Game.Services.Implementations;
using Game.World.Audio;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;
using UnityEngine.VFX;

namespace Game.World.Environments.Forest
{
    public class AudioColor : ExtendedBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _threshold;
        [SerializeField] private Color[] _colors;
        [Header("Materials")] 
        [SerializeField] private Material _skybox;
        [SerializeField] private Material _leaves;
        [SerializeField] private Material _road;
        [SerializeField] private Material _spectrum;
        [SerializeField] private VisualEffect _leavesVFX;
        
        private IAudioAnalyzer _audioAnalyzer;

        private Color _currentColor;
        private float _lastSpeed;
        private float _lastChange;
        
        private static readonly int SkyColor = Shader.PropertyToID("SkyColor");
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private static readonly int ColorBc741495 = Shader.PropertyToID("_BaseColor");
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            
            InitiateColorChange();
        }

        private void Update()
        {
            float speed = _audioAnalyzer.PureSpeed;
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

            _currentColor = color;
                
            StartCoroutine(ChangeColor(color));
                
            _lastChange = Time.time;
        }
        
        private IEnumerator ChangeColor(Color color)
        {
            float time = 0f;
            Color currentColor = _skybox.GetColor("SkyColor");

            while (true)
            {
                Color lColor = Color.Lerp(currentColor, color, time);
                lColor.a = 255f;
                
                _skybox.SetColor(SkyColor, lColor);
                _leaves.SetColor(BaseColor, lColor);
                _road.SetColor(ColorBc741495, lColor);
                _spectrum.SetColor(EmissionColor, lColor * 0.7f);
                _leavesVFX.SetVector4("Color", lColor * 0.3f);
                
                time += Time.fixedDeltaTime * 3f;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}