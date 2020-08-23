using General.Behaviours;
using General.Inputs;

namespace Game.Services.Implementation
{
    public class PlayerInput : ExtendedBehaviour, IPlayerInput
    {
        public bool ReadInputs { get; set; } = true;
        public float Direction { get; private set; }

        private PlayerActions _playerActions;

        private void Awake()
        {
            _playerActions = new PlayerActions();
        }

        private void OnEnable()
        {
            _playerActions.Enable();
        }

        private void OnDisable()
        {
            _playerActions.Disable();
        }

        private void Update()
        {
            if (ReadInputs)
            {
                Direction = _playerActions.Car.Move.ReadValue<float>();
            }
        }
    }
}