using General.Audio;
using General.Behaviours;
using UnityEngine;

namespace Game.Services.Implementations
{
    public class GameSettingsService : ExtendedBehaviour
    {
        public Configuration Configuration { get; set; }
        public AudioClip Clip { get; set; }
        public AnalyzedAudioObject AnalyzedAudio { get; set; }
    }
}