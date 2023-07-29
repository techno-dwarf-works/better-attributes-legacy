using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Misc
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class CustomTooltipAttribute : MiscAttribute
    {
        public string Tooltip { get; }
        
        public char TooltipSymbol { get; set; }

        public CustomTooltipAttribute(string tooltip)
        {
            Tooltip = tooltip;
        }
    }
}