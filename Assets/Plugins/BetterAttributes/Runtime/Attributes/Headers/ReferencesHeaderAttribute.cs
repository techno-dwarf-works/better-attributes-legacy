using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Headers
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