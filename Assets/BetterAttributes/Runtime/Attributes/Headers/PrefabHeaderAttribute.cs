using System;
using System.Diagnostics;
using Better.EditorTools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("Prefabs")
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class PrefabHeaderAttribute : HeaderAttribute
    {
        public PrefabHeaderAttribute() : base("Prefabs")
        {
        }
    }
}