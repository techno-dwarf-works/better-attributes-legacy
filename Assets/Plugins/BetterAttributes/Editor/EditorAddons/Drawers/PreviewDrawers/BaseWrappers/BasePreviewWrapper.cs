using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Helpers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    public abstract class BasePreviewWrapper : UtilityWrapper
    {
        public abstract void OnGUI(Rect position, SerializedProperty serializedProperty, float size);

        private protected virtual bool ValidateObject(Object drawnObject)
        {
            var value = drawnObject != null;
            if (!value)
            {
                DrawersHelper.HelpBox("Preview not available for null", MessageType.Warning, false);
            }

            return value;
        }
    }
}