using System;
using BetterAttributes.EditorAddons.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    public class PreviewWrapperCollection : WrapperCollection<BasePreviewWrapper>
    {
        public void OnGUI(Rect position, SerializedProperty property, float previewSize)
        {
            foreach (var value in Values)
            {
                value.Item1.OnGUI(position, property, previewSize);
            }
        }
    }
}