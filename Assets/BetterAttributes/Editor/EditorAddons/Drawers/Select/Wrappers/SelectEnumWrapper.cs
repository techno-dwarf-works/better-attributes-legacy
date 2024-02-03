using System;
using System.Reflection;
using Better.Attributes.EditorAddons.Extensions;
using Better.EditorTools;
using Better.EditorTools.Drawers.Base;
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

        public override HeightCache GetHeight()
        {
            var heightCache = HeightCache.GetFull(EditorGUI.GetPropertyHeight(_property, false));
            return heightCache;
        }

        public override void SetProperty(SerializedProperty property, FieldInfo fieldInfo)
        {
            base.SetProperty(property, fieldInfo);
            var enumType = fieldInfo.GetFieldOrElementType();
            _everythingValue = enumType.EverythingFlag().ToFlagInt();
            _isFlag = fieldInfo.FieldType.GetCustomAttribute<FlagsAttribute>() != null;
        }

        public override void Update(object objValue)
        {
            if (!_property.Verify()) return;
            var value = (int)objValue;
            var currentValue = _property.intValue;
            currentValue = EnumSetterExtension.CalculateCurrentValue(currentValue, _isFlag, value, _everythingValue);

            _property.intValue = currentValue;
        }
        
        public override object GetCurrentValue()
        {
            return _property.intValue;
        }
    }
}