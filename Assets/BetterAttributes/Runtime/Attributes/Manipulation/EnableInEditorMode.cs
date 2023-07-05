using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class EnableInEditorMode : ManipulateAttribute
    {
        public EnableInEditorMode() : base(ManipulationMode.Enable)
        {
        }
    }
}