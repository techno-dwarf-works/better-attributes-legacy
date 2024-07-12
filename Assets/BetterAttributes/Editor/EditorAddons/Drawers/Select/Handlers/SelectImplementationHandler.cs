using System;
using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using Better.Commons.EditorAddons.Enums;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.Runtime.Extensions;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    public class SelectImplementationHandler : BaseSelectTypeHandler
    {
        protected override void OnSetup()
        {
        }

        public override void Update(object value)
        {
            var property = _container.SerializedProperty;
            if (!property.Verify()) return;
            var typeValue = (Type)value;
            property.managedReferenceValue = typeValue == null ? null : Activator.CreateInstance(typeValue);

            base.Update(value);
        }

        public override void OnPopulateContainer()
        {
        }

        public override object GetCurrentValue()
        {
            var property = _container.SerializedProperty;
            return property.GetManagedType();
        }

        protected override IEnumerable<Type> GetInheritedTypes(Type fieldType)
        {
            return fieldType.GetAllInheritedTypesWithoutUnityObject();
        }

        public override bool IsSkippingFieldDraw()
        {
            return false;
        }

        public override bool CheckSupported()
        {
            var baseType = GetFieldOrElementType();
            return baseType.IsAbstract || baseType.IsInterface;
        }

        public override bool ValidateSelected(object item)
        {
            var isValid = base.ValidateSelected(item);
            if (!isValid)
            {
                return false;
            }

            if (item == null)
            {
                return true;
            }

            return item is Type type && type.HasParameterlessConstructor();
        }

        protected override IEnumerable<GUIContent> GetResolvedGroupedName(object value, DisplayGrouping grouping)
        {
            if (value is Type type)
            {
                if (!type.HasParameterlessConstructor())
                {
                    var resolveName = GUIContent(type);
                    return new GUIContent[] { resolveName };
                }
            }

            return base.GetResolvedGroupedName(value, grouping);
        }

        protected override GUIContent GetResolvedName(object value, DisplayName displayName)
        {
            if (value is Type type)
            {
                if (!type.HasParameterlessConstructor())
                {
                    var resolveName = GUIContent(type);
                    return resolveName;
                }
            }

            return base.GetResolvedName(value, displayName);
        }

        private static GUIContent GUIContent(Type type)
        {
            var resolveName = IconType.ErrorMessage.GetIconGUIContent();
            resolveName.text = $"{type.Name}";
            resolveName.tooltip = "Type has not parameterless constructor!";
            return resolveName;
        }
    }
}