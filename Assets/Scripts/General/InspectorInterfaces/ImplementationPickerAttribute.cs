using System;
using UnityEngine;

namespace General.InspectorInterfaces
{
    public class ImplementationPickerAttribute : PropertyAttribute
    {
        public Type Type;

        public ImplementationPickerAttribute(Type type)
        {
            Type = type;
        }
    }
}