using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    /// <summary>
    /// Attribute to disable field editing in Inspector 
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : ManipulateAttribute
    {
        public ReadOnlyAttribute() : base(ManipulationMode.Disable)
        {
        }
    }
}