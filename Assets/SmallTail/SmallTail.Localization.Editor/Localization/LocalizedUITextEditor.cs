using SmallTail.Localization;
using UnityEditor;
using UnityEngine;

namespace SmallTail.Editor.Localization
{
    [CustomEditor(typeof(LocalizedUIText))]
    public class LocalizedUITextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            LocalizedUIText localizedUiText = target as LocalizedUIText;
            
            if (localizedUiText != null)
            {
                EditorGUILayout.LabelField("Selected key");

                GUI.enabled = false;
                EditorGUILayout.TextField(string.IsNullOrWhiteSpace(localizedUiText.Key) ? "None" : localizedUiText.Key);
                GUI.enabled = true;
                
                if (GUILayout.Button("Open key selection window"))
                {
                    KeySelectionWindow window = EditorWindow.GetWindow(typeof(KeySelectionWindow), true) as KeySelectionWindow;

                    if (window != null)
                    {
                        window.LocalizedUiText = localizedUiText;
                        window._serializedObject = serializedObject;
                    }
                }
            }
        }
    }
}