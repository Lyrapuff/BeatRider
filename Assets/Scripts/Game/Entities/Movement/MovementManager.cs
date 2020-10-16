using System;
using System.Collections.Generic;
using Game.CPURoad;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Entities.Movement
{
    public class MovementManager : ExtendedBehaviour
    {
        private IAudioAnalyzer _audioAnalyzer;
        
        private List<Transform> _entities = new List<Transform>();
        
        private JobHandle _forwardMovementHandle;
        private JobHandle _roadConnectorHandle;

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
        }

        private void OnDisable()
        {
            _forwardMovementHandle.Complete();
            _roadConnectorHandle.Complete();
        }

        private void Update()
        {
            TransformAccessArray taa = new TransformAccessArray(_entities.ToArray());
            
            ForwardMovementJob forwardMovementJob = new ForwardMovementJob();
            forwardMovementJob.AudioSpeed = _audioAnalyzer.Speed;
            forwardMovementJob.DeltaTime = Time.deltaTime;
            _forwardMovementHandle = forwardMovementJob.Schedule(taa);
            
            RoadConnectorJob roadConnectorJob = new RoadConnectorJob();
            _roadConnectorHandle = roadConnectorJob.Schedule(taa);
            
            _forwardMovementHandle.Complete();
            _roadConnectorHandle.Complete();
            
            taa.Dispose();
        }

        public void AddEntity(Transform entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(Transform entity)
        {
            _entities.Remove(entity);
        }
    }
}