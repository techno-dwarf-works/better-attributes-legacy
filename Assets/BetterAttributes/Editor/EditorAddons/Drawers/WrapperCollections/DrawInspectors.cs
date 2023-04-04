using Better.Attributes.EditorAddons.Drawers.DrawInspector;
using Better.EditorTools.Drawers.Base;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.WrapperCollections
{
    public class DrawInspectors : WrapperCollection<DrawInspectorWrapper>
    {
        public void OnGUI(SerializedProperty serializedProperty)
        {
            if (TryGetValue(serializedProperty, out var collectionValue))
            {
                collectionValue.Wrapper.OnGUI();
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

        public void SetObjectFromProperty(SerializedProperty property)
        {
            if (TryGetValue(property, out var wrapper))
            {
                wrapper.Wrapper.CreateEditor(property.objectReferenceValue);
            }
        }
    }
}