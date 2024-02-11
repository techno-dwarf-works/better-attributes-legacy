﻿using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Misc
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumButtonsAttribute : MiscAttribute
    {
        public EnumButtonsAttribute()
        {
            order = 999;
        }
    }
}