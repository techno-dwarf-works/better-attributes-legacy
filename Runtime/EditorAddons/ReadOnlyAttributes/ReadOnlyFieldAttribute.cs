using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.EditorAddons.ReadOnlyAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyFieldAttribute : PropertyAttribute
    {
        
    }
}