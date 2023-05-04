using System;
using System.Diagnostics;

namespace Better.Attributes.Runtime.Select
{
    /// <summary>
    /// Attribute for Implementation selection in Inspector.
    /// Use in pair with [SerializeReference] Attribute.
    /// </summary>
    [Conditional(ConstantDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    [Obsolete("Use SelectAttribute instead", true)]
    public class SelectEnumAttribute : SelectAttributeBase
    {
        public SelectEnumAttribute(Type type) : base(type)
        {
        }

        public SelectEnumAttribute(Type type, DisplayName displayName) : base(type, displayName)
        {
        }

        public SelectEnumAttribute(DisplayName displayName) : base(displayName)
        {
        }

        public SelectEnumAttribute()
        {
        }
    }
}