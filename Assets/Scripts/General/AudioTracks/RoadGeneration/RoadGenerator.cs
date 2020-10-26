using System.Collections.Generic;
using General.AudioTracks.Analyzing;
using UnityEngine;

namespace General.AudioTracks.RoadGeneration
{
    public class RoadGenerator
    {
        public Road Generate(AnalyzedAudio analyzedAudio, float length, int samples)
        {
            Road road = new Road();
            road.Points = new List<Vector2>();

            List<float> averages = analyzedAudio.Averages;

            List<float> beats = new List<float>();
            
            float timeStep = 1 / 10f;
            float time = 0f;
            float distance = 0f;
            float lengthPerSample = length / samples;
            
            float threshold = 0.55f;
            float step = 1.3f;
            float height = 0f;
            float direction;

            float distanceFromLastPoint = 0f;

            while (time < length)
            {
                int index = Mathf.FloorToInt(time / lengthPerSample) / 1024 / analyzedAudio.StoreEvery;

                if (index >= averages.Count)
                {
                    break;
                }

                float average = averages[index];

                if (average >= threshold)
                {
                    direction = -1f *
                                Mathf.Lerp(0.5f, 1f, average.Remap(threshold, 1f, 0f, 1f));
                }
                else
                {
                    direction
                        = 1f * Mathf.Lerp(0.5f, 1f, average.Remap(0f, threshold, 0f, 1f));
                }

                height += step * direction;
                
                if (distanceFromLastPoint >= 35f)
                {
                    road.Points.Add(new Vector2(distance, height));
                    distanceFromLastPoint = 0f;
                }

                float movement = (average * 200f + 1f) * timeStep;
                
                distance += movement;
                distanceFromLastPoint += movement;

                if (index > 0)
                {
                    float previous = averages[index - 1];
                    
                    if (average - previous >= 0.09f)
                    {
                        beats.Add(distance);
                    }
                }
                
                time += timeStep;
            }

            road.Points.Add(new Vector2(distance, height));

            road.Beats = beats;
            
            road.Length = distance;
            
            for (int i = 0; i < road.Points.Count; i++)
            {
                EnforceMode(i, road);
            }
            
            Debug.Log($"points: {road.Points.Count}");
            
            return road;
        }
        
        private void EnforceMode(int index, Road road)
        {
            int middleIndex = (index - 1) / 3;

            if (middleIndex == 0 || middleIndex == road.Points.Count - 1)
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

            Vector2 middle = road.Points[middleIndex];
            Vector2 enforcedTangent = middle - road.Points[fixedIndex];
            road.Points[enforcedIndex] = middle + enforcedTangent;
        }
    }
}