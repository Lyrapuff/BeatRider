using System;
using System.Collections;
using UnityEngine;

namespace General.UI.Windows
{
    [RequireComponent(typeof(Window))]
    [RequireComponent(typeof(Animator))]
    public class WindowAnimator : MonoBehaviour
    {
        private Window _window;
        private Animator _animator;
        
        private static readonly int Showed = Animator.StringToHash("Showed");

        private void Awake()
        {
            _window = GetComponent<Window>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _window.OnClosed += HandleClosed;
        }
        
        private void OnDisable()
        {
            _window.OnClosed -= HandleClosed;
        }
        
        private void HandleClosed()
        {
            _animator.SetBool(Showed, false);
            StartCoroutine(Clear());
        }

        private IEnumerator Clear()
        {
            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Hidden"))
            {
                yield return new WaitForFixedUpdate();
            }
            
            Destroy(gameObject);
        }
    }
}