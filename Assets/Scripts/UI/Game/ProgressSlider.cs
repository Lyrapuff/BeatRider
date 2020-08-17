using General.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    [RequireComponent(typeof(Slider))]
    public class ProgressSlider : ExtendedBehaviour
    {
        private Slider _slider;
        private AudioSource _audioSource;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _audioSource = FindObjectOfType<AudioSource>();
        }

        private void Update()
        {
            _slider.value = _audioSource.timeSamples / (float)_audioSource.clip.samples;
        }
    }
}