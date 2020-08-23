using General.Behaviours;
using General.Storage;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.World.AudioTracks
{
    public class AudioStatus : ExtendedBehaviour
    {
        [SerializeField] private AudioMixer _mixer;

        private void Start()
        {
            HandleChanged(GameDataStorage.Instance.GetData());
        }

        private void OnEnable()
        {
            GameDataStorage.Instance.OnChanged += HandleChanged;
        }

        private void OnDisable()
        {
            GameDataStorage.Instance.OnChanged -= HandleChanged;
        }

        private void HandleChanged(GameData gameData)
        {
            _mixer.SetFloat("Volume", gameData.MusicEnabled ? 0f : -80f);
        }
    }
}