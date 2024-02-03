using System;
using System.Reflection;
using Better.EditorTools;
using Better.EditorTools.Drawers.Base;
using Better.Extensions.Runtime;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectSerializedTypeWrapper : BaseSelectWrapper
    {
        public override bool SkipFieldDraw()
        {
            return true;
        }

        public override HeightCache GetHeight()
        {
            var full = HeightCache.GetFull(EditorGUI.GetPropertyHeight(_property, false));
            return full;
        }

        public override void Update(object value)
        {
            if (!_property.Verify()) return;
            var typeValue = (Type)value;
            if (_property.propertyType == SerializedPropertyType.Generic)
            {
                var buffer = typeValue != null ? new SerializedType(typeValue) : new SerializedType();
                _property.SetValue(buffer);
            }
            else if (_property.propertyType == SerializedPropertyType.ManagedReference)
            {
                _property.managedReferenceValue = typeValue == null ? null : Activator.CreateInstance(typeof(SerializedType), typeValue);
            }
        }

        public override object GetCurrentValue()
        {
            var objectOfProperty = _property.GetValue();
            var type = objectOfProperty.GetType();
            if (type == typeof(SerializedType))
            {
                type = (objectOfProperty as SerializedType)?.Type;
            }

            return type;
        }
    }
}