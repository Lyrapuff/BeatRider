using System;
using System.IO;
using System.Threading.Tasks;
using General.AudioTracks.Searching;
using Un4seen.Bass;
using UnityEngine;

namespace General.AudioTracks.Processing
{
    [CreateAssetMenu(menuName = "Tracks/Processor/PCM")]
    public class TrackPCMProcessor : TrackProcessor
    {
        public override Task Process(ISearchResult searchResult, Action<bool> OnProcessed)
        {
            string path = Context.Path;

            if (File.Exists(path + "/audio.m4a"))
            {

                Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

                int num = Bass.BASS_StreamCreateFile(path + "/audio.m4a", 0L, 0L, BASSFlag.BASS_STREAM_DECODE);

                float freq = 0;
                Bass.BASS_ChannelGetAttribute(num, BASSAttribute.BASS_ATTRIB_FREQ, ref freq);

                long length = Bass.BASS_ChannelGetLength(num, BASSMode.BASS_POS_BYTES);
                double seconds = Bass.BASS_ChannelBytes2Seconds(num, length);

                int samples = Mathf.RoundToInt((float) (seconds * freq));

                short[] waveBuffer = new short[length];

                Bass.BASS_ChannelGetData(num, waveBuffer, (int) length);

                float[] floatWave = new float[length];

                for (int i = 0; i < length; i++)
                {
                    floatWave[i] = waveBuffer[i] / 32768f;
                }

                Bass.BASS_StreamFree(num);

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

            return Task.CompletedTask;
        }
    }
}