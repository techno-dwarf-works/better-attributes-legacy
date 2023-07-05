using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using Better.Tools.Runtime.Attributes;
using UnityEngine;

namespace Better.Attributes.Runtime.Rename
{
    [Conditional(BetterEditorDefines.Editor)]
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