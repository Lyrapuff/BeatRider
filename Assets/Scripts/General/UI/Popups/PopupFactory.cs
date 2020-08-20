using System;
using General.Behaviours;
using UnityEngine;

namespace General.UI.Popups
{
    public class PopupFactory : ExtendedBehaviour, IPopupFactory
    {
        public IPopup CreatePopup(string popupName, string title, string[] options, Action<string> onClosed)
        {
            Popup popupPrefab = Resources.Load<Popup>("Prefabs/UI/Popups/" + popupName);

            if (popupPrefab != null)
            {
                IPopup popupInstance = Instantiate(popupPrefab, transform);
                popupInstance.Title = title;
                popupInstance.Options = options;
                popupInstance.OnClosed = onClosed;
                
                return popupInstance;
            }

            return null;
        }
    }
}