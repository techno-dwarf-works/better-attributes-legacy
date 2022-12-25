using System;
using System.Diagnostics;
using UnityEngine;

namespace Better.Attributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("References")
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional(ConstantDefines.Editor)]
    public class ReferencesHeaderAttribute : HeaderAttribute
    {
        public ReferencesHeaderAttribute() : base("References")
        {
        }
    }
}