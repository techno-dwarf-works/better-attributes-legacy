using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using Better.Tools.Runtime.Attributes;
using UnityEngine;

namespace Better.Attributes.Runtime.Select
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

    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class SelectAttributeBase : MultiPropertyAttribute
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