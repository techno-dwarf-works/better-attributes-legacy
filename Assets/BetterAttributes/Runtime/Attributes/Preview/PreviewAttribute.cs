using System;
using System.Diagnostics;
using Better.Tools.Runtime;
using UnityEngine;

namespace Better.Attributes.Runtime.Preview
{
    /// <summary>
    /// Attribute for preview in Inspector.
    /// Click on question mark or label to see preview.
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Field)]
    public class PreviewAttribute : PropertyAttribute
    {
        private readonly float _previewSize;

        public float PreviewSize => _previewSize;

        public PreviewAttribute()
        {
            _previewSize = 150f;
        }

        public PreviewAttribute(float previewSize)
        {
            _previewSize = previewSize;
        }
    }
}