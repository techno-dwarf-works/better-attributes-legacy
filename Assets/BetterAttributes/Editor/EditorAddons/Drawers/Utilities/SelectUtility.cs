using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Select.SetupStrategies;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.Attributes.Runtime.Select;
using Better.EditorTools.Comparers;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Utilities;
using Better.Extensions.Runtime;
using UnityEditor;
using UnityEngine;

#pragma warning disable CS0618

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class SelectUtility : BaseUtility<SelectUtility>
    {
        public const string NotSupported = "Not supported";

        protected Dictionary<Type, Dictionary<Type, Type>> _setupWrappers;
        private HashSet<SerializedPropertyType> _supportedTypes;

        public SelectUtility()
        {
            _setupWrappers = new Dictionary<Type, Dictionary<Type, Type>>(TypeComparer.Instance)
            {
                {
                    typeof(SelectAttribute), new Dictionary<Type, Type>(TypeComparer.Instance)
                    {
                        { typeof(SerializedType), typeof(SelectTypeStrategy) },
                        { typeof(Enum), typeof(SelectEnumStrategy) },
                        { typeof(Type), typeof(SelectImplementationStrategy) }
                    }
                }
            };

            _supportedTypes = new HashSet<SerializedPropertyType>()
            {
                SerializedPropertyType.Generic,
                SerializedPropertyType.ManagedReference,
                SerializedPropertyType.Enum
            };
        }

        protected override BaseWrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection(TypeComparer.Instance)
            {
                {
                    typeof(SelectAttribute), new Dictionary<Type, Type>(TypeComparer.Instance)
                    {
                        { typeof(SerializedType), typeof(SelectSerializedTypeWrapper) },
                        { typeof(Enum), typeof(SelectEnumWrapper) },
                        { typeof(Type), typeof(SelectTypeWrapper) }
                    }
                }
            };
        }

        protected Dictionary<Type, Type> GetSetupStrategyDictionary(Type type)
        {
            if (_setupWrappers.TryGetValue(type, out var dictionary))
            {
                return dictionary;
            }

            throw new KeyNotFoundException($"Supported types not found for {type}");
        }

        /// <summary>
        /// Generate ready to use wrapper's instance by dictionary from <see cref="GetSetupStrategyDictionary"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public SetupStrategy GetSetupStrategy(Type type, SelectAttributeBase attribute)
        {
            if (!IsSupported(type))
            {
                return null;
            }

            var gizmoWrappers = GetSetupStrategyDictionary(attribute.GetType());
            var wrapperType = gizmoWrappers[type];

            return (SetupStrategy)Activator.CreateInstance(wrapperType, new object[]{type, attribute});
        }

        protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>(TypeComparer.Instance)
            {
                typeof(Enum),
                typeof(Type),
                typeof(SerializedType)
            };
        }

        public bool CheckSupportedType(SerializedPropertyType propertyType)
        {
            return _supportedTypes.Contains(propertyType);
        }
    }
}