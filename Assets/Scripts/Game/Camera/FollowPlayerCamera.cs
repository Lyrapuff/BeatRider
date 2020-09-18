using General.Behaviours;
using UnityEngine;

namespace Game.Camera
{
    public class FollowPlayerCamera : ExtendedBehaviour
    {
        public Vector3 Offset { get; set; } = new Vector3(0f, 0f, 0f);
        
        [SerializeField] private Transform _player;
        [SerializeField] private float _xSmoothing;

        private Vector3 _xVelocity = Vector3.one;

        private void Update()
        {
            SmoothFollow();
        }

        private void SmoothFollow()
        {
            Vector3 targetPosition = _player.position + Offset;
            Vector3 currentPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _xVelocity, _xSmoothing);

            transform.position = currentPosition;

            Vector3 position = transform.position;
            position.y = _player.position.y + Offset.y;
            transform.position = position;
        }
    }
}