using Better.Attributes.EditorAddons.Drawers.DrawInspector;
using Better.EditorTools.EditorAddons.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.WrapperCollections
{
    public class DrawInspectors : WrapperCollection<DrawInspectorWrapper>
    {
        public void PostDraw(SerializedProperty serializedProperty, Rect rect)
        {
            if (TryGetValue(serializedProperty, out var collectionValue))
            {
                collectionValue.Wrapper.PostDraw(rect);
            }
        }

        public void SetOpen(SerializedProperty serializedProperty, bool value)
        {
            if (TryGetValue(serializedProperty, out var collectionValue))
            {
                collectionValue.Wrapper.SetOpen(value);
            }
        }

        public bool IsOpen(SerializedProperty serializedProperty)
        {
            if (TryGetValue(serializedProperty, out var collectionValue))
            {
                return collectionValue.Wrapper.IsOpen();
            }

            return false;
        }

        public void SetProperty(SerializedProperty property)
        {
            if (TryGetValue(property, out var wrapper))
            {
                wrapper.Wrapper.SetProperty(property);
            }
        }

        public float GetHeight(SerializedProperty property)
        {
            if (TryGetValue(property, out var wrapper))
            {
                return wrapper.Wrapper.GetHeight();
            }

            return 0;
        }
    }
}