using System;
using System.Diagnostics;
using Better.Extensions.Runtime;
using Better.Tools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.Headers
{
    /// <summary>
    /// Replacement for Header("Settings")
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class SettingsHeaderAttribute : HeaderAttribute
    {
        public SettingsHeaderAttribute() : base("Settings")
        {
        }

        public SettingsHeaderAttribute(string additionalText, bool preHeader = true) : base(preHeader
            ? $"{additionalText.PrettyCamelCase()} Settings"
            : $"Settings {additionalText.PrettyCamelCase()}")
        {
        }
    }
}