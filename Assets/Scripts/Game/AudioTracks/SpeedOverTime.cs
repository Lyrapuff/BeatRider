using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;

namespace Game.AudioTracks
{
    public class SpeedOverTime : ExtendedBehaviour
    {
        [SerializeField] private float _growPerCycle;
        
        private IAudioAnalyzer _audioAnalyzer;
        
        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();

            RepetitiveGame.OnRepeat += HandleRepeat;
        }

        private void HandleRepeat(int count)
        {
            _audioAnalyzer.SpeedMultiplier += _growPerCycle;
        }
    }
}