using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using SmallTail.Localization;
using UnityEditor;
using UnityEngine;

namespace SmallTail.Editor.Localization
{
    public class LocalizationEditorWindow : EditorWindow
    {
        private Locale _locale;
        
        private string[] _tabs;
        private int _tab;
        private bool _hasChanges;
        
        private string _newLanguage;
        private string _newLocale;

        private Vector2 _scrollPosition;

        private bool _configuring;
        private string _search = "";
        private Filter _filter = Filter.All;

        private int _selectedIndex;
        
        [MenuItem("Window/SmallTail/Localization")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow(typeof(LocalizationEditorWindow), false, "Localization");
            window.titleContent = new GUIContent
            {
                text = "Localization"
            };
        }

        private void Awake()
        {
            LocalizationService.Load();
            _locale = LocalizationService.Locale;
        }

        private void OnGUI()
        {
            _tabs = _locale.Languages.Select(x => x.Key).Append("New").ToArray();

            _tab = GUILayout.Toolbar(_tab, _tabs);

            if (_tab < _locale.Languages.Count)
            {
                ShowLocale();
            }
            else
            {
                ShowNew();
            }

            GUILayout.FlexibleSpace();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            string focusedName = GUI.GetNameOfFocusedControl();
            string[] split = focusedName.Split(':');
            
            if ((split[0] == "Key" || split[0] == "Value") && GUILayout.Button("Delete " + split[3], GUILayout.Width(300)))
            {
                DeleteKey(int.Parse(split[2]));
            }
            
            GUI.enabled = _hasChanges;
            
            if (_hasChanges & GUILayout.Button("Apply", GUILayout.Width(100)))
            {
                Apply();
            }
            
            GUI.enabled = true;

            GUILayout.EndHorizontal();
        }

        private void DeleteKey(int index)
        {
            _locale.Keys.RemoveAt(index);
            
            for (var i = 0; i < _locale.Languages.Count; i++)
            {
                _locale.Languages[i].Words.RemoveAt(index);
            }

            if (_locale.Keys.Count > 0)
            {

                int newIndex = index > 0 ? index - 1 : 0;

                GUI.FocusControl("Key:" + _tabs[_tab] + ":" + newIndex + ":" + _locale.Keys[newIndex]);
            }

            _hasChanges = true;
        }
        
        private void Apply()
        {
            try
            {
                FileStream stream = new FileStream(
                    Application.dataPath + "/Resources/Localization/Locales.bytes",
                    FileMode.OpenOrCreate, FileAccess.ReadWrite);

                BinaryFormatter binary = new BinaryFormatter();
                binary.Serialize(stream, _locale);

                stream.Close();
                
                _hasChanges = false;
            }
            catch (Exception e)
            {
                Debug.LogError($"Applying error: " + e);
            }
        }
        
        private void ShowLocale()
        {
            string languageName = _tabs[_tab];

            _configuring = EditorGUILayout.Foldout(_configuring, "Filters");

            if (_locale.Keys.Count > 0)
            {
                if (_configuring)
                {
                    GUILayout.BeginHorizontal();

                    EditorGUIUtility.labelWidth = 100f;
                    _search = EditorGUILayout.TextField("Search key",_search);

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                    EditorGUIUtility.labelWidth = 100f;
                    _filter = (Filter) EditorGUILayout.EnumPopup("Filter", _filter);
                    
                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal();

                GUILayout.Label("Key");
                GUILayout.Label("Value");

                GUILayout.EndHorizontal();
            }

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            GUILayout.ExpandWidth(false);

            for (var i = 0; i < _locale.Keys.Count; i++)
            {
                string key = _locale.Keys[i];
                
                if (!key.ToLower().Contains(_search.ToLower()))
                {
                    continue;
                }

                Language language = _locale.Languages.FirstOrDefault(x => x.Key == languageName);

                if (language == null)
                {
                    continue;
                }

                if (_filter == Filter.Localized && language.Words[i] == "New word")
                {
                    continue;
                }

                if (_filter == Filter.NotLocalized && language.Words[i] != "New word")
                {
                    continue;
                }

                GUILayout.BeginHorizontal();

                GUI.SetNextControlName("Key:" + languageName + ":" + i + ":" + key);
                string newKey = EditorGUILayout.TextField(_locale.Keys[i]);

                GUI.SetNextControlName("Value:" + languageName + ":" + i + ":" + key);
                string newValue = EditorGUILayout.TextField(language.Words[i]);

                if (newKey != _locale.Keys[i] || newValue != language.Words[i])
                {
                    _hasChanges = true;
                }

                _locale.Keys[i] = newKey;
                language.Words[i] = newValue;

                GUILayout.EndHorizontal();
            }

            GUILayout.ExpandWidth(true);
            
            GUILayout.EndScrollView();

            if (GUILayout.Button("New key"))
            {
                _locale.Keys.Add("New key");

                for (var i = 0; i < _locale.Languages.Count; i++)
                {
                    Language language = _locale.Languages[i];
                    language.Words.Add("New word");
                    _locale.Languages[i] = language;
                }

                _hasChanges = true;
            }
        }

        private void ShowNew()
        {
            GUILayout.Label("Language");
            GUI.SetNextControlName("New language");
            _newLanguage = GUILayout.TextField(_newLanguage);

            bool empty = string.IsNullOrWhiteSpace(_newLanguage);

            GUI.enabled = !empty;
            
            if (!empty & GUILayout.Button("Add"))
            {
                Language language = new Language();
                language.Key = _newLanguage;
                
                List<string> words = new List<string>();

                for (int i = 0; i < _locale.Keys.Count; i++)
                {
                    words.Add("New word");
                }

                language.Words = words;
                
                _locale.Languages.Add(language);

                _newLanguage = "";
                _hasChanges = true;
            }

            GUI.enabled = true;
        }
    }

    public enum Filter
    {
        [Description("All")]
        All,
        [Description("Localized")]
        Localized,
        [Description("Not localized")]
        NotLocalized
    }
}
