using General.AudioTracks.Analyzing;
using General.Behaviours;
using UnityEngine;
using UnityEngine.VFX;

namespace Game.World.Environments.Forest
{
    [RequireComponent(typeof(VisualEffect))]
    public class AudioPatricles : ExtendedBehaviour
    {
        private VisualEffect _vfx;
        private IAudioAnalyzer _audioAnalyzer;

        private void Awake()
        {
            _vfx = GetComponent<VisualEffect>();
            _audioAnalyzer = FindComponentOfInterface<IAudioAnalyzer, NullAudioAnalyzer>();
        }

        private void Update()
        {
            Vector3 velocity = new Vector3(0f, -(_audioAnalyzer.PureSpeed * 3.5f) - 3f, -(_audioAnalyzer.PureSpeed * 60f) - 5f);
            _vfx.SetVector3("Velocity", velocity);
        }
    }
}