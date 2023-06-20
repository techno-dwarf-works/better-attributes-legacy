using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.Rename
{
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class RenameFieldAttribute : PropertyAttribute
    {
        public RenameFieldAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}