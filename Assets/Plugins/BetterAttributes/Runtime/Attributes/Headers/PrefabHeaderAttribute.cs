using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Headers
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