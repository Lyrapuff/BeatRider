using System.Collections.Generic;
using SmallTail.Localization;
using UnityEditor;
using UnityEngine;

namespace SmallTail.Editor.Localization
{
    public class KeySelectionWindow : EditorWindow
    {
        public LocalizedUIText LocalizedUiText;
        public SerializedObject _serializedObject;
        
        private Locale _locale;
        
        private string _keyName;
        
        private void Awake()
        {
            _locale = LocalizationService.Locale;
        }
        
        private void OnGUI()
        {
            _keyName = EditorGUILayout.TextField(_keyName);

            for (var i = 0; i < _locale.Keys.Count; i++)
            {
                string key = _locale.Keys[i];
                
                if (!string.IsNullOrWhiteSpace(_keyName) && !key.ToLower().Contains(_keyName.ToLower()))
                {
                    continue;
                }
                
                if (GUILayout.Button(key))
                {
                    SerializedProperty property = _serializedObject.FindProperty("Key");
                    property.stringValue = key;
                    _serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}