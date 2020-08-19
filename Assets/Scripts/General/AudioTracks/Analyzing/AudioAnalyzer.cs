using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using DSPLib;
using UnityEngine;

namespace General.AudioTracks.Analyzing
{
    public class AudioAnalyzer
    {
        public void Analyze(float[] wave, int channels, int samples, Action<AnalyzedAudio> onAnalyzed)
        {
            AnalyzedAudio analyzedAudio = new AnalyzedAudio();

            Task.Run(() =>
            {
                try
                {
                    float[] combinedSamples = new float[wave.Length];

                    
                    int processed = 0;
                    float average = 0f;

                    for (int i = 0; i < wave.Length; i++)
                    {
                        average += wave[i];

                        if ((i + 1) % channels == 0)
                        {
                            combinedSamples[processed] = average / channels;
                            average = 0f;

                            processed++;
                        }
                    }

                    int spectrumSampleSize = 1024;
                    int iterations = combinedSamples.Length / spectrumSampleSize;

                    FFT fft = new FFT();
                    fft.Initialize((uint) spectrumSampleSize);

                    List<float[]> bandsAll = new List<float[]>();
                    List<float> averages = new List<float>();

                    double[] sampleChunk = new double[spectrumSampleSize];

                    for (int i = 0; i < iterations; i++)
                    {
                        if (i % analyzedAudio.StoreEvery != 0)
                        {
                            continue;
                        }

                        Array.Copy(combinedSamples, i * spectrumSampleSize, sampleChunk, 0, spectrumSampleSize);

                        double[] windowCoefs =
                            DSP.Window.Coefficients(DSP.Window.Type.Hanning, (uint) spectrumSampleSize);
                        double[] scaledSpectrumChunk = DSP.Math.Multiply(sampleChunk, windowCoefs);
                        double scaleFactor = DSP.Window.ScaleFactor.Signal(windowCoefs);

                        Complex[] fftSpectrum = fft.Execute(scaledSpectrumChunk);
                        double[] scaledFFTSpectrum = DSP.ConvertComplex.ToMagnitude(fftSpectrum);
                        scaledFFTSpectrum = DSP.Math.Multiply(scaledFFTSpectrum, scaleFactor);

                        float[] currentSpectrum = scaledFFTSpectrum.Select(x => (float) x).ToArray();
                        float[] bands = new float[8];

                        int count = 0;

                        for (int j = 0; j < bands.Length; j++)
                        {
                            float sampleCount = (int) Mathf.Pow(2, j + 1);

                            float averageValue = 0f;

                            for (int k = 0; k < sampleCount; k++)
                            {
                                averageValue += currentSpectrum[count] * (count + 1);
                                count++;
                            }

                            averageValue /= count;

                            bands[j] = averageValue;
                        }

                        bandsAll.Add(bands);
                        averages.Add(bands.Skip(analyzedAudio.Skip).Take(analyzedAudio.Take).Average());
                    }

                    float min = averages.Min();
                    float max = averages.Max();

                    for (int i = 0; i < averages.Count; i++)
                    {
                        averages[i] = Mathf.InverseLerp(min, max, averages[i]);
                    }
                    
                    analyzedAudio.Bands = bandsAll;
                    analyzedAudio.Averages = averages;

                    onAnalyzed?.Invoke(analyzedAudio);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            });
        }

        public void Analyze(AudioClip audioClip, Action<AnalyzedAudio> onAnalyzed)
        {
            int channels = audioClip.channels;
            int samples = audioClip.samples;

            float[] multiChannelSamples = new float[samples * channels];
            audioClip.GetData(multiChannelSamples, 0);

            Analyze(multiChannelSamples, channels, samples, onAnalyzed);
        }
    }
}