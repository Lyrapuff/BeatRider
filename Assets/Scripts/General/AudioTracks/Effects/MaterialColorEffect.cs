using System;
using UnityEngine;

namespace General.AudioTracks.Effects
{
    [RequireComponent(typeof(AudioColorEffect))]
    public class MaterialColorEffect : MonoBehaviour
    {
        [SerializeField] private MaterialSetting[] _settings;

        private AudioColorEffect _audioColor;

        private void Awake()
        {
            _audioColor = GetComponent<AudioColorEffect>();
        }

        private void OnEnable()
        {
            _audioColor.OnColorChanged += HandleColorChanged;
        }

        private void OnDisable()
        {
            _audioColor.OnColorChanged -= HandleColorChanged;
        }

        private void HandleColorChanged(Color color)
        {
            foreach (MaterialSetting setting in _settings)
            {
                setting.Material.SetColor(setting.PropertyName, color);
            }
        }
    }

    [Serializable]
    public struct MaterialSetting
    {
        public string PropertyName;
        public Material Material;
    }
}