using System;
using General.Behaviours;
using General.Inputs;

namespace Game.Services.Implementation
{
    public class PauseService : ExtendedBehaviour, IPause
    {
        public Action OnPaused { get; set; }
        public Action OnUnpaused { get; set; }
        public bool Paused { get; private set; }

        private IUIInput _uiInput;

        private void Awake()
        {
            _uiInput = FindComponentOfInterface<IUIInput>();
        }

        private void OnEnable()
        {
            _uiInput.OnPaused += Switch;
        }

        private void OnDisable()
        {
            _uiInput.OnPaused -= Switch;
        }

        public void Pause()
        {
            if (!Paused)
            {
                Paused = true;
                OnPaused?.Invoke();
            }
        }

        public void Unpause()
        {
            if (Paused)
            {
                Paused = false;
                OnUnpaused?.Invoke();
            }
        }

        public void Switch()
        {
            if (Paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
}