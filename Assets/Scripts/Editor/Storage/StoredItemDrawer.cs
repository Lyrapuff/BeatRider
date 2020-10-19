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
            Color color = property.FindPropertyRelative("Persistant").boolValue ? Color.blue : Color.gray;
            
            EditorGUI.DrawRect(new Rect(position.x, position.y, 10f, position.height), color);
            
            EditorGUI.LabelField(new Rect(20f, position.y, position.width, position.height), property.FindPropertyRelative("Key").stringValue);
        }
    }
}