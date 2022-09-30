#region license

// Copyright 2021 - 2022 Arcueid Elizabeth D'athemon
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using BetterAttributes.Runtime.Attributes.Select;
using UnityEditor;
using Object = UnityEngine.Object;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : SelectDrawerBase<SelectImplementationAttribute>
    {
        private Type _type;
        private List<object> _reflectionTypes;

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

        private protected override void Setup(SerializedProperty property,
            SelectImplementationAttribute currentAttribute)
        {
            var currentObjectType = property.serializedObject.targetObject.GetType();
            LazyGetAllInheritedType(currentAttribute.GetFieldType() ?? fieldInfo.FieldType, currentObjectType);
        }

        private protected override void UpdateValue(SerializedProperty property)
        {
            property.managedReferenceValue = _type == null ? null : Activator.CreateInstance(_type);
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

            return new string[] { "Not Supported" };
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

            return "Not supported";
        }

        private protected override bool ResolveState(object currentValue, object iteratedValue)
        {
            if (iteratedValue == null && currentValue == null) return true;
            return iteratedValue is Type type && currentValue?.GetType() == type;
        }

        private protected override void OnSelectItem(object obj)
        {
            if (obj == null)
            {
                _type = null;
                SetNeedUpdate();
                return;
            }

            var type = (Type)obj;
            if (_type != null)
            {
                if (_type == type)
                {
                    return;
                }
            }

            _type = type;
            SetNeedUpdate();
        }
    }
}