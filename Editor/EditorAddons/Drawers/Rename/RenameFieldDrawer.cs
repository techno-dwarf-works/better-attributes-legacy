using System.Reflection;
using Better.Attributes.Runtime.Rename;
using Better.EditorTools.Attributes;
using Better.EditorTools.Drawers.Base;
using Better.Tools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Rename
{
    [MultiCustomPropertyDrawer(typeof(RenameFieldAttribute))]
    public class RenameFieldDrawer : FieldDrawer
    {
        protected override void Deconstruct()
        {
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var rename = (_attribute as RenameFieldAttribute)?.Name;
            label.text = rename;
            return true;
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
        }

        public RenameFieldDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }
    }
}