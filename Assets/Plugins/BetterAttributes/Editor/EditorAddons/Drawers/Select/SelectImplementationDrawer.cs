using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using BetterAttributes.Runtime.Attributes.Select;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Select
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

        private protected override void UpdateValue(SerializedProperty property)
        {
            Collection.Update(_type.Property, _type.Data);
        }
    }
}