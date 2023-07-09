using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class HideInPlayModeAttribute : ManipulateAttribute
    {
        public HideInPlayModeAttribute() : base(ManipulationMode.Hide)
        {
        }
    }
}