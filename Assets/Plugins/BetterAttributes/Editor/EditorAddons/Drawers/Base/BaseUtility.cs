using System;
using System.Collections.Generic;

namespace BetterAttributes.EditorAddons.Drawers.Base
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
        
        public void ValidateCachedProperties<T>(WrapperCollection<T> gizmoWrappers) where T : UtilityWrapper
        {
            foreach (var (key, value) in gizmoWrappers)
            {
                if (!IsSupported(value.Item2))
                {
                    gizmoWrappers.Remove(key);
                }
            }
        }

        private protected abstract WrappersTypeCollection GenerateCollection();

        private protected abstract HashSet<Type> GenerateAvailable();

        public virtual T GetUtilityWrapper<T>(Type type, Type attributeType) where T : UtilityWrapper
        {
            if (!IsSupported(type))
            {
                return null;
            }

            var gizmoWrappers = GetWrapperDictionary(attributeType);
            var wrapperType = gizmoWrappers[type];

            if (wrapperType.IsAssignableFrom(typeof(T))) return null;

            return (T)Activator.CreateInstance(wrapperType);
        }

        public virtual bool IsSupported(Type type)
        {
            return _availableTypes.Contains(type);
        }
    }
}