using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Gizmo
{
    /// <summary>
    /// Attribute to draw handles in scene view
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoAttribute : PropertyAttribute
    {
    }
}