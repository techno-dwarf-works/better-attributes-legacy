using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Select
{
    public enum DisplayName
    {
        Short,
        Full,
    }

    public enum DisplayGrouping
    {
        None = 1,
        Grouped = 2 ,
        GroupedFlat = 3
    }

    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class SelectAttributeBase : PropertyAttribute
    {
        private readonly Type _type;
        public DisplayName DisplayName { get; } = DisplayName.Short;
        public DisplayGrouping DisplayGrouping { get; } = DisplayGrouping.None;

        public SelectAttributeBase(Type type)
        {
            _type = type;
        }

        public SelectAttributeBase(Type type, DisplayName displayName)
        {
            _type = type;
            DisplayName = displayName;
        }
        
        public SelectAttributeBase(Type type, DisplayGrouping displayGrouping)
        {
            _type = type;
            DisplayName = DisplayName.Short;
            DisplayGrouping = displayGrouping;
        }

        public SelectAttributeBase(DisplayName displayName)
        {
            DisplayName = displayName;
        }

        public SelectAttributeBase(DisplayGrouping displayGrouping)
        {
            DisplayName = DisplayName.Short;
            DisplayGrouping = displayGrouping;
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