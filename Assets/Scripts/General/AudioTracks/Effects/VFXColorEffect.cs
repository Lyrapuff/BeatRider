using UnityEngine;
using UnityEngine.VFX;

namespace General.AudioTracks.Effects
{
    [RequireComponent(typeof(VisualEffect))]
    public class VFXColorEffect : MonoBehaviour
    {
        private VisualEffect _visualEffect;
        private AudioColorEffect _audioColor;

        private void Awake()
        {
            _visualEffect = GetComponent<VisualEffect>();
            _audioColor = FindObjectOfType<AudioColorEffect>();
        }
        
        private void OnEnable()
        {
            _audioColor.OnColorChanged += SetColor;
            SetColor(_audioColor.CurrentColor);
        }

        private void OnDisable()
        {
            _audioColor.OnColorChanged -= SetColor;
        }

        private void SetColor(Color color)
        {
            color *= 0.7f;
            color.a = 255f;
            _visualEffect.SetVector4("Color", color);
        }
    }
}