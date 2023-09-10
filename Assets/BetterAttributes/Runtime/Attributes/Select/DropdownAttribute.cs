using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime.Select
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class DropdownAttribute : SelectAttributeBase
    {
        private readonly string _selectorName;

        public bool ShowDefault { get; set; } = true;
        public bool ShowUniqueKey { get; set; }

        public DropdownAttribute(string selectorName) : base(null)
        {
            _selectorName = selectorName;
        }

        public DropdownAttribute(string selectorName, DisplayName displayName) : base(null, displayName)
        {
            _selectorName = selectorName;
        }

        public DropdownAttribute(string selectorName, DisplayGrouping displayGrouping) : base(null, displayGrouping)
        {
            _selectorName = selectorName;
        }

        public string SelectorName => _selectorName;
    }
}