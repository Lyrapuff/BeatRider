using System;
using General.Behaviours;
using UnityEngine.InputSystem;

namespace General.Inputs
{
    public class UIInput : ExtendedBehaviour, IUIInput
    {
        public Action OnPaused { get; set; }

        private UIActions _uiActions;

        private void Awake()
        {
            _uiActions = new UIActions();
        }
        
        private void OnEnable()
        {
            _uiActions.Enable();
            _uiActions.Player.Pause.performed += HandlePaused;
        }

        private void OnDisable()
        {
            _uiActions.Disable();
            _uiActions.Player.Pause.performed -= HandlePaused;
        }

        private void HandlePaused(InputAction.CallbackContext context)
        {
            OnPaused?.Invoke();
        }
    }
}