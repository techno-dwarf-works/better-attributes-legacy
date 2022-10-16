using System;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Select.Wrappers;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.WrapperCollections
{
    public class SelectTypeWrapperCollection : WrapperCollection<SelectTypeWrapper>
    {
        public void Update(SerializedProperty property, Type type)
        {
            if (TryGetValue(property, out var value))
            {
                value.Wrapper.Update(type);
            }
        }
    }
    
    public class SelectEnumWrapperCollection : WrapperCollection<SelectEnumWrapper>
    {
        public void Update(SerializedProperty property, Enum type)
        {
            if (TryGetValue(property, out var value))
            {
                value.Wrapper.Update(type);
            }
        }
    }
}