using Game.CPURoad;
using UnityEngine;

namespace Game.Road
{
    public class RoadConnector : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _accuracy;
        
        private RoadHeight _roadMovement;

        private void Awake()
        {
            _roadMovement = FindObjectOfType<RoadHeight>();
        }

        private void Update()
        {
            SetPosition();
            SetRotation();
        }

        private void SetPosition()
        {
            Vector3 position = transform.position;
            
            float height = _roadMovement.GetHeight(position);
            
            position.y = Mathf.Lerp(position.y, height, 0.5f);
            transform.position = position + _offset;
        }

        private void SetRotation()
        {
            Vector3 currentPosition = transform.position;
            Vector3 futurePosition = currentPosition;
            
            float cPosition = _roadMovement.GetHeight(currentPosition);
            currentPosition.y = cPosition;
            
            float fPosition = _roadMovement.GetHeight(currentPosition + Vector3.one * _accuracy);
            futurePosition.y = fPosition;
            futurePosition.z += 0.2f;

            Vector3 direction = (futurePosition - currentPosition).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}