﻿using System;
using System.Diagnostics;
using Better.EditorTools.Runtime.Attributes;
using Better.Internal.Core.Runtime;

namespace Better.Attributes.Runtime.Misc
{
    [Conditional(Defines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class MiscAttribute : MultiPropertyAttribute
    {
        
    }
}