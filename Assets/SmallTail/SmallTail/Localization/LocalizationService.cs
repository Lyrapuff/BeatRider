using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace SmallTail.Localization
{
    public static class LocalizationService
    {
        public static Action OnKeyChanged { get; set; }
        public static string CurrentKey { get; private set; } = "English";
        
        public static Locale Locale
        {
            get
            {
                if (_locale == null)
                {
                    Load();
                }

                return _locale;
            }
        }

        private static Locale _locale;

        public static void Load()
        {
#if UNITY_EDITOR
            
            bool exists = AssetDatabase.IsValidFolder("Assets/Resources/Localization");

            if (!exists)
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Localization");
            }

#endif
            
            TextAsset localeAsset = Resources.Load<TextAsset>("Localization/Locales");

#if UNITY_EDITOR
            
            if (localeAsset == null)
            {
                localeAsset = new TextAsset("");
                AssetDatabase.CreateAsset(localeAsset, "Assets/Resources/Localization/Locales.bytes");

                _locale = new Locale();
                
                return;
            }
            
#endif
            
            byte[] bytes = localeAsset.bytes;

            MemoryStream stream = new MemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            BinaryFormatter binary = new BinaryFormatter();

            Locale locale = binary.Deserialize(stream) as Locale;

            if (locale == null)
            {
                return;
            }

            _locale = locale;

            stream.Close();

            if (PlayerPrefs.HasKey("language_key"))
            {
                CurrentKey = PlayerPrefs.GetString("language_key");
            }
        }

        public static string GetValue(string key)
        {
            int index = Locale.Keys.IndexOf(key);
            return Locale.Languages.FirstOrDefault(x => x.Key == CurrentKey)?.Words[index];
        }

        public static void SetKey(string key)
        {
            CurrentKey = key;
            PlayerPrefs.SetString("language_key", key);
            OnKeyChanged?.Invoke();
        }
    }
}