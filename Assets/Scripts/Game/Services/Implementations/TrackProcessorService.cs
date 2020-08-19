using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using Un4seen.Bass;
using UnityEngine;

namespace Game.Services.Implementations
{
    public class TrackProcessorService : ExtendedBehaviour
    {
        public void Process(string path, Action<AudioClip, AnalyzedAudio> onProcessed)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            int num = Bass.BASS_StreamCreateFile(path + "/audio.m4a", 0L, 0L, BASSFlag.BASS_STREAM_DECODE);

            float freq = 0;
            Bass.BASS_ChannelGetAttribute(num, BASSAttribute.BASS_ATTRIB_FREQ, ref freq);

            long length = Bass.BASS_ChannelGetLength(num, BASSMode.BASS_POS_BYTES);
            double seconds = Bass.BASS_ChannelBytes2Seconds(num, length);

            int samples = Mathf.RoundToInt((float) (seconds * freq));

            short[] waveBuffer = new short[length];

            Bass.BASS_ChannelGetData(num, waveBuffer, (int)length);

            float[] floatWave = new float[length];

            for (int i = 0; i < length; i++)
            {
                floatWave[i] = waveBuffer[i] / 32768f;
            }
            
            Bass.BASS_StreamFree(num);

            AudioClip clip = AudioClip.Create("test", samples, 2, (int)freq, false);
            clip.SetData(floatWave, 0);

            if (!File.Exists(path + "spectrum.bytes"))
            {
                AudioAnalyzer audioAnalyzer = new AudioAnalyzer();

                audioAnalyzer.Analyze(clip, analyzedAudio =>
                {
                    onProcessed?.Invoke(clip, analyzedAudio);

                    BinaryFormatter formatter = new BinaryFormatter();

                    using (FileStream file = new FileStream(path + "/spectrum.bytes", FileMode.Create))
                    {
                        formatter.Serialize(file, analyzedAudio);
                    }
                });
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                
                using (StreamReader reader = new StreamReader(path + "spectrum.bytes"))
                {
                    AnalyzedAudio analyzedAudio = formatter.Deserialize(reader.BaseStream) as AnalyzedAudio;
                    
                    onProcessed?.Invoke(clip, analyzedAudio);
                }
            }
        }
    }
}