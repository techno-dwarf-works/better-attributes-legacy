using System;
using Better.EditorTools.EditorAddons.Drawers.Base;
using Better.Extensions.EditorAddons;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectTypeWrapper : BaseSelectWrapper
    {
        public override bool SkipFieldDraw()
        {
            return false;
        }

        public override HeightCacheValue GetHeight()
        {
            var full = HeightCacheValue.GetFull(EditorGUI.GetPropertyHeight(_property, true));
            return full;
        }

        public override void Update(object value)
        {
            if (!_property.Verify()) return;
            var typeValue = (Type)value;
            _property.managedReferenceValue = typeValue == null ? null : Activator.CreateInstance(typeValue);
        }

        public override object GetCurrentValue()
        {
            return _property.GetManagedType();
        }
    }
}