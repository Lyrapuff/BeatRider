using General.Behaviours;
using General.UI.CanvasManagement;
using UnityEngine;

namespace UI.CanvasManagement
{
    [RequireComponent(typeof(Animator))]
    public class CanvasSwitchAnimation : ExtendedBehaviour
    {
        [SerializeField] private SwitchableCanvas _switchableCanvas;

        private Animator _animator;
        private static readonly int Showed = Animator.StringToHash("Showed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _switchableCanvas.OnOpened += OpenedHandle;
            _switchableCanvas.OnClosed += ClosedHandle;
        }

        private void OnDisable()
        {
            _switchableCanvas.OnOpened -= OpenedHandle;
            _switchableCanvas.OnClosed -= ClosedHandle;
        }

        private void OpenedHandle()
        {
            _animator.SetBool(Showed, true);
        }

        private void ClosedHandle()
        {
            _animator.SetBool(Showed, false);
        }
    }
}