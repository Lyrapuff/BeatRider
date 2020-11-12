using Entities.Movement;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Game.CPURoad
{
    
    public struct RoadConnectorJob : IJobParallelFor
    {
        public NativeArray<MovingEntityData> Data;
        
        [ReadOnly]
        public NativeArray<Vector2> Points;
        public float Offset;
        public int IndexOffset;

        public void Execute(int index)
        {
            MovingEntityData data = Data[index];
            
            Vector3 currentPosition = data.Position;
            Vector3 futurePosition = currentPosition;
            
            float height = GetHeight(currentPosition.z);
            
            currentPosition.y = Mathf.Lerp(currentPosition.y, height, 0.5f);
            data.Position = currentPosition;
            
            float cPosition = GetHeight(currentPosition.z);
            currentPosition.y = cPosition;
            
            float fPosition = GetHeight(currentPosition.z + 0.2f);
            futurePosition.y = fPosition;
            futurePosition.z += 0.2f;

            Vector3 direction = (futurePosition - currentPosition).normalized;
            data.Rotation = Quaternion.LookRotation(direction);

            Data[index] = data;
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
        
        public float GetHeight(float z)
        {
            float position = Offset + z;
            
            int j;
            Vector2 p0 = Vector2.zero;
            Vector2 p1 = Vector2.zero;

            for (j = IndexOffset; j < Points.Length - 1; j++)
            {
                if (position < p1.x)
                {
                    break;
                }
                
                p0 = Points[j];
                p1 = Points[j + 1];
            }

            float i0 = (float) j / Points.Length;
            float i1 = (float) (j + 1) / Points.Length;
            float time = (p1.x - position) / (p1.x - p0.x);

            float t = Mathf.Lerp(i1, i0, time);

            int i;
            
            if (t >= 1f)
            {
                t = 1f;
                i = Points.Length - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * (Points.Length - 1) / 3;
                i = (int) t;
                t -= i;
                i *= 3;
            }

            Vector2 value = GetPoint(Points[i], Points[i + 1], Points[i + 2], Points[i + 3], t);

            return value.y;
        }
    }
}