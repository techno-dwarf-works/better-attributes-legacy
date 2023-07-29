using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using Better.Tools.Runtime.Attributes;

namespace Better.Attributes.Runtime.Misc
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class MiscAttribute : MultiPropertyAttribute
    {
        
    }
    
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class HelpBoxAttribute : MiscAttribute
    {
        public HelpBoxAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}