using UnityEngine;
using UnityEngine.Jobs;

namespace Game.CPURoad
{
    public struct RoadConnectorJob : IJobParallelForTransform
    {
        public void Execute(int index, TransformAccess transform)
        {
            Vector3 currentPosition = transform.position;
            Vector3 futurePosition = currentPosition;
            
            float height = RoadHeight.Instance.GetHeight(currentPosition.z);
            
            currentPosition.y = Mathf.Lerp(currentPosition.y, height, 0.5f);
            transform.position = currentPosition;
            
            float cPosition = RoadHeight.Instance.GetHeight(currentPosition.z);
            currentPosition.y = cPosition;
            
            float fPosition = RoadHeight.Instance.GetHeight(currentPosition.z + 0.2f);
            futurePosition.y = fPosition;
            futurePosition.z += 0.2f;

            Vector3 direction = (futurePosition - currentPosition).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}