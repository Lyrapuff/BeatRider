using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Entities.Movement
{
    public struct ForwardMovementJob : IJobParallelFor
    {
        public NativeArray<MovingEntityData> Data;
        public float DeltaTime;
        public float AudioSpeed;
        
        public void Execute(int index)
        {
            MovingEntityData data = Data[index];
            
            data.Position -= Vector3.forward * (AudioSpeed * DeltaTime * data.Speed);

            Data[index] = data;
        }
    }
}