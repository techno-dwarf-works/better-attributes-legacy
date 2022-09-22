using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Select
{
    public enum DisplayName
    {
        Short, Full, Extended
    }
    
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class SelectAttributeBase : PropertyAttribute
    {
        private readonly Type _type;
        public DisplayName DisplayName { get; } = DisplayName.Short;

        public SelectAttributeBase(Type type)
        {
            _type = type;
        }
        
        public SelectAttributeBase(Type type, DisplayName displayName)
        {
            _type = type;
            DisplayName = displayName;
        }
        
        public SelectAttributeBase(DisplayName displayName)
        {
            DisplayName = displayName;
        }

        public Type GetFieldType()
        {
            return _type;
        }

        public SelectAttributeBase()
        {
        }
    }
}
