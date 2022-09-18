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
            foreach (var value in Values)
            {
                value.Item1.IsObjectUpdated(objectChanged);
                value.Item1.OnGUI(position, property, previewSize);
            }
        }
    }
}