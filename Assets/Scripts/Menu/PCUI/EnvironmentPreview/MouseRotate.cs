using UnityEngine;

namespace UI.PCUI.EnvironmentPreview
{
    public class MouseRotate : MonoBehaviour
    {
        [SerializeField] private Transform _go;
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _deceleration;
        [SerializeField] private Vector3 _staticVelocity;

        private float _velocity;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                float y = Input.GetAxis("Mouse Y");

                _velocity += y * _sensitivity * Time.deltaTime;
            }

            _go.Rotate(_staticVelocity * Time.deltaTime + Vector3.right * _velocity);

            _velocity *= _deceleration;
        }
    }
}