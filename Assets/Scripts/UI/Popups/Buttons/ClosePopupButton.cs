using UnityEngine;

namespace UI.Popups.Buttons
{
    public class ClosePopupButton : MonoBehaviour
    {
        public void CloseSilent(Popup popup)
        {
            popup.CloseSilent();
        }
    }
}