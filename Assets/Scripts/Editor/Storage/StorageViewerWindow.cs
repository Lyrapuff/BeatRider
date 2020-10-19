using System.Collections.Generic;
using General.Storage;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Storage
{
    public class StorageViewerWindow : EditorWindow
    {
        private General.Storage.Storage _storage;
        private SerializedObject _serializedObject;
        private ReorderableList _list;
        private int _selectedIndex;
        
        private static readonly Rect _listRect = new Rect(Vector2.zero, Vector2.one * 300f);
        
        [MenuItem("SmallTail/Storage/Viewer")]
        public static void ShowWindow()
        {
            StorageViewerWindow window = GetWindow(typeof(StorageViewerWindow), false, "Localization") as StorageViewerWindow;
            
            window.titleContent = new GUIContent
            {
                text = "Storage viewer"
            };
        }

        private void OnEnable()
        {
            _storage = FindObjectOfType<General.Storage.Storage>();

            if (_storage != null)
            {
                _serializedObject = new SerializedObject(_storage);

                _list = new ReorderableList(_serializedObject, _serializedObject.FindProperty("storage")
                    , false, false, false, true);

                _list.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    SerializedProperty property = _list.serializedProperty.GetArrayElementAtIndex(index);

                    EditorGUI.PropertyField(rect, property, true);
                };

                _list.drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, "Values");
                };
                
                _list.onSelectCallback = list =>
                {
                    _selectedIndex = list.index;
                };
            }
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            if (_storage == null)
            {
                EditorGUILayout.LabelField("No storage was found");
                return;
            }

            GUILayout.BeginHorizontal();
            
            GUILayout.BeginVertical();
            
            _serializedObject.Update();
            _list.DoLayoutList();
            _serializedObject.ApplyModifiedProperties();

            GUILayout.EndVertical ();
            
            if (_list.serializedProperty.arraySize < 1)
            {
                EditorGUILayout.LabelField("No items");
                return;
            }
            
            GUILayout.BeginVertical("Box");
            
            SerializedProperty property = _list.serializedProperty.GetArrayElementAtIndex(_selectedIndex);
            
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Key"));

            object obj = property.serializedObject.targetObject;
            
            
            List<StoredObject> list = obj.GetType().GetField("storage").GetValue(obj)
                as List<StoredObject>;

            if (list.Count < 1)
            {
                EditorGUILayout.LabelField("No items");
                return;
            }
            
            StoredObject stored = list[_selectedIndex];

            EditorGUILayout.LabelField(stored.Data + "");
            
            GUILayout.EndVertical();
            
            GUILayout.EndHorizontal();
        }
    }
}