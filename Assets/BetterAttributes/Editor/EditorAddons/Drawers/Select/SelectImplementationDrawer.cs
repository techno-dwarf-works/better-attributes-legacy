using System;
using System.Reflection;
using Better.Attributes.Runtime.Select;
using Better.EditorTools;
using Better.EditorTools.Helpers;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : BaseSelectTypeDrawer
    {
        private protected override object GetCurrentValue(SerializedProperty property)
        {
            return property.GetManagedType();
        }

        private bool ValidateType(Type type)
        {
            if (type.IsValueType)
            {
                return true;
            }

            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null, Type.EmptyTypes, null);

            return constructor != null;
        }

        private protected override GUIContent ResolveName(object value, DisplayName displayName)
        {
            if (value is Type type && !ValidateType(type))
            {
                var resolveName = DrawersHelper.GetIconGUIContent(IconType.ErrorMessage);
                resolveName.text = $"{type.Name}";
                resolveName.tooltip = "Type has not parameterless constructor!";
                return resolveName;
            }

            return base.ResolveName(value, displayName);
        }

        private protected override GUIContent[] ResolveGroupedName(object value, DisplayGrouping grouping)
        {
            if (value is Type type && !ValidateType(type))
            {
                var resolveName = DrawersHelper.GetIconGUIContent(IconType.ErrorMessage);
                resolveName.text = $"{type.Name}";
                resolveName.tooltip = "Type has not parameterless constructor!";
                return new GUIContent[] { resolveName };
            }

            return base.ResolveGroupedName(value, grouping);
        }

        private protected override void OnSelectItem(object obj)
        {
            if (obj is SelectedItem<object> value && value.Data is Type type && !ValidateType(type))
            {
                return;
            }
            base.OnSelectItem(obj);
        }

        private protected override void UpdateValue(SerializedProperty property)
        {
            Collection.Update(_type.Property, _type.Data);
        }
    }
}