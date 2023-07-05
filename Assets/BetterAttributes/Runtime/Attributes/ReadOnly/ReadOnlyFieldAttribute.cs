﻿using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using Better.Tools.Runtime.Attributes;
using UnityEngine;

namespace Better.Attributes.Runtime.ReadOnly
{
    /// <summary>
    /// Attribute to disable field editing in Inspector 
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyFieldAttribute : MultiPropertyAttribute
    {
        
    }
}