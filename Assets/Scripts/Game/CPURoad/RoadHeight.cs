using System.Collections.Generic;
using System.Linq;
using General.AudioTracks;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace Game.CPURoad
{
    public class RoadHeight : ExtendedBehaviour
    {
        public float Offset { get; private set; }
        public float Max { get; private set; }
        public float Min { get; private set; }
        
        private IAudioAnalyzer _audioAnalyzer;
        private float _seed;
        private AudioTrack _track;
        private float[] _road;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
            
            IStorage storage = FindComponentOfInterface<IStorage>();
            _seed = storage.Get<float>("Game/Seed");
            _track = storage.Get<AudioTrack>("Game/Track");
            
            Generate();
        }

        private void Update()
        {
            Offset += Time.deltaTime * _audioAnalyzer.Speed * 20f;
        }

        private void Generate()
        {
            List<float> averages = _track.AnalyzedAudio.Averages;

            int blankCount = 0;
            
            for (int i = 0; i < averages.Count; i++)
            {
                float average = averages[i];

                if (average == 0f)
                {
                    blankCount++;
                }
                else
                {
                    blankCount = 0;
                }

                if (blankCount >= 200)
                {
                    averages.RemoveRange(i - 200, averages.Count - i - 200);
                    break;
                }
            }

            float timeStep = 1 / 60f;
            float time = 0f;
            float distance = 0f;
            float lengthPerSample = _track.AudioClip.length / _track.AudioClip.samples;

            while (time < _track.AudioClip.length)
            {
                try
                {
                    int index = Mathf.FloorToInt(time / lengthPerSample) / 1024 / _track.AnalyzedAudio.StoreEvery;

                    distance += (averages[index] * 6f + 1f) * timeStep;

                    time += timeStep;
                }
                catch
                {
                    //ignore
                }
            }

            int length = Mathf.FloorToInt(distance * 20f);

            float threshold = 0.55f;
            float step = 0.4f;
            
            _road = new float[length];
            float height = 0f;

            for (int i = 0; i < length; i++)
            {
                try
                {
                    int index = Mathf.FloorToInt(((float) i).Remap(0, length, 0, averages.Count));

                    float average = averages[index];

                    if (average > threshold)
                    {
                        height -= step * Mathf.Lerp(0.5f, 1f, average.Remap(threshold, 1f, 0f, 1f));
                    }
                    else
                    {
                        height += step * Mathf.Lerp(0.5f, 1f, average.Remap(0f, threshold, 0f, 1f));
                    }

                    _road[i] = height;
                }
                catch
                {
                    //ignore
                }
            }
            
            int severity = 20;
            
            for (int i = 1; i < _road.Length; i++)
            {
                int start = i - severity > 0 ? i - severity : 0;
                int end = i + severity < _road.Length ? i + severity : _road.Length;

                float sum = 0;

                for (int j = start; j < end; j++)
                {
                    sum += _road[j];
                }

                float avg = sum / (end - start);
                
                _road[i] = avg;
            }

            Max = _road.Max();
            Min = _road.Min();
            
            Debug.Log($"max: {Max}");
            Debug.Log($"min: {Min}");
            
            for (int i = 0; i < _road.Length; i++)
            {
                _road[i] = Mathf.InverseLerp(Min, Max, _road[i]);
            }
            
            float[] _smoothRoad = new float[length * 10];
            
            _smoothRoad[0] = _road[0];
            _smoothRoad[_smoothRoad.Length - 1] = _road[_road.Length - 1];

            for (int i = 1; i < _smoothRoad.Length - 1; i++)
            {
                float jd = ((float)i * (float)(_road.Length - 1) / (float)(_smoothRoad.Length - 1));
                int j = (int)jd;
                _smoothRoad[i] = _road[j] + (_road[j + 1] - _road[j]) * (jd - (float)j);
            }
            
            _road = _smoothRoad;
        }
        
        public float GetHeight(float point)
        {
            int index = Mathf.CeilToInt((point + Offset) * 10f);

            try
            {
                return _road[index];
            }
            catch
            {
                return _road[_road.Length - 1];
            }

            //return Mathf.PerlinNoise((point + Offset + _seed) / 214.253f, 0.245f);
        }
        
        public float GetHeight(Vector3 position)
        {
            return GetHeight(position.z) * 1600f;
        }
    }
}