using System;
using BetterAttributes.Runtime.Attributes.Select;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : SelectTypeDrawer
    {
        private protected override object GetCurrentValue(SerializedProperty property)
        {
            return property.managedReferenceValue;
        }

        private protected override void UpdateValue(SerializedProperty property)
        {
            property.managedReferenceValue = _type == null ? null : Activator.CreateInstance(_type);
        }
    }
}