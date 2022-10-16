using System;
using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.Utilities;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public class WrapperCollectionValue<T> where T : UtilityWrapper
    {
        public WrapperCollectionValue(T wrapper, Type type)
        {
            Wrapper = wrapper;
            Type = type;
        }

        public T Wrapper { get; }
        public Type Type { get; }
    }
    
    public class WrapperCollection<T> : Dictionary<SerializedProperty, WrapperCollectionValue<T>> where T : UtilityWrapper
    {
        public WrapperCollection() : base(SerializedPropertyComparer.Instance)
        {
        }

        public void Deconstruct()
        {
            foreach (var gizmo in Values)
            {
                gizmo.Wrapper.Deconstruct();
            }
        }
    }
}