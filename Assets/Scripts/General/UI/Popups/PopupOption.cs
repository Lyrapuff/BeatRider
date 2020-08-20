using System;
using General.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI.Popups
{
    [RequireComponent(typeof(Button))]
    public class PopupOption : ExtendedBehaviour
    {
        public Button Button { get; private set; }
        public string Name { get; set; }

        [SerializeField] private Text _text;

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        private void Start()
        {
            _text.text = Name;
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}