using System;
using System.Reflection;
using Better.EditorTools;
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

        public override float GetHeight()
        {
            return EditorGUI.GetPropertyHeight(_property, false);
        }

        public override void Update(object value)
        {
            if (!_property.Verify()) return;
            var typeValue = (Type)value;
            if(_property.propertyType == SerializedPropertyType.Generic)
            {
                var buffer = new SerializedType(typeValue);
                _property.SetValue(buffer);
            }
            else if(_property.propertyType == SerializedPropertyType.ManagedReference)
            {
                _property.managedReferenceValue = typeValue == null ? null : Activator.CreateInstance(typeof(SerializedType), typeValue);
            }
        }

        public override object GetCurrentValue()
        {
            Type type = null;
            if (_property.propertyType == SerializedPropertyType.Generic)
            {
                var objectOfProperty = _property.GetValue();
                type = objectOfProperty.GetType();
                if (type == typeof(SerializedType))
                {
                    type = (objectOfProperty as SerializedType)?.Type;
                }
            }
            else if(_property.propertyType == SerializedPropertyType.ManagedReference)
            {
                type = _property.GetManagedType();
                if (type == typeof(SerializedType))
                {
                    type = (_property.managedReferenceValue as SerializedType)?.Type;
                }
            }

            return type;
        }

        
    }
}