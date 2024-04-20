using System;
using Better.Attributes.EditorAddons.Drawers.Utility;
using Better.Commons.EditorAddons.Drawers.Caching;
using Better.Commons.EditorAddons.Enums;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.EditorAddons.Utility;
using Better.Commons.Runtime.Extensions;
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
            var type = _fieldInfo.FieldType;
            if (type.IsArrayOrList())
            {
                type = type.GetCollectionElementType();
            }

            var propertyHeight = EditorGUI.GetPropertyHeight(_property, true);
            if (!_setupStrategy.CheckSupported())
            {
                var message = ExtendedGUIUtility.NotSupportedMessage(_property.name, type, _attribute.GetType());
                propertyHeight += ExtendedGUIUtility.GetHelpBoxHeight(EditorGUIUtility.currentViewWidth, message, IconType.ErrorMessage);
            }
            var full = HeightCacheValue.GetFull(propertyHeight);
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