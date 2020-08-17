using System;
using UnityEngine;

namespace UI.Popups
{
    public class NullPopupFactory : IPopupFactory
    {
        public NullPopupFactory()
        {
            Debug.LogError(typeof(NullPopupFactory) + " is being used. Please, replace it with a real implementation.");
        }
        
        public IPopup CreatePopup(string popupName, string title, string[] options, Action<string> onClosed)
        {
            return null;
        }
    }
}