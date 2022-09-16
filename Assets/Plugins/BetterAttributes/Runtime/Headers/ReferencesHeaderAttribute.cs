using System;
using UnityEngine;

namespace BetterAttributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("References")
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ReferencesHeaderAttribute : HeaderAttribute
    {
        public ReferencesHeaderAttribute() : base("References")
        {
        }
    }
}
