using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.PCUI.EnvironmentPreview
{
    public class MouseRotate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Transform _go;
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _deceleration;
        [SerializeField] private Vector3 _staticVelocity;

        private float _velocity;
        private bool _rotate;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _rotate = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rotate = false;
        }

        private void Update()
        {
            if (_rotate)
            {
                float x = Input.GetAxis("Mouse X");

                _velocity += x * _sensitivity * Time.deltaTime;
            }

            _go.Rotate(_staticVelocity * Time.deltaTime + Vector3.up * _velocity);

            _velocity *= _deceleration;
        }
    }
}