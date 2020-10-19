using General.Storage;
using UnityEditor;
using UnityEngine;

namespace Storage
{
    [CustomPropertyDrawer(typeof(StoredObject))]
    public class StoredItemDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("Key"));
        }
    }
}