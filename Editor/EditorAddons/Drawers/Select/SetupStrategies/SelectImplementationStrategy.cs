using System;
using System.Reflection;
using Better.Attributes.Runtime.Select;
using Better.Commons.EditorAddons.Enums;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.EditorAddons.Utility;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select.SetupStrategies
{
    public class SelectImplementationStrategy : SelectTypeStrategy
    {
        public SelectImplementationStrategy(FieldInfo fieldInfo, object container, SelectAttributeBase selectAttributeBase) : base(fieldInfo, container,
            selectAttributeBase)
        {
        }

        public override GUIContent[] ResolveGroupedName(object value, DisplayGrouping grouping)
        {
            if (value is Type type)
            {
                if (!ValidateInternal(type))
                {
                    var resolveName = GUIContent(type);
                    return new GUIContent[] { resolveName };
                }
            }

            return base.ResolveGroupedName(value, grouping);
        }

        public override GUIContent ResolveName(object value, DisplayName displayName)
        {
            if (value is Type type)
            {
                if (!ValidateInternal(type))
                {
                    var resolveName = GUIContent(type);
                    return resolveName;
                }
            }

            return base.ResolveName(value, displayName);
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