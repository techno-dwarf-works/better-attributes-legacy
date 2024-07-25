using System;
using System.Collections.Generic;
using System.Linq;
using Better.Attributes.EditorAddons.Extensions;
using Better.Attributes.Runtime;
using Better.Attributes.Runtime.Select;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.EditorAddons.Utility;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utility;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    public class SelectSerializedTypeHandler : BaseSelectTypeHandler
    {
        protected override void OnSetup()
        {
            
        }

        public override void Update(object value)
        {
            var property = _container.SerializedProperty;
            if (!property.Verify()) return;
            var typeValue = (Type)value;
            if (property.propertyType == SerializedPropertyType.Generic)
            {
                var buffer = typeValue != null ? new SerializedType(typeValue) : new SerializedType();
                property.SetValue(buffer);
            }
            else if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                var buffer = typeValue == null ? null : Activator.CreateInstance(typeof(SerializedType), typeValue);
                property.managedReferenceValue = buffer;
            }
            base.Update(value);
        }

        public override object GetCurrentValue()
        {
            var property = _container.SerializedProperty;
            var objectOfProperty = property.GetValue();
            Type type = null;
            if (objectOfProperty == null)
            {
                type = GetFieldOrElementType();
                return type;
            }
            
            type = objectOfProperty.GetType();
            if (type == typeof(SerializedType))
            {
                type = (objectOfProperty as SerializedType)?.Type;
            }

            return type;
        }
        
        protected override bool ResolveState(object iteratedValue)
        {
            if (iteratedValue == null && _currentValue == null) return true;
            return iteratedValue is Type type && _currentValue is Type currentType && currentType == type;
        }
        
        public override bool IsSkippingFieldDraw()
        {
            return true;
        }

        public override bool ValidateSelected(object item)
        {
            return true;
        }

        public override bool CheckSupported()
        {
            return true;
        }

        public override GUIContent GenerateHeader()
        {
            return new GUIContent("Available Types");
        }

        protected override IEnumerable<GUIContent> GetResolvedGroupedName(object value, DisplayGrouping grouping)
        {
            if (value == null)
            {
                return new GUIContent[] { new GUIContent(LabelDefines.Null) };
            }

            if (value is Type type)
            {
                if (string.IsNullOrEmpty(type.FullName))
                {
                    return new GUIContent[] { new GUIContent(type.Name) };
                }

                var split = type.FullName.Split(SelectorUtility.NameSeparator);
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
                        DebugUtility.LogException<ArgumentOutOfRangeException>(nameof(grouping));
                        return Array.Empty<GUIContent>();
                }
            }

            return new GUIContent[] { new GUIContent(LabelDefines.NotSupported) };
        }

        protected override GUIContent GetResolvedName(object value, DisplayName displayName)
        {
            if (value == null)
            {
                return new GUIContent(LabelDefines.Null);
            }

            if (value is Type type)
            {
                switch (displayName)
                {
                    case DisplayName.Short:
                        return new GUIContent(type.Name);
                    case DisplayName.Full:
                        return new GUIContent(type.FullName);
                    default:
                        DebugUtility.LogException<ArgumentOutOfRangeException>(nameof(displayName));
                        return null;
                }
            }

            return new GUIContent(LabelDefines.NotSupported);
        }

        public override string GetButtonText()
        {
            if (_currentValue is Type type)
            {
                return type.Name;
            }

            return LabelDefines.Null;
        }

        protected override IEnumerable<Type> GetInheritedTypes(Type fieldType)
        {
            return fieldType.GetAllInheritedTypes();
        }
    }
}