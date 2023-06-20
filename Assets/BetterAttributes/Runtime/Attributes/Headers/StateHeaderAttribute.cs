using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("State")
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StateHeaderAttribute : HeaderAttribute
    {
        public StateHeaderAttribute() : base("State")
        {
        }
    }
}