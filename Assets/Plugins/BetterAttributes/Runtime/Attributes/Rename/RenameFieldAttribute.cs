using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.Attributes.Rename
{
    [Conditional(ConstantDefines.Editor)]
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