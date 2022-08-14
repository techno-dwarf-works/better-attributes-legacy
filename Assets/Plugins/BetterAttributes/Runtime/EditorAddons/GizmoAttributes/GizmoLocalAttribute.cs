using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.EditorAddons.GizmoAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class GizmoLocalAttribute : PropertyAttribute
    {
    }
}