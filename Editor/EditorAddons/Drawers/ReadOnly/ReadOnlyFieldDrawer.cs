using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.Runtime.Attributes.ReadOnly;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.ReadOnly
{
    [CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
    public class ReadOnlyFieldDrawer : FieldDrawer
    {
        private protected override void Deconstruct()
        {
            
        }

        private protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            return true;
        }

        private protected override Rect PreparePropertyRect(Rect position)
        {
            return position;
        }

        private protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.EndDisabledGroup();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}