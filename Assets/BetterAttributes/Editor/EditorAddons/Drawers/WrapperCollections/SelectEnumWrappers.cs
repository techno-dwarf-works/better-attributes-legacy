using System;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.EditorTools.Drawers.Base;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.WrapperCollections
{
    public class SelectEnumWrappers : WrapperCollection<SelectEnumWrapper>
    {
        /// <summary>
        /// Update call for stored wrappers
        /// </summary>
        /// <param name="property"></param>
        /// <param name="type"></param>
        public void Update(SerializedProperty property, Enum type)
        {
            if (TryGetValue(property, out var value))
            {
                value.Wrapper.Update(type);
            }
        }
    }
}