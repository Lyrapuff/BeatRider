using Game.Services;
using General.Behaviours;
using UnityEngine;

namespace Entities.Player.Movement
{
    [RequireComponent(typeof(IPlayerInput))]
    public class PlayerMovement : ExtendedBehaviour
    {
        [SerializeField] private float _speed;
        
        private IPlayerInput _input;
        private IPause _pause;

        private void Awake()
        {
            _input = FindComponentOfInterface<IPlayerInput>();
            _pause = FindComponentOfInterface<IPause>();
        }

        private void Update()
        {
            if (_pause.Paused)
            {
                return;
            }
            
            transform.position += Vector3.right * (_input.Direction * _speed * Time.deltaTime);
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -10f, 10f);
            transform.position = position;
        }
    }
}