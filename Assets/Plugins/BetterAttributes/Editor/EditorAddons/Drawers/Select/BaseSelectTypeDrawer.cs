using System;
using System.Collections.Generic;
using System.Linq;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Select.Wrappers;
using BetterAttributes.EditorAddons.Drawers.WrapperCollections;
using BetterAttributes.Runtime.Attributes.Select;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    public abstract class BaseSelectTypeDrawer : SelectDrawerBase<SelectImplementationAttribute, SelectTypeWrapper>
    {
        private protected SelectedItem<Type> _type;
        private List<object> _reflectionTypes;

        private protected SelectTypeWrappers Collection => _wrappers as SelectTypeWrappers;

        private protected override WrapperCollection<SelectTypeWrapper> GenerateCollection()
        {
            return new SelectTypeWrappers();
        }

        private void LazyGetAllInheritedType(Type baseType, Type currentObjectType)
        {
            if (_reflectionTypes != null) return;

            _reflectionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()).Where(p => ArgIsValueType(baseType, currentObjectType, p))
                .Select(x => (object)x).ToList();
            _reflectionTypes.Insert(0, null);
        }

        private bool ArgIsValueType(Type baseType, Type currentObjectType, Type iterateValue)
        {
            return iterateValue != currentObjectType && CheckType(baseType, iterateValue) &&
                   (iterateValue.IsClass || iterateValue.IsValueType) &&
                   !iterateValue.IsAbstract && !iterateValue.IsSubclassOf(typeof(Object));
        }

        private bool CheckType(Type baseType, Type p)
        {
            return baseType.IsAssignableFrom(p);
        }

        private protected override bool CheckSupported(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.ManagedReference;
        }

        private protected override GUIContent GenerateHeader()
        {
            return new GUIContent("Available Types");
        }

        private protected override string GetButtonName(object currentValue)
        {
            if (currentValue is Type type)
            {
                return type.Name;
            }

            return "null";
        }

        private protected override void Setup(SerializedProperty property, SelectImplementationAttribute currentAttribute)
        {
            var currentObjectType = property.serializedObject.targetObject.GetType();
            LazyGetAllInheritedType(GetFieldType(), currentObjectType);
        }

        private protected override string[] ResolveGroupedName(object value, DisplayGrouping grouping)
        {
            if (value == null)
            {
                return new string[] { "null" };
            }

            if(value is Type type)
            {
                if (string.IsNullOrEmpty(type.FullName))
                {
                    return new string[] { type.Name };
                }

                var split = type.FullName.Split('.');
                if (split.Length <= 1)
                {
                    return new string[] { type.Name };
                }

                switch (grouping)
                {
                    case DisplayGrouping.Grouped:
                        return split;
                    case DisplayGrouping.GroupedFlat:
                        return new string[] { split.First(), split.Last() };
                    default:
                        throw new ArgumentOutOfRangeException(nameof(grouping), grouping, null);
                }
            }

            return new string[] { NotSupported };
        }

        private protected override List<object> GetSelectCollection()
        {
            return _reflectionTypes;
        }

        private protected override string ResolveName(object value, DisplayName displayName)
        {
            if (value == null)
            {
                return "null";
            }

            if (value is Type type)
            {
                switch (displayName)
                {
                    case DisplayName.Short:
                        return $"{type.Name}";
                    case DisplayName.Full:
                        return $"{type.FullName}";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(displayName), displayName, null);
                }
            }

            return NotSupported;
        }

        private protected override bool ResolveState(object currentValue, object iteratedValue)
        {
            if (iteratedValue == null && currentValue == null) return true;
            return iteratedValue is Type type && currentValue is Type currentType && currentType == type;
        }

        private protected override void AfterValueUpdated(SerializedProperty property)
        {
            _type = null;
        }

        private protected override void OnSelectItem(object obj)
        {
            if (obj == null)
            {
                _type = null;
                SetNeedUpdate();
                return;
            }

            var type = (SelectedItem<object>)obj;
            if (_type != default)
            {
                if (_type.Equals(type))
                {
                    return;
                }
            }

            _type = new SelectedItem<Type>(type.Property, type.Data as Type);
            SetNeedUpdate();
        }
    }
}