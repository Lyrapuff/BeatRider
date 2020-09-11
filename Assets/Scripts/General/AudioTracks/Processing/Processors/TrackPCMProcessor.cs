using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using Un4seen.Bass;
using UnityEngine;

namespace General.AudioTracks.Processing.Processors
{
    [CreateAssetMenu(menuName = "Tracks/Processor/PCM")]
    public class TrackPCMProcessor : TrackProcessor
    {
        public override void Process(IAudioTrack track, Action<bool> OnProcessed)
        {
            string path = Context.Path;

            if (File.Exists(path + "/audio.m4a"))
            {
                Debug.Log("Init");
                bool yes = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

                if (!yes)
                {
                    Debug.Log("init: " + Bass.BASS_ErrorGetCode());
                }

                string pluginPath = Application.streamingAssetsPath + @"/BassPlugins/";

                Dictionary<int, string> plugins = Bass.BASS_PluginLoadDirectory(pluginPath);
                
                Debug.Log($"plguin count: " + plugins?.Count);
                
                foreach (KeyValuePair<int, string> keyValuePair in plugins)
                {
                    Debug.Log($"plguin {keyValuePair.Key}: {keyValuePair.Value}");
                }
                
                Debug.Log("Stream create");
                
                int num = Bass.BASS_StreamCreateFile(path + "/audio.m4a", 0L, 0L, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_UNICODE);

                if (num == 0)
                {
                    Debug.Log("num: " + Bass.BASS_ErrorGetCode());
                }
                
                Debug.Log("Freq get");
                float freq = 0;
                bool success = Bass.BASS_ChannelGetAttribute(num, BASSAttribute.BASS_ATTRIB_FREQ, ref freq);

                if (!success)
                {
                    Debug.Log("freq error: " + Bass.BASS_ErrorGetCode());
                }
                
                Debug.Log("Length calc");
                long length = Bass.BASS_ChannelGetLength(num, BASSMode.BASS_POS_BYTES);

                if (length == -1)
                {
                    Debug.Log("length error: " + Bass.BASS_ErrorGetCode());
                }
                
                Debug.Log("Seconds calc");
                double seconds = Bass.BASS_ChannelBytes2Seconds(num, length);

                if (seconds == -1)
                {
                    Debug.Log("seconds error: " + Bass.BASS_ErrorGetCode());
                }
                
                Debug.Log("length: " + length);
                Debug.Log("seconds: " + seconds);
                Debug.Log("freq: " + freq);
                Debug.Log("seconds * freq: " + seconds * freq);
                
                Debug.Log("Samples calc");
                int samples = Mathf.RoundToInt((float) (seconds * freq));

                short[] waveBuffer = new short[length];

                Debug.Log("Filling buffer");
                Bass.BASS_ChannelGetData(num, waveBuffer, (int) length);

                Debug.Log("Converting to float");
                float[] floatWave = new float[length];

                for (int i = 0; i < length; i++)
                {
                    floatWave[i] = waveBuffer[i] / 32768f;
                }

                Debug.Log("Stream free");
                Bass.BASS_StreamFree(num);

                foreach (var plugin in plugins)
                {
                    Bass.BASS_PluginFree(plugin.Key);
                }

                Bass.BASS_Free();
                
                Context.Wave = floatWave;
                Context.Channels = 2;
                Context.Samples = samples;
                Context.Frequency = (int) freq;

                OnProcessed?.Invoke(true);
            }
            else
            {
                OnProcessed?.Invoke(false);
            }
        }
    }
}