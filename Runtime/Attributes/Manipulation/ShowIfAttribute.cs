﻿using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowIfAttribute : ManipulateUserConditionAttribute
    {
        public ShowIfAttribute(string memberName, object memberValue) : base(memberName, memberValue, ManipulationMode.Show)
        {
        }
    }
}