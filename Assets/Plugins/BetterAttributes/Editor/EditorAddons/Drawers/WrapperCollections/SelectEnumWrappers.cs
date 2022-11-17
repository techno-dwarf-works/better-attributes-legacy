using System;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Select.Wrappers;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.WrapperCollections
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