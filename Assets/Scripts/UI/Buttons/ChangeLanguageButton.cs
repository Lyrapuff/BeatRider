using General.Behaviours;
using SmallTail.Localization;
using UI.Popups;

namespace UI.Buttons
{
    public class ChangeLanguageButton : ExtendedBehaviour
    {
        private IPopupFactory _popupFactory;

        private void Awake()
        {
            _popupFactory = FindComponentOfInterface<IPopupFactory, NullPopupFactory>();
        }

        public void Open()
        {
            _popupFactory.CreatePopup("LanguagePopup",
                LocalizationService.GetValue("ui_popup_language_title"),
                new[]
                {
                    "Русский",
                    "English"
                }, option =>
                {
                    if (option == "Русский")
                    {
                        LocalizationService.SetKey("Russian");
                    }
                    else
                    {
                        LocalizationService.SetKey("English");
                    }
                });
        }
    }
}