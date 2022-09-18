using BetterAttributes.EditorAddons.Drawers.Utilities;
using BetterAttributes.EditorAddons.Helpers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BetterAttributes.EditorAddons.Drawers.Preview
{
    public abstract class BasePreviewWrapper : UtilityWrapper
    {
        public abstract void OnGUI(Rect position, SerializedProperty serializedProperty, float size);

        private protected virtual bool ValidateObject(Object drawnObject)
        {
            var value = drawnObject != null;
            if (!value)
            {
                DrawersHelper.HelpBox("Preview not available for null", IconType.WarningMessage, false);
            }

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