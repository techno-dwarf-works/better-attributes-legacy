using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Manipulation
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class ManipulateUserConditionAttribute : ManipulateAttribute
    {
        public string MemberName { get; }
        public object MemberValue { get; }
        
        public ManipulateUserConditionAttribute(string memberName, object memberValue, ManipulationMode modeType) : base(modeType)
        {
            MemberName = memberName;
            MemberValue = memberValue;
            order = -999;
        }
    }
}