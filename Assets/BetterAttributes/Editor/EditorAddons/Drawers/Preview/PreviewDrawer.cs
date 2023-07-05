using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Preview;
using Better.EditorTools.Attributes;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using Better.Tools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Preview
{
    [MultiCustomPropertyDrawer(typeof(PreviewAttribute))]
    public class PreviewDrawer : MultiFieldDrawer<BasePreviewWrapper>
    {
        private protected bool _objectChanged;
        private float _previewSize;
        public PreviewWrappers Collection => _wrappers as PreviewWrappers;

        protected override void Deconstruct()
        {
            _wrappers.Deconstruct();
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = fieldInfo.FieldType;
            var attributeType = attribute.GetType();
            if (!PreviewUtility.Instance.IsSupported(fieldType))
            {
                DrawersHelper.NotSupportedAttribute(label.text, fieldType, attributeType);
                return false;
            }

            var cache = ValidateCachedProperties(property, PreviewUtility.Instance);
            if (!cache.IsValid)
            {
            }

            EditorGUI.BeginChangeCheck();
            _previewSize = ((PreviewAttribute)attribute).PreviewSize;
            if (!Collection.ValidateObject(property))
            {
                const string message = "Preview not available for empty field";
                var offset = DrawersHelper.GetHelpBoxHeight(position.width, message, IconType.WarningMessage);
                var buffer = new Rect(position);
                buffer.y += EditorGUI.GetPropertyHeight(property, label, false) + DrawersHelper.SpaceHeight;
                DrawersHelper.HelpBox(buffer, message, IconType.WarningMessage);

                position.height += offset + DrawersHelper.SpaceHeight * 2;
                return true;
            }

            label.image = DrawersHelper.GetIcon(IconType.View);
            var copy = DrawersHelper.GetClickRect(position, label);
            copy.height = EditorGUIUtility.singleLineHeight;

            Collection.PreDraw(copy, property, _previewSize, _objectChanged);

            return true;
        }

        protected override HeightCache GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Collection.ValidateObject(property))
            {
                const string message = "Preview not available for null";
                var additive = DrawersHelper.GetHelpBoxHeight(EditorGUIUtility.currentViewWidth, message, IconType.WarningMessage);
                return HeightCache.GetFull(EditorGUI.GetPropertyHeight(property, label, true) + additive + DrawersHelper.SpaceHeight * 2);
            }

            return HeightCache.GetAdditive(0);
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            _objectChanged = EditorGUI.EndChangeCheck();
        }

        protected override WrapperCollection<BasePreviewWrapper> GenerateCollection()
        {
            return new PreviewWrappers();
        }

        public PreviewDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }
    }
}