using System;
using System.Diagnostics;
using UnityEngine;

namespace Better.Attributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("State")
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional(ConstantDefines.Editor)]
    public class StateHeaderAttribute : HeaderAttribute
    {
        public StateHeaderAttribute() : base("State")
        {
        }
    }
}