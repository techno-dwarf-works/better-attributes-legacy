using System.Reflection;
using Better.Attributes.Runtime.ReadOnly;
using Better.EditorTools.Attributes;
using Better.EditorTools.Drawers.Base;
using Better.Tools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.ReadOnly
{
    [MultiCustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
    public class ReadOnlyFieldDrawer : FieldDrawer
    {
        protected override void Deconstruct()
        {
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            return true;
        }

        protected override Rect PreparePropertyRect(Rect position)
        {
            return position;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.EndDisabledGroup();
        }

        public ReadOnlyFieldDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }
    }
}