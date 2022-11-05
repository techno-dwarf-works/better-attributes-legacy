using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.Runtime.Attributes.Rename;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Rename
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