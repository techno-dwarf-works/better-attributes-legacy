﻿using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnableIf : ManipulateConditionAttribute
    {
        public EnableIf(string memberName, object memberValue) : base(memberName, memberValue, ManipulationMode.Enable)
        {
        }
    }
}