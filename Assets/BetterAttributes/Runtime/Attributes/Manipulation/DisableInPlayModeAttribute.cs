using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class DisableInPlayModeAttribute : ManipulateAttribute
    {
        public DisableInPlayModeAttribute() : base(ManipulationMode.Disable)
        {
        }
    }
}