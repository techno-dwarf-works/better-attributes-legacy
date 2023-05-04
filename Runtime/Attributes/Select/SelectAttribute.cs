using System;
using System.Diagnostics;

namespace Better.Attributes.Runtime.Select
{
    /// <summary>
    /// Attribute for selection in Inspector.
    /// Use in pair with [SerializeReference] Attribute.
    /// </summary>
    [Conditional(ConstantDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectAttribute : SelectAttributeBase
    {
        public SelectAttribute(Type type) : base(type)
        {
        }

        public SelectAttribute(Type type, DisplayName displayName) : base(type, displayName)
        {
        }

        public SelectAttribute(Type type, DisplayGrouping displayGrouping) : base(type, displayGrouping)
        {
        }

        public SelectAttribute(DisplayName displayName) : base(displayName)
        {
        }

        public SelectAttribute(DisplayGrouping displayGrouping) : base(displayGrouping)
        {
        }

        public SelectAttribute()
        {
        }
    }
}