using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Select.Wrappers;
using BetterAttributes.EditorAddons.Drawers.WrapperCollections;
using BetterAttributes.Runtime.Attributes.Select;
using BetterExtensions.Runtime.Extension;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    [CustomPropertyDrawer(typeof(SelectEnumAttribute))]
    public class SelectEnumDrawer : SelectDrawerBase<SelectEnumAttribute, SelectEnumWrapper>
    {
        private List<object> _enumValues;
        private bool _isFlag;
        private SelectedItem<Enum> _enumValue;
        private Type _enumType;

        private PredefinedValues _none = new PredefinedValues("None", 0);
        private PredefinedValues _everythingValue = new PredefinedValues("Everything", -1);

        private SelectEnumWrappers Collection => _wrappers as SelectEnumWrappers;

        private struct PredefinedValues
        {
            public PredefinedValues(string name, int value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public int Value { get; }
        }

        private protected override WrapperCollection<SelectEnumWrapper> GenerateCollection()
        {
            return new SelectEnumWrappers();
        }

        private protected override void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            var preparePropertyRect = PreparePropertyRect(position);
            EditorGUI.LabelField(preparePropertyRect, label, EditorStyles.label);
        }

        private protected override bool CheckSupported(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.Enum;
        }

        private protected override GUIContent GenerateHeader()
        {
            return new GUIContent("Options");
        }

        private protected override void Setup(SerializedProperty property, SelectEnumAttribute currentAttribute)
        {
            _enumType = fieldInfo.FieldType;
            _isFlag = _enumType.GetCustomAttribute<FlagsAttribute>() != null;
            Collection[property].Wrapper.SetIsFlag(_isFlag);
            var ints = _enumType.GetAllValues();
            if (_isFlag)
            {
                _none = new PredefinedValues("None", EnumExtensions.FlagNone);
                if (!ints.Contains(_none.Value))
                {
                    ints.Insert(0, _none.Value);
                }

                var everything = _enumType.EverythingFlag().ToFlagInt();
                _everythingValue = new PredefinedValues("Everything", everything);
                if (!ints.Contains(_everythingValue.Value))
                {
                    ints.Insert(ints.Count, _everythingValue.Value);
                }
            }

            _enumValues = ints.Cast<object>().ToList();
        }

        private protected override string GetButtonName(object currentValue)
        {
            var intValue = (int)currentValue;
            if (_isFlag)
            {
                if (intValue.Equals(_none.Value))
                {
                    return _none.Name;
                }

                if (intValue.Equals(_everythingValue.Value))
                {
                    return _everythingValue.Name;
                }
            }

            var value = Enum.ToObject(_enumType, intValue);
            return value.ToString();
        }

        private protected override void UpdateValue(SerializedProperty property)
        {
            Collection.Update(_enumValue.Property, _enumValue.Data);
        }

        private protected override object GetCurrentValue(SerializedProperty property)
        {
            if (!_isFlag) return _enumValues[property.enumValueIndex];
            int currentEnum;
#if UNITY_2021_1_OR_NEWER
            currentEnum = property.enumValueFlag;
#else
            currentEnum = property.intValue;
#endif

            if (currentEnum == EnumExtensions.FlagNone)
            {
                return _none.Value;
            }

            if (currentEnum == _everythingValue.Value)
            {
                return _everythingValue.Value;
            }

            return ((Enum)Enum.ToObject(_enumType, currentEnum)).ToFlagInt();
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
            if (value is int intValue)
            {
                var eEnum = Enum.ToObject(_enumType, intValue);

                if (intValue == _everythingValue.Value)
                {
                    return _everythingValue.Name;
                }

                if (intValue == _none.Value)
                {
                    return _none.Name;
                }

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
            if (currentValue is int intCurrent && iteratedValue is int intIterated)
            {
                if (!_isFlag) return intCurrent == intIterated;

                if (intIterated == _none.Value || intIterated == _everythingValue.Value)
                {
                    return intCurrent == intIterated;
                }

                return (intCurrent & intIterated) != 0;
            }

            return false;
        }

        private protected override void AfterValueUpdated(SerializedProperty property)
        {
            _enumValue = null;
        }

        private protected override void OnSelectItem(object obj)
        {
            if (obj is SelectedItem<object> value && value.Data is int intValue)
            {
                if (_isFlag)
                {
                    if (intValue == _none.Value)
                    {
                        intValue = EnumExtensions.FlagNone;
                    }
                    else if (intValue == _everythingValue.Value)
                    {
                        intValue = _everythingValue.Value;
                    }
                }

                _enumValue = new SelectedItem<Enum>(value.Property, (Enum)Enum.ToObject(_enumType, intValue));
                SetNeedUpdate();
            }
        }
    }
}