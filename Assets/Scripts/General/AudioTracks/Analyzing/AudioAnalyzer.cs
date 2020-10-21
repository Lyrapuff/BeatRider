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
        private float[][] _bands;
        private float[] _averages;

        private object _bandsLock = new object();
        private object _averagesLock = new object();
        
        public AnalyzedAudio Analyze(float[] wave, int channels)
        {
            AnalyzedAudio analyzedAudio = new AnalyzedAudio();

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
                int iterations = combinedSamples.Length / spectrumSampleSize / analyzedAudio.StoreEvery;

                _bands = new float[iterations + 1][];
                _averages = new float[iterations + 1];
                
                Parallel.For(0, iterations, i =>
                {
                    FFT fft = new FFT();
                    fft.Initialize((uint) spectrumSampleSize);
                    
                    double[] sampleChunk = new double[spectrumSampleSize];

                    Array.Copy(combinedSamples, i * spectrumSampleSize * analyzedAudio.StoreEvery, sampleChunk, 0,
                        spectrumSampleSize);

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

                    SetBand(i, bands);
                    SetAverage(i, bands.Skip(analyzedAudio.Skip).Take(analyzedAudio.Take).Average());
                });

                int emptyCount = 0;

                for (int i = 0; i < _averages.Length; i++)
                {
                    if (_averages[i] == 0f)
                    {
                        emptyCount++;
                    }
                    else
                    {
                        emptyCount = 0;
                    }

                    if (emptyCount >= 300)
                    {
                        int index = i - emptyCount;

                        Array.Resize(ref _averages, index + 1);
                        Array.Resize(ref _bands, index + 1);
                        
                        break;
                    }
                }
                
                float min = _averages.Min();
                float max = _averages.Max();
                
                for (int i = 0; i < _averages.Length; i++)
                {
                    _averages[i] = Mathf.InverseLerp(min, max, _averages[i]);
                }

                analyzedAudio.Bands = _bands.ToList();
                analyzedAudio.Averages = _averages.ToList();

                return analyzedAudio;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return null;
        }

        public void Analyze(AudioClip audioClip, Action<AnalyzedAudio> onAnalyzed)
        {
            int channels = audioClip.channels;
            int samples = audioClip.samples;

            float[] multiChannelSamples = new float[samples * channels];
            audioClip.GetData(multiChannelSamples, 0);

            onAnalyzed?.Invoke(Analyze(multiChannelSamples, channels));
        }

        private void SetAverage(int i, float average)
        {
            lock (_averagesLock)
            {
                _averages[i] = average;
            }
        }
        
        private void SetBand(int i, float[] band)
        {
            lock (_bandsLock)
            {
                _bands[i] = band;
            }
        }
    }
}