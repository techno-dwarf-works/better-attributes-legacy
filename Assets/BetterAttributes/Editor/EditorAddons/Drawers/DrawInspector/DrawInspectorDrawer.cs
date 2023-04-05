using System;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.DrawInspector;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.DrawInspector
{
    [CustomPropertyDrawer(typeof(DrawInspectorAttribute))]
    public class DrawInspectorDrawer : MultiFieldDrawer<DrawInspectorWrapper>
    {
        private bool _isOpen;
        private DrawInspectors Collection => _wrappers as DrawInspectors;

        protected override void Deconstruct()
        {
            _wrappers.Deconstruct();
        }

        protected override Type GetFieldOrElementType()
        {
            return fieldInfo.FieldType;
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = GetFieldOrElementType();
            if (fieldType.IsArray || !DrawInspectorUtility.Instance.IsSupported(fieldType))
            {
                EditorGUI.BeginChangeCheck();
                DrawField(position, property, label);
                DrawersHelper.NotSupportedAttribute(label.text, fieldType, attribute.GetType(), false);
                return false;
            }

            if (!ValidateCachedProperties(property, DrawInspectorUtility.Instance))
            {
                Collection.SetObjectFromProperty(property);
            }

            _isOpen = Collection.IsOpen(property);
            if (property.objectReferenceValue)
            {
                label.image = DrawersHelper.GetIcon(_isOpen ? IconType.Minus : IconType.PlusMore);
            }

            if (DrawersHelper.IsClickedAt(DrawersHelper.GetClickRect(position, label)))
            {
                Collection.SetOpen(property, !_isOpen);
            }

            return true;
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            Collection.OnGUI(property);
        }

        protected override WrapperCollection<DrawInspectorWrapper> GenerateCollection()
        {
            return new DrawInspectors();
        }
    }
}