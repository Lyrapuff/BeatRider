using General.Behaviours;
using Game.World;
using UnityEngine;

namespace Game.Movement
{
    public class RoadConnector : ExtendedBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _accuracy;

        private RoadHeight _roadHeight;
        private Vector3 _baseRotation;

        private void Start()
        {
            _roadHeight = FindObjectOfType<RoadHeight>();
            _baseRotation = transform.localEulerAngles;
        }

        private void LateUpdate()
        {
            SetPosition();
            SetRotation();
        }

        private void SetPosition()
        {
            Vector3 currentPosition = transform.position;
            
            float position = _roadHeight.GetHeight(currentPosition.z);
            currentPosition.y = position;
            currentPosition += _offset;
            
            transform.position = currentPosition;
        }

        private void SetRotation()
        {
            Vector3 currentPosition = transform.position;
            Vector3 futurePosition = currentPosition;
            
            float cPosition = _roadHeight.GetHeight(currentPosition.z);
            currentPosition.y = cPosition;
            
            float fPosition = _roadHeight.GetHeight(currentPosition.z + _accuracy);
            futurePosition.y = fPosition;
            futurePosition.z += _accuracy;

            Vector3 direction = (futurePosition - currentPosition).normalized;
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(_baseRotation);
        }
    }
}