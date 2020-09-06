using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmallTail.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedUIText : MonoBehaviour
    {
        public string Key;
        public Dictionary<string, string> DynamicValues = new Dictionary<string, string>();

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            LocalizationService.OnKeyChanged += SetText;
            SetText();
        }

        private string GetText()
        {
            string text = LocalizationService.GetValue(Key);

            foreach (Match match in Regex.Matches(text, "%(.+)%"))
            {
                string key = match.Groups[1].Value;

                if (!DynamicValues.ContainsKey(key))
                {
                    continue;
                }
                
                string wordKey = DynamicValues[key];

                text = text.Replace($"%{key}%", LocalizationService.GetValue(wordKey));
            }
            
            return text;
        }

        public void SetText()
        {
            _text.text = GetText();
        }
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (_text == null)
            {
                _text = GetComponent<TMP_Text>();
            }
            
            _text.text = Key;
        }
        
#endif
    }
}