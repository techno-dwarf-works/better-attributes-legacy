using System;
using System.Diagnostics;
using Better.EditorTools.Runtime.Attributes;
using Better.Internal.Core.Runtime;

namespace Better.Attributes.Runtime.Rename
{
    [Conditional(Defines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class RenameFieldAttribute : MultiPropertyAttribute
    {
        public RenameFieldAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}