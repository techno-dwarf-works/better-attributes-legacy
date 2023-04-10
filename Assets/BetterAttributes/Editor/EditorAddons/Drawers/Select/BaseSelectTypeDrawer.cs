using System;
using System.Collections.Generic;
using System.Linq;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Select;
using Better.EditorTools.Drawers.Base;
using Better.Extensions.Runtime;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    public abstract class BaseSelectTypeDrawer : SelectDrawerBase<SelectImplementationAttribute, SelectTypeWrapper>
    {
        private protected SelectedItem<Type> _type;
        private List<object> _reflectionTypes;

        private protected SelectTypeWrappers Collection => _wrappers as SelectTypeWrappers;

        protected override WrapperCollection<SelectTypeWrapper> GenerateCollection()
        {
            return new SelectTypeWrappers();
        }

        private void LazyGetAllInheritedType(Type baseType)
        {
            if (_reflectionTypes != null) return;
            _reflectionTypes = ReflectionExtensions.GetAllInheritedType(baseType).Cast<object>().ToList();
            _reflectionTypes.Insert(0, null);
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

        private protected override void Setup(SerializedProperty property,
            SelectImplementationAttribute currentAttribute)
        {
            var currentObjectType = property.serializedObject.targetObject.GetType();
            LazyGetAllInheritedType(GetFieldOrElementType());
        }

        private protected override GUIContent[] ResolveGroupedName(object value, DisplayGrouping grouping)
        {
            if (value == null)
            {
                return new GUIContent[] { new GUIContent("null") };
            }

            if (value is Type type)
            {
                if (string.IsNullOrEmpty(type.FullName))
                {
                    return new GUIContent[] { new GUIContent(type.Name) };
                }

                var split = type.FullName.Split('.');
                if (split.Length <= 1)
                {
                    return new GUIContent[] { new GUIContent(type.Name) };
                }

                switch (grouping)
                {
                    case DisplayGrouping.Grouped:
                        return split.Select(x => new GUIContent(x)).ToArray();
                    case DisplayGrouping.GroupedFlat:
                        return new GUIContent[] { new GUIContent(split.First()), new GUIContent(split.Last()) };
                    default:
                        throw new ArgumentOutOfRangeException(nameof(grouping), grouping, null);
                }
            }

            return new GUIContent[] { new GUIContent(NotSupported) };
        }

        private protected override List<object> GetSelectCollection()
        {
            return _reflectionTypes;
        }

        private protected override GUIContent ResolveName(object value, DisplayName displayName)
        {
            if (value == null)
            {
                return new GUIContent("null");
            }

            if (value is Type type)
            {
                switch (displayName)
                {
                    case DisplayName.Short:
                        return new GUIContent($"{type.Name}");
                    case DisplayName.Full:
                        return new GUIContent($"{type.FullName}");
                    default:
                        throw new ArgumentOutOfRangeException(nameof(displayName), displayName, null);
                }
            }

            return new GUIContent(NotSupported);
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