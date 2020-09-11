using UnityEngine;
using UnityEngine.UI;

namespace General.AudioTracks.Effects
{
    [RequireComponent(typeof(Image))]
    public class ImageColorEffect : MonoBehaviour
    {
        [SerializeField] private float _multiplier;
        [SerializeField] private float _alpha = 255;
        
        private Image _image;
        private AudioColorEffect _audioColor;

        private void Awake()
        {
            _image = GetComponent<Image>();
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
            color *= _multiplier;
            color.a = _alpha / 255f;
            _image.color = color;
        }
    }
}