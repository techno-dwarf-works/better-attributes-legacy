using System;
using System.Diagnostics;
using Better.EditorTools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.Gizmo
{
    /// <summary>
    /// Attribute to draw handles in scene view
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoAttribute : PropertyAttribute
    {
    }
}