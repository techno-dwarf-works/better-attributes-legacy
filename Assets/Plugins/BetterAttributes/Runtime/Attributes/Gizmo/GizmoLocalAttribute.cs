using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Gizmo
{
    /// <summary>
    /// Attribute to draw handles in scene view in local space
    /// This attribute works only for scene objects
    /// </summary>
    [Conditional(ConstantDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoLocalAttribute : PropertyAttribute
    {
    }
}