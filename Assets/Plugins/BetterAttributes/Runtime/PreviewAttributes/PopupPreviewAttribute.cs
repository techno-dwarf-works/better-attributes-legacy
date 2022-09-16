using System;
using System.Diagnostics;

namespace BetterAttributes.Runtime.PreviewAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public class PopupPreviewAttribute : PreviewAttribute
    {
    }
}