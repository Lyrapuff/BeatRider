using System;
using System.Collections;
using General.Behaviours;
using UnityEngine;

namespace General.UI.Popups.CanvasManagement
{
    [RequireComponent(typeof(Canvas))]
    public class SwitchableCanvas : ExtendedBehaviour
    {
        public Action OnOpened;
        public Action OnClosed;
        public string Name => _name;
        
        [SerializeField] private string _name;
        [SerializeField] private GameObject _content;

        public void Open()
        {
            _content.SetActive(true);
            OnOpened?.Invoke();
        }

        public void Close()
        {
            StartCoroutine(CloseAfter(0.2f));
            OnClosed?.Invoke();
        }

        public void CloseHard()
        {
            _content.SetActive(false);
            OnClosed?.Invoke();
        }

        private IEnumerator CloseAfter(float time)
        {
            yield return new WaitForSeconds(time);
            _content.SetActive(false);
        }
    }
}