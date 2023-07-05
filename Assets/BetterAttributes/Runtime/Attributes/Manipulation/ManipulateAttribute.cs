using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using Better.Tools.Runtime.Attributes;
using UnityEngine;

namespace Better.Attributes.Runtime.Manipulation
{
    public enum ManipulationMode
    {
        Show,
        Hide,
        Disable,
        Enable
    }
    
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class ManipulateAttribute : MultiPropertyAttribute
    {
        public ManipulationMode ModeType { get; }

        public ManipulateAttribute(ManipulationMode modeType)
        {
            ModeType = modeType;
        }
    }
}