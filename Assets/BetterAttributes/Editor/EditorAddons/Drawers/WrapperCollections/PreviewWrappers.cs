using Better.Attributes.EditorAddons.Drawers.Preview;
using Better.EditorTools.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.WrapperCollections
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