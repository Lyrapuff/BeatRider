using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

            foreach (MaterialSetting setting in _settings)
            {
                setting.MaterialReference.LoadAssetAsync<Material>().Completed += handle =>
                {
                    setting.Material = handle.Result;
                };
            }
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
                setting.Material.SetColor(setting.PropertyName, color * setting.Multiplier);
            }
        }
    }

    [Serializable]
    public class MaterialSetting
    {
        public string PropertyName;
        public AssetReference MaterialReference;
        [HideInInspector] public Material Material;
        public float Multiplier;
    }
}