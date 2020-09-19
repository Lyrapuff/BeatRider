﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/Road generator")]
    public class RoadGeneratorProcessor : TrackProcessor
    {
        public override void Process(IAudioTrack track, Action<bool> OnProcessed)
        {
            if (Context.AnalyzedAudio == null)
            {
                Debug.LogError("RoadGeneratorProcessor should be called after audio was analyzed.", this);
                OnProcessed?.Invoke(false);
                return;
            }

            if (!File.Exists(Context.Path + "/road.bytes"))
            {
                float[] road = new float[Mathf.FloorToInt(Context.AnalyzedAudio.Averages.Sum())];

                for (int i = 0; i < road.Length; i++)
                {
                    road[i] = Mathf.PerlinNoise(i / 211.42f, 0.53f);
                }

                Context.Road = road;
                
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream file = new FileStream(Context.Path + "/road.bytes", FileMode.Create))
                {
                    formatter.Serialize(file, road);
                }
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                
                using (StreamReader reader = new StreamReader(Context.Path + "/road.bytes"))
                {
                    float[] road = formatter.Deserialize(reader.BaseStream) as float[];

                    Context.Road = road;
                }
            }
            
            OnProcessed?.Invoke(true);
        }
    }
}