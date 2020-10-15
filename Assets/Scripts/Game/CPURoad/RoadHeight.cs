using General.AudioTracks.Analyzing;
using General.AudioTracks.RoadGeneration;
using General.Behaviours;
using General.Storage;
using UnityEngine;

namespace Game.CPURoad
{
    public class RoadHeight : ExtendedBehaviour
    {
        public static RoadHeight Instance { get; private set; }
        
        public float Offset { get; private set; }

        private IAudioAnalyzer _audioAnalyzer;
        private Road _road;
        
        private static readonly int OffsetID = Shader.PropertyToID("_Offset");

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer>();
            
            IStorage storage = FindComponentOfInterface<IStorage>();
            _road = storage.Get<Road>("Game/Road");

            Instance = this;
        }

        private void Update()
        {
            Offset += Time.deltaTime * _audioAnalyzer.Speed;
            Shader.SetGlobalFloat(OffsetID, Offset);
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

            for (j = 0; j < _road.Points.Count - 1; j++)
            {
                if (position < p1.x)
                {
                    break;
                }
                
                p0 = _road.Points[j];
                p1 = _road.Points[j + 1];
            }

            float i0 = (float) j / _road.Points.Count;
            float i1 = (float) (j + 1) / _road.Points.Count;
            float time = (p1.x - position) / (p1.x - p0.x);

            float t = Mathf.Lerp(i1, i0, time);

            int i;
            
            if (t >= 1f)
            {
                t = 1f;
                i = _road.Points.Count - 4;
            }
            else
            {
                t = Mathf.Clamp01(t) * (_road.Points.Count - 1) / 3;
                i = (int) t;
                t -= i;
                i *= 3;
            }

            Vector2 value = GetPoint(_road.Points[i], _road.Points[i + 1], _road.Points[i + 2], _road.Points[i + 3], t);

            if (z == 0f)
            {
                if (i > 6)
                {
                    i -= 6;
                    _road.Points.RemoveAt(i);
                    _road.Points.RemoveAt(i + 1);
                    _road.Points.RemoveAt(i + 2);
                }
            }
            
            return value.y;
        }
    }
}