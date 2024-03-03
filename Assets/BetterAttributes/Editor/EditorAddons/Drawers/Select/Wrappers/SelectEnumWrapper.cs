using System;
using System.Reflection;
using Better.Attributes.EditorAddons.Extensions;
using Better.EditorTools.EditorAddons.Drawers.Base;
using Better.Extensions.EditorAddons;
using Better.Extensions.Runtime;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectEnumWrapper : BaseSelectWrapper
    {
        private bool _isFlag;
        private int _everythingValue;

        public override bool SkipFieldDraw()
        {
            return true;
        }

        public override HeightCacheValue GetHeight()
        {
            var heightCacheValue = HeightCacheValue.GetFull(EditorGUI.GetPropertyHeight(_property, false));
            return heightCacheValue;
        }

        public override void SetProperty(SerializedProperty property, FieldInfo fieldInfo)
        {
            base.SetProperty(property, fieldInfo);
            var enumType = fieldInfo.FieldType;
            if (enumType.IsArrayOrList())
            {
                enumType = enumType.GetCollectionElementType();
            }
            _everythingValue = EnumUtility.EverythingFlag(enumType).ToFlagInt();
            _isFlag = fieldInfo.FieldType.GetCustomAttribute<FlagsAttribute>() != null;
        }

        public override void Update(object objValue)
        {
            if (!_property.Verify()) return;
            var value = (int)objValue;
            var currentValue = _property.intValue;
            currentValue = EnumCalculator.CalculateCurrentValue(currentValue, _isFlag, value, _everythingValue);

            _property.intValue = currentValue;
        }
        
        public override object GetCurrentValue()
        {
            return _property.intValue;
        }
    }
}