using System;

namespace General.InspectorInterfaces
{
    [Serializable]
    public class InterfaceContainer<T>
    {
        public Object ReferenceValue;
        public T Value;

        public InterfaceContainer(T value)
        {
            Value = value;
        }
    }
}