using System;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Headers
{
    /// <summary>
    /// Replacement for Header("Prefabs")
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class PrefabHeaderAttribute : HeaderAttribute
    {
        public PrefabHeaderAttribute() : base("Prefabs")
        {
        }
    }
}
