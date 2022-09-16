using System;
using UnityEngine;

namespace BetterAttributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("State")
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StateHeaderAttribute : HeaderAttribute
    {
        public StateHeaderAttribute() : base("State")
        {
        }
    }
}
