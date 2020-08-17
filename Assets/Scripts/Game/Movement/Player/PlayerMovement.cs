using General.Behaviours;
using General.Inputs;
using UnityEngine;

namespace Game.Movement.Player
{
    [RequireComponent(typeof(IPlayerInput))]
    public class PlayerMovement : ExtendedBehaviour
    {
        [SerializeField] private float _speed;
        
        private IPlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<IPlayerInput, NullPlayerInput>();
        }

        private void Update()
        {
            transform.position += Vector3.right * (_input.Direction * _speed * Time.deltaTime);
            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -10f, 10f);
            transform.position = position;
        }
    }
}