using General.Audio;
using General.Behaviours;
using UnityEngine;

namespace Game.World.Audio
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

            float y = _audioAnalyzer.PureSpeed;

            Vector3 newScale = new Vector3(scale.x, 10f + y * 60f + sin * 5f, scale.z);

            transform.localScale = Vector3.Lerp(scale, newScale, 0.05f);
        }
    }
}