using System;
using System.Diagnostics;
using Better.Internal.Core.Runtime;

namespace Better.Attributes.Runtime.Select
{
    /// <summary>
    /// Attribute for Implementation selection in Inspector.
    /// Use in pair with [SerializeReference] Attribute.
    /// </summary>
    [Conditional(Defines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    [Obsolete("Use SelectAttribute instead")]
    public class SelectImplementationAttribute : SelectAttribute
    {
        public SelectImplementationAttribute(Type type) : base(type)
        {
        }

        public SelectImplementationAttribute(Type type, DisplayName displayName) : base(type, displayName)
        {
        }

        public SelectImplementationAttribute(Type type, DisplayGrouping displayGrouping) : base(type, displayGrouping)
        {
        }

        public SelectImplementationAttribute(DisplayName displayName) : base(displayName)
        {
        }

        public SelectImplementationAttribute(DisplayGrouping displayGrouping) : base(displayGrouping)
        {
        }

        public SelectImplementationAttribute()
        {
        }
    }
}