using System;
using System.Diagnostics;
using Better.EditorTools.Runtime.Attributes;
using Better.Internal.Core.Runtime;

namespace Better.Attributes.Runtime.Gizmo
{
    /// <summary>
    /// Attribute to draw handles in scene view in local space
    /// This attribute works only for scene objects
    /// </summary>
    [Conditional(Defines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoLocalAttribute : MultiPropertyAttribute
    {
    }
}