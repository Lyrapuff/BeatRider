using System;
using System.Collections.Generic;
using System.Linq;
using Game.Services;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.ObjectManagement
{
    public class LineSpawner : ExtendedBehaviour
    {
        [SerializeField] private float _range;
        [SerializeField] private float _speed;
        [SerializeField] private SpawnSetting[] _spawnSettings;
        
        private List<SpawnedObject> _pool = new List<SpawnedObject>();

        private IAudioAnalyzer _audioAnalyzer;
        private IPause _pause;

        private float _timeToSpawn;

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            _pause = FindComponentOfInterface<IPause>();

            GenerateChunks();
        }

        private void Update()
        {
            if (_pause.Paused)
            {
                return;
            }
            
            _timeToSpawn -= Time.deltaTime * _audioAnalyzer.Speed;

            if (_timeToSpawn <= 0f)
            {
                SpawnSetting spawnSetting = GetRandomSpawnSetting();
                _timeToSpawn = Spawn(spawnSetting) / _speed;
            }
        }

        private SpawnSetting GetRandomSpawnSetting()
        {
            float rnd = Random.Range(0, 101) / 100f;
            float lastChunk = float.MaxValue;

            SpawnSetting spawnSetting = new SpawnSetting();

            for (int i = 0; i < _spawnSettings.Length; i++)
            {
                float chunk = _spawnSettings[i].Chunk;

                if (chunk >= rnd && chunk < lastChunk)
                {
                    spawnSetting = _spawnSettings[i];
                    lastChunk = spawnSetting.Chunk;
                }
            }
            
            return spawnSetting;
        }
        
        private float Spawn(SpawnSetting spawnSetting)
        {
            GameObject go = _pool
                .FirstOrDefault(x => !x.Instance.activeSelf && x.SpawnSetting.Prefab == spawnSetting.Prefab)
                .Instance;

            Transform instance;
            
            if (go == null)
            {
                instance = Instantiate(spawnSetting.Prefab, transform).transform;

                _pool.Add(new SpawnedObject
                {
                    SpawnSetting = spawnSetting,
                    Instance = instance.gameObject
                });
            }
            else
            {
                go.SetActive(true);
                instance = go.transform;
            }

            instance.localPosition = new Vector3(Random.Range(-_range, _range), 0f, 0f);
            instance.localEulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);
            instance.localScale = Vector3.one * Random.Range(0.85f, 1.15f);
            
            return spawnSetting.Size;
        }

        private void GenerateChunks()
        {
            int periodicityTotal = _spawnSettings.Sum(x => x.Periocidity);

            for (int i = 0; i < _spawnSettings.Length; i++)
            {
                float offset = 0f;

                if (i > 0)
                {
                    offset = _spawnSettings[i - 1].Chunk;
                }
                
                _spawnSettings[i].Chunk = offset + (float)_spawnSettings[i].Periocidity / periodicityTotal;
            }
        }
        
        [Serializable]
        private struct SpawnSetting
        {
            [HideInInspector] public float Chunk;
            
            public GameObject Prefab => _prefab;
            public float Size => _size;
            public int Periocidity => _periodicity;
            
            [SerializeField] private GameObject _prefab;
            [SerializeField] private float _size;
            [SerializeField] private int _periodicity;
        }
        
        private struct SpawnedObject
        {
            public SpawnSetting SpawnSetting;
            public GameObject Instance;
        }
    }
}