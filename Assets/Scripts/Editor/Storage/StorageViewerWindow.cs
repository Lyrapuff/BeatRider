using System.Linq;
using General.Storage;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Storage
{
    public class StorageViewerWindow : EditorWindow
    {
        private General.Storage.Storage _storage;
        private SerializedObject _serializedObject;
        private ReorderableList _list;
        
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
                    , false, true, false, true);

                _list.drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, "Storage");
                };

                _list.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    SerializedProperty property = _list.serializedProperty.GetArrayElementAtIndex(index);

                    EditorGUI.PropertyField(rect, property, true);
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

            EditorGUILayout.LabelField("List");

            _serializedObject.Update();
            _list.DoList(_listRect);
            _serializedObject.ApplyModifiedProperties();
        }
    }
}