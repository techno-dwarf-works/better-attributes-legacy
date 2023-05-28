using System;
using System.Diagnostics;
using Better.EditorTools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("References")
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ReferencesHeaderAttribute : HeaderAttribute
    {
        public ReferencesHeaderAttribute() : base("References")
        {
        }
    }
}