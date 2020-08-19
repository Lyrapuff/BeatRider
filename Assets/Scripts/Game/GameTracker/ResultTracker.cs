using General.AudioTracks.Analyzing;
using General.Behaviours;
using Game.Player;
using UnityEngine;

namespace Game.GameTracker
{
    public class ResultTracker : ExtendedBehaviour
    {
        public GameResult GameResult => _gameResult;
        
        private IAudioAnalyzer _audioAnalyzer;
        private GameResult _gameResult;

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
            
            _gameResult = new GameResult();
        }

        private void Update()
        {
            _gameResult.DistanceDriven += _audioAnalyzer.PureSpeed * Time.deltaTime;
        }

        private void OnEnable()
        {
            PointIncrementer.OnPointIncremented += HandlePointIncremented;
            RepetitiveGame.OnRepeat += HandleRepeat;
        }

        private void OnDisable()
        {
            PointIncrementer.OnPointIncremented -= HandlePointIncremented;
            RepetitiveGame.OnRepeat -= HandleRepeat;
        }

        private void HandlePointIncremented()
        {
            _gameResult.CoinCount++;
        }
        
        private void HandleRepeat(int _)
        {
            _gameResult.RepeatCount++;
        }
    }
}