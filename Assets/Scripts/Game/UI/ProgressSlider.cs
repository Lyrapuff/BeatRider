using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Slider))]
    public class ProgressSlider : MonoBehaviour
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
            _slider.value = (float)_audioSource.timeSamples / _audioSource.clip.samples;
        }
    }
}