using System;
using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.DrawInspector;
using Better.EditorTools.Attributes;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using Better.Tools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.DrawInspector
{
    [MultiCustomPropertyDrawer(typeof(DrawInspectorAttribute))]
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
                DrawersHelper.NotSupportedAttribute(position, property, label, fieldType, _attribute.GetType());
                return false;
            }

            var cache = ValidateCachedProperties(property, DrawInspectorUtility.Instance);
            if (!cache.IsValid)
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

        protected override HeightCache GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var additionalHeight = 0f;
            if (Collection != null && Collection.IsOpen(property))
            {
                additionalHeight = Collection.GetHeight(property);
            }

            return HeightCache.GetAdditive(additionalHeight);
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            Collection.PostDraw(property, position);
        }

        protected override WrapperCollection<DrawInspectorWrapper> GenerateCollection()
        {
            return new DrawInspectors();
        }

        public DrawInspectorDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }
    }
}