using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Continue
{
    [RequireComponent(typeof(Image))]
    public class ContinueAnimation : MonoBehaviour
    {
        [SerializeField] private ContinueTimer _timer;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _timer.OnTick += TickHandle;
        }

        private void OnDisable()
        {
            _timer.OnTick -= TickHandle;
        }

        private void TickHandle(float time)
        {
            _image.fillAmount = Mathf.Lerp(1f, 0f, time);
        }
    }
}