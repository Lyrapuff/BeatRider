﻿using System;
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

        private IAudioAnalyzer _audioAnalyzer;
        private AudioTrack _track;
        private List<Vector2> _points = new List<Vector2>();
        private int _length;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
            
            IStorage storage = FindComponentOfInterface<IStorage>();
            _track = storage.Get<AudioTrack>("Game/Track");
            
            Generate();
        }

        private void Update()
        {
            Offset += Time.deltaTime * _audioAnalyzer.Speed;
        }

        private void Generate()
        {
            List<float> averages = _track.AnalyzedAudio.Averages;

            float timeStep = 1 / 10f;
            float time = 0f;
            float distance = 0f;
            float lengthPerSample = _track.AudioClip.length / _track.AudioClip.samples;
            
            int storeEvery = 15;
            float threshold = 0.55f;
            float step = 0.2f;
            float height = 0f;
            float direction = 0f;
            

            while (time < _track.AudioClip.length)
            {
                int index = Mathf.FloorToInt(time / lengthPerSample) / 1024 / _track.AnalyzedAudio.StoreEvery;

                if (index >= averages.Count)
                {
                    break;
                }

                float average = averages[index];
                
                if (average >= threshold)
                {
                    direction
                        = -1f *
                          Mathf.Lerp(0.5f, 1f, average.Remap(threshold, 1f, 0f, 1f));
                }
                else
                {
                    direction
                        = 1f *
                          Mathf.Lerp(0f, 1f, average.Remap(0f, threshold, 0f, 1f));
                }

                height += step * direction;
                
                if (distance == 0f || (int)(time / timeStep) % storeEvery == 0)
                {
                    _points.Add(new Vector2(distance, height));
                }
                
                distance += (average * 160f + 1f) * timeStep;

                time += timeStep;
            }
            
            _points.Add(new Vector2(distance, height));
            
            for (int i = 0; i < _points.Count; i++)
            {
                EnforceMode(i);
            }
            
            Debug.Log($"points: {_points.Count}");
        }
        
        private Vector2 GetPoint (Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            t = Mathf.Clamp01(t);
            
            float oneMinusT = 1f - t;
            
            return
                oneMinusT * oneMinusT * oneMinusT * p0 +
                3f * oneMinusT * oneMinusT * t * p1 +
                3f * oneMinusT * t * t * p2 +
                t * t * t * p3;
        }

        private void EnforceMode(int index)
        {
            int middleIndex = (index - 1) / 3;

            if (middleIndex == 0 || middleIndex == _points.Count - 1)
            {
                return;
            }
            
            middleIndex *= 3;
            
            int fixedIndex;
            int enforcedIndex;

            if (index <= middleIndex)
            {
                fixedIndex = middleIndex - 1;
                enforcedIndex = middleIndex + 1;
            }
            else
            {
                fixedIndex = middleIndex + 1;
                enforcedIndex = middleIndex - 1;
            }

            Vector2 middle = _points[middleIndex];
            Vector2 enforcedTangent = middle - _points[fixedIndex];
            _points[enforcedIndex] = middle + enforcedTangent;
        }
        
        public float GetHeight(float z)
        {
            float position = z + Offset;

            int i;
            float t = position / _length;

            if (t >= 1f)
            {
                t = 1f;
                i = _points.Count - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * (_points.Count - 1) / 3;
                i = (int) t;
                t -= i;
                i *= 3;
            }

            Vector2 value = GetPoint(_points[i], _points[i + 1], _points[i + 2], _points[i + 3], t);
            
            return value.y;
        }

        public IEnumerable<Vector2> GetPoints()
        {
            return _points.ToArray();
        }

        public float GetLength()
        {
            return _length;
        }
        
        public float GetHeight(Vector3 position)
        {
            return GetHeight(position.z);
        }
    }
}