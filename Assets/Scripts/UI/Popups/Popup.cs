using System;
using System.Collections;
using General.Behaviours;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    [RequireComponent(typeof(Animator))]
    public class Popup : ExtendedBehaviour, IPopup
    {
        public Action<string> OnClosed { get; set; }
        public string Title { get; set; }
        public string[] Options { get; set; }

        [SerializeField] private Text _title;
        [SerializeField] private PopupOption _optionPrefab;
        [SerializeField] private Transform _optionsParent;

        private Animator _animator;
        private bool _closed;
        
        private static readonly int Closed = Animator.StringToHash("Showed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            foreach (string option in Options ?? new string[0])
            {
                PopupOption optionInstance = Instantiate(_optionPrefab, _optionsParent);
                optionInstance.Name = option;
                optionInstance.Button.onClick.AddListener(() =>
                {
                    if (!_closed)
                    {
                        Close(option);
                    }
                });
            }

            _title.text = Title;
        }

        public void Close(string optionName)
        {
            _closed = true;
            StartCoroutine(CloseAnimation());
            OnClosed?.Invoke(optionName);
        }

        public void CloseSilent()
        {
            _closed = true;
            StartCoroutine(CloseAnimation());
        }

        private IEnumerator CloseAnimation()
        {
            _animator.SetBool(Closed, false);
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
        }
    }
}