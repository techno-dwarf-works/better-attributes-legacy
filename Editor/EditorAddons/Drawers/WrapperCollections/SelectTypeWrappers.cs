using System;
using Better.Attributes.EditorAddons.Drawers.Base;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.WrapperCollections
{
    public class SelectTypeWrappers : WrapperCollection<SelectTypeWrapper>
    {
        public void Update(SerializedProperty property, Type type)
        {
            if (TryGetValue(property, out var value))
            {
                value.Wrapper.Update(type);
            }
        }
    }
}