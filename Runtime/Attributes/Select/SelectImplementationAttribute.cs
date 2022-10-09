using System;
using System.Diagnostics;

namespace BetterAttributes.Runtime.Attributes.Select
{
    /// <summary>
    /// Attribute for Implementation selection in Inspector.
    /// Use in pair with [SerializeReference] Attribute.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class SelectImplementationAttribute : SelectAttributeBase
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
