using System.Collections.Generic;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.Road
{
    public class RoadMovement : ExtendedBehaviour
    {
        [SerializeField] private RoadGenerator _roadGenerator;
        [SerializeField] private Material _roadMaterial;
        [SerializeField] private Material _roadGrassMaterial;

        private GameStatusService _gameStatus;
        private PauseService _pause;
        private IAudioAnalyzer _audioAnalyzer;
        private List<float> _heights = new List<float>();
        private float _seed;
        private float _offset;
        private float _newSize;
        private int _times;

        private void Awake()
        {
            _seed = Random.Range(-100000f, 100000f);
            _pause = Toolbox.Instance.GetService<PauseService>();
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            
            Generate();
        }

        private void Update()
        {
            if (_pause.Paused || _gameStatus.Status == GameStatusChangeType.Crushed)
            {
                return;
            }
            
            _offset += Time.deltaTime * _audioAnalyzer.Speed * 30f;
            _roadMaterial.SetFloat("_Offset", _offset + _seed);
            _roadGrassMaterial.SetFloat("_Offset", _offset + _seed);
        }

        private void Generate()
        {
            _newSize = 500f * _times;
            _heights.AddRange(_roadGenerator.Generate(_newSize, _seed));
        }

        public float GetHeight(float z)
        {
            if (_heights == null)
            {
                return 0f;
            }

            int index = Mathf.RoundToInt((z + _offset) * 10f);

            if (index < 0f)
            {
                return 0f;
            }

            if (index >= _heights.Count)
            {
                _times++;
                Generate();
                return GetHeight(z);
            }

            float height = _heights[index];

            return height;
        }
    }
}