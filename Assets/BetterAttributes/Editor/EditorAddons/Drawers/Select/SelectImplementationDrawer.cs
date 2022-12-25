using System;
using System.Linq;
using System.Reflection;
using Better.Attributes.EditorAddons.Helpers;
using Better.Attributes.Runtime.Select;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : BaseSelectTypeDrawer
    {
        private protected override object GetCurrentValue(SerializedProperty property)
        {
#if UNITY_2021_1_OR_NEWER
            return property.managedReferenceValue?.GetType();
#else
            if (string.IsNullOrEmpty(property.managedReferenceFullTypename))
            {
                return null;
            }

            var split = property.managedReferenceFullTypename.Split(' ');
            var assembly = GetAssembly(split[0]);
            var currentValue = assembly.GetType(split[1]);
            return currentValue;
#endif
        }

        private static Assembly GetAssembly(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == name);
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