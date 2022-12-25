using Better.Attributes.EditorAddons.Drawers.Base;
using Better.Attributes.Runtime.Rename;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Rename
{
    [CustomPropertyDrawer(typeof(RenameFieldAttribute))]
    public class RenameFieldDrawer : FieldDrawer
    {
        private protected override void Deconstruct()
        {
        }

        private protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var rename = (attribute as RenameFieldAttribute)?.Name;
            label.text = rename;
            return true;
        }

        private protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        private protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
        }
    }
}