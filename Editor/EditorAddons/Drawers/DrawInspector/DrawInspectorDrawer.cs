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
            _wrappers?.Deconstruct();
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = GetFieldOrElementType();
            if (!DrawInspectorUtility.Instance.IsSupported(fieldType))
            {
                EditorGUI.BeginChangeCheck();
                DrawField(position, property, label);
                DrawersHelper.NotSupportedAttribute(label.text, fieldType, attribute.GetType(), false);
                return false;
            }

            if (!ValidateCachedProperties(property, DrawInspectorUtility.Instance))
            {
                Collection.SetProperty(property);
            }

            _isOpen = Collection.IsOpen(property);
            if (property.objectReferenceValue)
            {
                label.image = DrawersHelper.GetIcon(_isOpen ? IconType.Minus : IconType.PlusMore);
            }

            var copy = DrawersHelper.GetClickRect(position, label);
            copy.height = EditorGUIUtility.singleLineHeight;
            if (DrawersHelper.IsClickedAt(copy))
            {
                Collection.SetOpen(property, !_isOpen);
            }

            return true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var additionalHeight = 0f;
            if (Collection != null && Collection.IsOpen(property))
            {
                additionalHeight = Collection.GetHeight(property);
            }

            var propertyHeight = EditorGUI.GetPropertyHeight(property, label) + additionalHeight;
            return propertyHeight;
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            Collection.OnGUI(property, position);
        }

        protected override WrapperCollection<DrawInspectorWrapper> GenerateCollection()
        {
            return new DrawInspectors();
        }
    }
}