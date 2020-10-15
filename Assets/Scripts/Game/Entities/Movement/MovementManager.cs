using System;
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
        
        private TransformAccessArray _entities;
        
        private JobHandle _forwardMovementHandle;
        private JobHandle _roadConnectorHandle;

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
            
            _entities = new TransformAccessArray(0);
        }

        private void OnDisable()
        {
            _forwardMovementHandle.Complete();
            _roadConnectorHandle.Complete();
        }

        private void Update()
        {
            ForwardMovementJob forwardMovementJob = new ForwardMovementJob();
            forwardMovementJob.AudioSpeed = _audioAnalyzer.Speed;
            forwardMovementJob.DeltaTime = Time.deltaTime;
            _forwardMovementHandle = forwardMovementJob.Schedule(_entities);
            
            RoadConnectorJob roadConnectorJob = new RoadConnectorJob();
            _roadConnectorHandle = roadConnectorJob.Schedule(_entities);
            
            _forwardMovementHandle.Complete();
            _roadConnectorHandle.Complete();
        }

        public void AddEntity(Transform entity)
        {
            _entities.capacity = _entities.length + 1;
            _entities.Add(entity);
        }
    }
}