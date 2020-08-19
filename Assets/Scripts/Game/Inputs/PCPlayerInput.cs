using System;
using General.Behaviours;
using General.Inputs;
using General.Services.GameStatus;
using General.Services.Pause;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.Inputs
{
    public class PCPlayerInput : ExtendedBehaviour, IPlayerInput
    {
        public bool ReadInputs
        {
            get => !_pause.Paused && !_crushed;
            set => throw new NotImplementedException();
        }

        public Action OnBack { get; set; }
        public Action OnPaused { get; set; }
        public float Direction { get; private set; }

        private PauseService _pause;
        private GameStatusService _gameStatus;
        private bool _crushed;

        private void Awake()
        {
            _pause = Toolbox.Instance.GetService<PauseService>();
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();

            _gameStatus.OnChanged += ChangedHandle;
        }

        private void ChangedHandle(GameStatusChangeType changeType, object dataOverload)
        {
            switch (changeType)
            {
                case GameStatusChangeType.Started:
                    _crushed = false;
                    break;
                case GameStatusChangeType.Crushed:
                    _crushed = true;
                    break;
                case GameStatusChangeType.Continued:
                    _crushed = false;
                    break;
            }
        }

        private void Update()
        {
            if (!ReadInputs)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OnPaused?.Invoke();
                }

                Direction = 0f;

                return;
            }
            
            if (Input.GetMouseButton(0))
            {
                Direction = Input.mousePosition.x > Screen.width * 0.5f ? 1f : -1f;
            }
            else
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                Direction = horizontal;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Direction = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPaused?.Invoke();
                OnBack?.Invoke();
            }
        }
    }
}