using Better.Attributes.EditorAddons.Drawers.Preview;
using Better.EditorTools.EditorAddons.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.WrapperCollections
{
    public class PreviewWrappers : WrapperCollection<BasePreviewWrapper>
    {
        public void PreDraw(Rect position, SerializedProperty property, float previewSize, bool objectChanged)
        {
            if (TryGetValue(property, out var wrapper))
            {
                wrapper.Wrapper.IsObjectUpdated(objectChanged);
                wrapper.Wrapper.PreDraw(position, property, previewSize);
            }
        }

        public bool ValidateObject(SerializedProperty property)
        {
            if (TryGetValue(property, out var wrapper))
            {
               return wrapper.Wrapper.ValidateObject(property);
            }

            return false;
        }
    }
}