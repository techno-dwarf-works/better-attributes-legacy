using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Plugin.EditorAddons.ReadOnlyAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyFieldAttribute : PropertyAttribute
    {
        
    }
}