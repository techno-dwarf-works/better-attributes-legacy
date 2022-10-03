using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BetterAttributes.Runtime.Attributes.Select;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    [CustomPropertyDrawer(typeof(SelectEnumAttribute))]
    public class SelectEnumDrawer : SelectDrawerBase<SelectEnumAttribute>
    {
        private List<object> _enumValues;
        private bool _isFlag;
        private Enum _enumValue;
        private Type _enumType;
        private const string NoneValue = "None";
        private const string EverythingValue = "Everything";

        private protected override void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            var preparePropertyRect = PreparePropertyRect(position);
            EditorGUI.PrefixLabel(preparePropertyRect, label);
        }

        private protected override bool CheckSupported(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.Enum;
        }

        private protected override GUIContent GenerateHeader()
        {
            return new GUIContent("Options");
        }

        private protected override string GetButtonName(object currentValue)
        {
            if (_isFlag)
            {
                if (currentValue.ToString().Equals(NoneValue))
                {
                    return NoneValue;
                }

                if (currentValue.ToString().Equals(EverythingValue))
                {
                    return EverythingValue;
                }
            }

            if (Enum.TryParse(_enumType, (string)currentValue, out var eEnum))
            {
                return eEnum.ToString();
            }

            return NotSupported;
        }

        private protected override void Setup(SerializedProperty property, SelectEnumAttribute currentAttribute)
        {
            _isFlag = fieldInfo.FieldType.GetCustomAttribute<FlagsAttribute>() != null;
            var buffer = property.enumNames.ToList();
            if (_isFlag)
            {
                if (!buffer.Contains(NoneValue))
                {
                    buffer.Insert(0, NoneValue);
                }

                if (!buffer.Contains(EverythingValue))
                {
                    buffer.Insert(buffer.Count, EverythingValue);
                }
            }

            _enumValues = buffer.Cast<object>().ToList();
            _enumType = fieldInfo.FieldType;
        }

        private protected override void UpdateValue(SerializedProperty property)
        {
            if (!_isFlag)
            {
                property.enumValueIndex = _enumValues.IndexOf(_enumValue.ToString());
            }
            else
            {
                var currentEnum = (Enum)Enum.ToObject(_enumType, property.enumValueFlag);
                if (_enumValue.IsFlagNone())
                {
                    currentEnum = _enumValue;
                }
                else
                {
                    if (currentEnum.HasFlag(_enumValue))
                    {
                        currentEnum = currentEnum.Remove(_enumValue);
                    }
                    else
                    {
                        currentEnum = currentEnum.Add(_enumValue);
                    }
                }

                property.enumValueFlag = currentEnum.ToFlagInt();
            }
        }

        private protected override object GetCurrentValue(SerializedProperty property)
        {
            if (!_isFlag) return _enumValues[property.enumValueIndex].ToString();
            
            if (property.enumValueFlag == EnumExtensions.FlagNone)
            {
                return NoneValue;
            }

            if (property.enumValueFlag == _enumType.AllFlags().ToFlagInt())
            {
                return EverythingValue;
            }
            return Enum.ToObject(_enumType, property.enumValueFlag).ToString();

        }

        private protected override List<object> GetSelectCollection()
        {
            return _enumValues;
        }

        private protected override string[] ResolveGroupedName(object value, DisplayGrouping grouping)
        {
            return new string[] { NotSupported };
        }

        private protected override string ResolveName(object value, DisplayName displayName)
        {
            if (value is string eEnum)
            {
                switch (displayName)
                {
                    case DisplayName.Short:
                        return $"{eEnum}";
                    case DisplayName.Full:
                        return $"{_enumType.Name}.{eEnum}";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(displayName), displayName, null);
                }
            }

            return NotSupported;
        }

        private protected override bool ResolveState(object currentValue, object iteratedValue)
        {
            if (currentValue is string stringCurrent && iteratedValue is string stringIterated)
            {
                return stringCurrent.Contains(stringIterated);
            }

            return false;
        }

        private protected override void OnSelectItem(object obj)
        {
            if (!(obj is string name)) return;
            if (_isFlag)
            {
                var intValue = 0;
                switch (name)
                {
                    case NoneValue:
                        intValue = EnumExtensions.FlagNone;
                        break;
                    case EverythingValue:
                        intValue = _enumType.AllFlags().ToFlagInt();
                        break;
                }

                _enumValue = (Enum)Enum.ToObject(_enumType, intValue);
                SetNeedUpdate();
            }

            if (Enum.TryParse(_enumType, name, out var objectEnum) && objectEnum is Enum eEnum)
            {
                _enumValue = eEnum;
                SetNeedUpdate();
            }
        }
    }
}