using System.Collections.Generic;
using System.Linq;
using Game.CPURoad;
using General.AudioTracks.Analyzing;
using General.AudioTracks.RoadGeneration;
using General.Behaviours;
using General.Storage;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Entities.Movement
{
    public class MovementManager : ExtendedBehaviour
    {
        private IAudioAnalyzer _audioAnalyzer;

        private Dictionary<Transform, MovingEntityData> _data = new Dictionary<Transform, MovingEntityData>();
        
        private JobHandle _forwardMovementHandle;
        private JobHandle _roadConnectorHandle;
        
        private NativeArray<Vector2> _points;
        private float _offset;
        private int _indexOffset;

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();

            IStorage storage = FindComponentOfInterface<IStorage>();
            Road road = storage.Get<Road>("Game/Road");
            
            _points = new NativeArray<Vector2>(road.Points.Count, Allocator.TempJob);
            
            for (int i = 0; i < road.Points.Count; i++)
            {
                _points[i] = road.Points[i];
            }
        }

        private void OnDisable()
        {
            _forwardMovementHandle.Complete();
            _roadConnectorHandle.Complete();
            _points.Dispose();
        }

        private void Update()
        {
            foreach (KeyValuePair<Transform, MovingEntityData> entity in _data.ToArray())
            {
                MovingEntityData data = _data[entity.Key];

                data.Position = entity.Key.position;
                data.Rotation = entity.Key.rotation;
                
                _data[entity.Key] = data;
            }

            _offset += Time.deltaTime * _audioAnalyzer.Speed;
            CalculateIndexOffset();

            NativeArray<MovingEntityData> settings = new NativeArray<MovingEntityData>(_data.Count, Allocator.TempJob);
            settings.CopyFrom(_data.Select(x => x.Value).ToArray());
            
            ForwardMovementJob forwardMovementJob = new ForwardMovementJob();
            forwardMovementJob.Data = settings;
            forwardMovementJob.AudioSpeed = _audioAnalyzer.Speed;
            forwardMovementJob.DeltaTime = Time.deltaTime;
            _forwardMovementHandle = forwardMovementJob.Schedule(_data.Count, 1);
            
            RoadConnectorJob roadConnectorJob = new RoadConnectorJob();
            roadConnectorJob.Data = settings;
            roadConnectorJob.Offset = _offset;
            roadConnectorJob.Points = _points;
            roadConnectorJob.IndexOffset = _indexOffset;
            _roadConnectorHandle = roadConnectorJob.Schedule(_data.Count, 1, _forwardMovementHandle);

            _forwardMovementHandle.Complete();
            _roadConnectorHandle.Complete();

            int i = 0;
            
            foreach (KeyValuePair<Transform, MovingEntityData> entity in _data.ToArray())
            {
                entity.Key.position = settings[i].Position;
                entity.Key.rotation = settings[i].Rotation;

                _data[entity.Key] = settings[i];
                
                i++;
            }
            
            settings.Dispose();
        }

        private void CalculateIndexOffset()
        {
            float position = _offset;
            
            int j;
            Vector2 p0 = Vector2.zero;
            Vector2 p1 = Vector2.zero;

            for (j = _indexOffset; j < _points.Length - 1; j++)
            {
                if (position < p1.x)
                {
                    break;
                }
                
                p0 = _points[j];
                p1 = _points[j + 1];
            }

            float i0 = (float) j / _points.Length;
            float i1 = (float) (j + 1) / _points.Length;
            float time = (p1.x - position) / (p1.x - p0.x);

            float t = Mathf.Lerp(i1, i0, time);

            int i;
            
            if (t >= 1f)
            {
                i = _points.Length - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * (_points.Length - 1) / 3;
                i = (int) t;
                i *= 3;
            }

            _indexOffset = i > 6 ? i - 6 : 0;
        }
        
        public void AddEntity(Transform t, MovingEntityData data)
        {
            data.Position = t.position;
            data.Rotation = t.rotation;
            
            _data[t] = data;
        }

        public void RemoveEntity(Transform t)
        {
            _data.Remove(t);
        }
    }
}