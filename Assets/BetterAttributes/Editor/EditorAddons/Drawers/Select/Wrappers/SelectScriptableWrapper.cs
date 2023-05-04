using System;
using Better.Attributes.Runtime.Select;
using Better.Extensions.Runtime;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectScriptableWrapper : BaseSelectWrapper
    {
        public override bool SkipFieldDraw()
        {
            return false;
        }

        public override float GetHeight()
        {
            return EditorGUI.GetPropertyHeight(_property, true);
        }

        public override void Update(object value)
        {
            if (_property == null) return;
            var typeValue = (Type)value;
            _property.managedReferenceValue = typeValue == null ? null : Activator.CreateInstance(typeof(SerializedType), typeValue);
        }

        public override object GetCurrentValue()
        {
            return _property.exposedReferenceValue;
        }

        public bool ResolveState(object currentValue, object iteratedValue)
        {
            if (iteratedValue == null && currentValue == null) return true;
            return iteratedValue is string type && currentValue is string currentType && currentType == type;
        }

        public bool Validate(object item)
        {
            return true;
        }

        public GUIContent[] ResolveGroupedName(object value, DisplayGrouping grouping)
        {
            throw new NotImplementedException();
        }

        public GUIContent ResolveName(object value, DisplayName displayName)
        {
            throw new NotImplementedException();
        }
    }
}