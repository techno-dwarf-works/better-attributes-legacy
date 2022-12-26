using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Base;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public abstract class UtilityWrapper
    {
        public abstract void Deconstruct();
    }

    public abstract class BaseUtility<THandler> where THandler : new()
    {
        private static THandler _instance;

        private protected HashSet<Type> _availableTypes;
        private protected WrappersTypeCollection _gizmoWrappersCollection;

        public static THandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new THandler();
                }

                return _instance;
            }
        }

        private protected BaseUtility()
        {
            Construct();
        }

        private void Construct()
        {
            _gizmoWrappersCollection = GenerateCollection();
            _availableTypes = GenerateAvailable();
        }

        private protected Dictionary<Type, Type> GetWrapperDictionary(Type type)
        {
            if (_gizmoWrappersCollection.TryGetValue(type, out var dictionary))
            {
                return dictionary;
            }

            throw new KeyNotFoundException($"Supported types not found for {type}");
        }

        /// <summary>
        /// Validates stored properties if their <see cref="WrapperCollectionValue{T}.Type"/> supported
        /// </summary>
        /// <param name="gizmoWrappers"></param>
        /// <typeparam name="T"></typeparam>
        public void ValidateCachedProperties<T>(WrapperCollection<T> gizmoWrappers) where T : UtilityWrapper
        {
            foreach (var value in gizmoWrappers)
            {
                if (!IsSupported(value.Value.Type))
                {
                    gizmoWrappers.Remove(value.Key);
                }
            }
        }

        /// <summary>
        /// Type collection for <see cref="GetUtilityWrapper"/>.
        /// <example> Example:
        /// <code>
        /// return new WrappersTypeCollection()
        /// {
        ///     {
        ///         typeof(SupportedAttributeType), new ()
        ///         {
        ///             { typeof(SupportedType), typeof(WrapperForSupportedType) },
        ///             { typeof(SupportedType2), typeof(WrapperForSupportedType2) }
        ///         }
        ///     }
        /// };
        /// </code>
        /// </example>
        /// <seealso cref="GenerateAvailable"/>
        /// </summary>
        /// <returns></returns>
        private protected abstract WrappersTypeCollection GenerateCollection();

        /// <summary>
        /// Types collection for <see cref="IsSupported"/> checks
        /// <example> Example:
        /// <code>
        ///  return new HashSet(BaseComparer.Instance)
        ///  {
        ///     typeof(SupportedType),
        ///     typeof(SupportedType2)
        ///  };
        /// </code>
        /// </example>
        /// <seealso cref="GenerateCollection"/>
        /// </summary>
        /// <returns></returns>
        private protected abstract HashSet<Type> GenerateAvailable();

        /// <summary>
        /// Generate ready to use wrapper's instance by dictionary from <see cref="GetWrapperDictionary"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attributeType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUtilityWrapper<T>(Type type, Type attributeType) where T : UtilityWrapper
        {
            if (!IsSupported(type))
            {
                return null;
            }

            var gizmoWrappers = GetWrapperDictionary(attributeType);
            var wrapperType = gizmoWrappers[type];

            return (T)Activator.CreateInstance(wrapperType);
        }

        /// <summary>
        /// Checks if type available in utility
        /// <seealso cref="GenerateAvailable"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool IsSupported(Type type)
        {
            return _availableTypes.Contains(type);
        }
    }
}