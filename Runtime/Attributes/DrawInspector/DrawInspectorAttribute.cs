using System;
using System.Diagnostics;
using Better.EditorTools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.DrawInspector
{
    /// <summary>
    /// Replaces object field with nested inspector
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class DrawInspectorAttribute : PropertyAttribute
    {
    }
}