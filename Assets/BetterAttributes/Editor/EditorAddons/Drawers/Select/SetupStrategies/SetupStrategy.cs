using System;
using System.Collections.Generic;
using System.Reflection;
using Better.Attributes.Runtime.Select;
using Better.Extensions.Runtime;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select.SetupStrategies
{
    public abstract class SetupStrategy
    {
        public abstract List<object> Setup(Type baseType);
        public abstract GUIContent ResolveName(object value, DisplayName displayName);
        public abstract GUIContent[] ResolveGroupedName(object value, DisplayGrouping grouping);
        public abstract string GetButtonName(object currentValue);
        public abstract bool ResolveState(object currentValue, object iteratedValue);
        public abstract bool Validate(object item);
        public abstract GUIContent GenerateHeader();

        public virtual Type GetFieldOrElementType(FieldInfo fieldInfo, SelectAttributeBase selectAttributeBase)
        {
            var t = selectAttributeBase.GetFieldType();
            if (t != null)
            {
                return t;
            }

            if (fieldInfo.IsArrayOrList())
            {
                return fieldInfo.GetArrayOrListElementType();
            }

            return fieldInfo.FieldType;
        }
    }
}