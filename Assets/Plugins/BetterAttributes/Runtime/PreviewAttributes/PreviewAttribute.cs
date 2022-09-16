using System;
using System.Diagnostics;
using UnityEngine;

namespace BetterAttributes.Runtime.PreviewAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class PreviewAttribute : PropertyAttribute
    {
        private readonly float _previewSize;

        public float PreviewSize => _previewSize;

        public PreviewAttribute()
        {
            _previewSize = 250f;
        }

        public PreviewAttribute(float previewSize)
        {
            _previewSize = previewSize;
        }
    }
}