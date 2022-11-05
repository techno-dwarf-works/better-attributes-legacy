using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Preview;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.WrapperCollections
{
    public class PreviewWrappers : WrapperCollection<BasePreviewWrapper>
    {
        public void OnGUI(Rect position, SerializedProperty property, float previewSize, bool objectChanged)
        {
            if (TryGetValue(property, out var wrapper))
            {
                wrapper.Wrapper.IsObjectUpdated(objectChanged);
                wrapper.Wrapper.OnGUI(position, property, previewSize);
            }
        }
    }
}