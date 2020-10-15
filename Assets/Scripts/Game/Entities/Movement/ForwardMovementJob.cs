using UnityEngine;
using UnityEngine.Jobs;

namespace Entities.Movement
{
    public struct ForwardMovementJob : IJobParallelForTransform
    {
        public float DeltaTime;
        public float AudioSpeed;
        
        public void Execute(int index, TransformAccess transform)
        {
            transform.position -= Vector3.forward * (AudioSpeed * DeltaTime);
        }
    }
}