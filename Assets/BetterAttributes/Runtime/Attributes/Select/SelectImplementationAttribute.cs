using System;
using System.Diagnostics;
using Better.EditorTools.Runtime;
using UnityEngine.Playables;

namespace Better.Attributes.Runtime.Select
{
    /// <summary>
    /// Attribute for Implementation selection in Inspector.
    /// Use in pair with [SerializeReference] Attribute.
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
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