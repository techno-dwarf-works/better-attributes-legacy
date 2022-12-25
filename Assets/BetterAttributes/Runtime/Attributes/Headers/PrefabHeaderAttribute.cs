using System;
using System.Diagnostics;
using UnityEngine;

namespace Better.Attributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("Prefabs")
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional(ConstantDefines.Editor)]
    public class PrefabHeaderAttribute : HeaderAttribute
    {
        public PrefabHeaderAttribute() : base("Prefabs")
        {
        }
    }
}