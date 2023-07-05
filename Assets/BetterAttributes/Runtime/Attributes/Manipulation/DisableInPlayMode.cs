using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class DisableInPlayMode : ManipulateAttribute
    {
        public DisableInPlayMode() : base(ManipulationMode.Disable)
        {
        }
    }
}