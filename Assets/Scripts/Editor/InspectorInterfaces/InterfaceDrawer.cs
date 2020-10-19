using System;
using System.Linq;
using System.Reflection;
using General.InspectorInterfaces;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InspectorInterfaces
{
    [CustomPropertyDrawer(typeof(InterfaceContainer<>))]
    public class InterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Type type = fieldInfo.FieldType.GetGenericArguments().FirstOrDefault();

            if (type == null)
            {
                return;
            }

            object container = Convert.ChangeType(property.objectReferenceValue, typeof(InterfaceContainer<>));
            
            Object obj = EditorGUI.ObjectField(position, label, null, type, true);

            PropertyInfo info = container.GetType().GetProperty("Value");

            if (info == null)
            {
                return;
            }
            
            info.SetValue(container, Convert.ChangeType(obj, type));

            property.managedReferenceValue = container;
            
            EditorGUI.EndProperty();
        }
    }
}