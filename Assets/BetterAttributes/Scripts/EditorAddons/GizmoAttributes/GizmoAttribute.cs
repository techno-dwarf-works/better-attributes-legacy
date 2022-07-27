using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.EditorAddons.GizmoAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoAttribute : PropertyAttribute
    {
    }
    
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoLocalAttribute : PropertyAttribute
    {
    }
}