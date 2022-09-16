using System;
using System.Collections.Generic;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public class WrapperCollection<T> : Dictionary<SerializedProperty, (T, Type)> where T : UtilityWrapper
    {
        private class SerializedPropertyComparer : IEqualityComparer<SerializedProperty>
        {
            public bool Equals(SerializedProperty x, SerializedProperty y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.propertyPath == y.propertyPath;
            }

            public int GetHashCode(SerializedProperty obj)
            {
                return (obj.propertyPath != null ? obj.propertyPath.GetHashCode() : 0);
            }
        }

        public WrapperCollection() : base(new SerializedPropertyComparer())
        {
        }
        
        

        public void Deconstruct()
        {
            foreach (var gizmo in Values)
            {
                gizmo.Item1.Deconstruct();
            }
        }
    }
}