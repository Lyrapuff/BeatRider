using System;

namespace UI.Popups
{
    public interface IPopupFactory
    {
        IPopup CreatePopup(string popupName, string title, string[] options, Action<string> onClosed);
    }
}