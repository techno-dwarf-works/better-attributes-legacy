using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Gizmo
{
    /// <summary>
    /// Attribute to draw handles in scene view
    /// </summary>
    [Conditional(ConstantDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoAttribute : PropertyAttribute
    {
    }
}