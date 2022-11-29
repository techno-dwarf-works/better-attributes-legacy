using System;
using System.Diagnostics;
using UnityEngine;

namespace Better.Attributes.Runtime.ReadOnly
{
    /// <summary>
    /// Attribute to disable field editing in Inspector 
    /// </summary>
    [Conditional(ConstantDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyFieldAttribute : PropertyAttribute
    {
        
    }
}