using System.Collections.Generic;
using General.Behaviours;
using UnityEngine;

namespace UI.GameConfiguration
{
    public class OptionSwitcher : ExtendedBehaviour
    {
        private List<Animator> _options;
        
        private static readonly int Showed = Animator.StringToHash("Showed");

        private void Awake()
        {
            _options = new List<Animator>();
            
            foreach (Transform child in transform)
            {
                Animator animator = child.GetComponent<Animator>();

                if (animator != null)
                {
                    _options.Add(animator);
                }
            }
        }

        private void OnEnable()
        {
            if (_options.Count > 0)
            {
                Show(0);
            }
        }

        public void Show(int index)
        {
            foreach (Animator animator in _options)
            {
                animator.SetBool(Showed, false);
            }

            if (index < _options.Count)
            {
                Animator animator = _options[index];
                animator.SetBool(Showed, true);
            }
        }
    }
}