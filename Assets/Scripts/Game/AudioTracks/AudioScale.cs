using System.Linq;
using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;

namespace Game.World.AudioTracks
{
    public class AudioScale : ExtendedBehaviour
    {
        private IAudioAnalyzer _audioAnalyzer;

        private void Awake()
        {
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
        }

        private void Update()
        {
            float sin = Mathf.Abs(Mathf.Sin(transform.position.x + Time.time)) + 0.5f;
            
            Vector3 scale = transform.localScale;

            float y = _audioAnalyzer.Band?.Take(5).Average() ?? 0f;

            Vector3 newScale = new Vector3(scale.x, 10f + y * 70f + sin * 5f, scale.z);

            transform.localScale = Vector3.Lerp(scale, newScale, 0.05f);
        }
    }
}