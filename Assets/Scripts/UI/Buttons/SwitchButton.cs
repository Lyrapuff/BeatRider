using System;
using General.Behaviours;
using General.Storage;
using SmallTail.Localization;
using UnityEngine;

namespace UI.Buttons
{
    public class SwitchButton : ExtendedBehaviour
    {
        [SerializeField] private SwitchType _switchType;
        [SerializeField] private LocalizedUIText _localizedUiText;

        private enum SwitchType
        {
            Music,
            Vibration
        }
        
        private void Start()
        {
            SetText();
        }

        public void Switch()
        {
            GameData gameData = GameDataStorage.Instance.GetData();

            switch (_switchType)
            {
                case SwitchType.Music:
                    gameData.MusicEnabled = !gameData.MusicEnabled;
                    break;
                case SwitchType.Vibration:
                    gameData.VibrationEnabled = !gameData.VibrationEnabled;
                    break;
            }
            
            GameDataStorage.Instance.SetData(gameData);
            SetText();
        }

        private void SetText()
        {
            GameData gameData = GameDataStorage.Instance.GetData();

            bool newValue = false;
            
            switch (_switchType)
            {
                case SwitchType.Music:
                    newValue = gameData.MusicEnabled;
                    break;
                case SwitchType.Vibration:
                    newValue = gameData.VibrationEnabled;
                    break;
            }
            
            if (_localizedUiText != null)
            {
                if (!_localizedUiText.DynamicValues.ContainsKey("status"))
                {
                    _localizedUiText.DynamicValues.Add("status", "status_" + newValue.ToString().ToLower());
                    _localizedUiText.SetText();
                    return;
                }
                
                _localizedUiText.DynamicValues["status"] = "status_" + newValue.ToString().ToLower();
                _localizedUiText.SetText();
            }
        }
    }
}