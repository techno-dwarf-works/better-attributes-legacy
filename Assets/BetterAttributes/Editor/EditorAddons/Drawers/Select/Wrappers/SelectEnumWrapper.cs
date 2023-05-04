using System;
using System.Reflection;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectEnumWrapper : BaseSelectWrapper
    {
        private bool _isFlag;

        public override bool SkipFieldDraw()
        {
            return true;
        }

        public override float GetHeight()
        {
            return EditorGUI.GetPropertyHeight(_property, false);
        }

        public override void SetProperty(SerializedProperty property, FieldInfo fieldInfo)
        {
            base.SetProperty(property, fieldInfo);
            _isFlag = fieldInfo.FieldType.GetCustomAttribute<FlagsAttribute>() != null;
        }

        public override void Update(object objValue)
        {
            var value = (int)objValue;
            var currentValue = _property.intValue;
            if (_isFlag)
            {
                if (currentValue == 0)
                {
                    currentValue = value;
                }
                else
                {
                    currentValue ^= value;
                }
            }
            else
            {
                currentValue = value;
            }

            _property.intValue = currentValue;
        }

        public override object GetCurrentValue()
        {
            return _property.intValue;
        }
    }
}