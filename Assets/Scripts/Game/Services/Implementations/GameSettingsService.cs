using General.Audio;
using General.Behaviours;
using UnityEngine;

namespace Game.Services.Implementations
{
    public class GameSettingsService : ExtendedBehaviour
    {
        public Configuration Configuration { get; set; } = new Configuration();
        public AudioClip Clip { get; set; }
        public AnalyzedAudio AnalyzedAudio { get; set; }
    }
}