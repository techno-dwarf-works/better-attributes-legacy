using Better.Commons.EditorAddons.Drawers.Utility;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Better.Attributes.EditorAddons.Drawers.Preview
{
    public abstract class BasePreviewWrapper : UtilityWrapper
    {
        public abstract void PreDraw(Rect position, SerializedProperty serializedProperty, float size);
        
        public bool ValidateObject(SerializedProperty serializedProperty)
        {
            return ValidateObject(serializedProperty.objectReferenceValue);
        }
        
        private protected virtual bool ValidateObject(Object drawnObject)
        {
            var value = drawnObject != null;
            return value;
        }

        public virtual void IsObjectUpdated(bool objectChanged)
        {
            if (objectChanged)
            {
                Deconstruct();
            }
        }
    }
}