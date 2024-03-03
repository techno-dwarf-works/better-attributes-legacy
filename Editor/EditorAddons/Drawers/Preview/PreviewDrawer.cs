using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Preview;
using Better.EditorTools.EditorAddons.Attributes;
using Better.EditorTools.EditorAddons.Drawers.Base;
using Better.EditorTools.EditorAddons.Helpers;
using Better.EditorTools.Runtime.Attributes;
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

        private const string Message = "Preview not available for empty field";

        protected override void Deconstruct()
        {
            _wrappers.Deconstruct();
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = _fieldInfo.FieldType;
            var attributeType = _attribute.GetType();
            if (!PreviewUtility.Instance.IsSupported(fieldType))
            {
                DrawersHelper.NotSupportedAttribute(position, property, label, fieldType, attributeType);
                return false;
            }

            var cache = ValidateCachedProperties(property, PreviewUtility.Instance);
            if (!cache.IsValid)
            {
            }

            EditorGUI.BeginChangeCheck();
            _previewSize = ((PreviewAttribute)_attribute).PreviewSize;
            if (!Collection.ValidateObject(property))
            {
                DrawersHelper.HelpBoxFromRect(position, property, label, Message, IconType.WarningMessage);
                return true;
            }

            label.image = DrawersHelper.GetIcon(IconType.View);
            var copy = DrawersHelper.GetClickRect(position, label);
            copy.height = EditorGUIUtility.singleLineHeight;

            Collection.PreDraw(copy, property, _previewSize, _objectChanged);

            return true;
        }

        protected override HeightCacheValue GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Collection.ValidateObject(property))
            {
                var additive = DrawersHelper.GetHelpBoxHeight(EditorGUIUtility.currentViewWidth, Message, IconType.WarningMessage);
                return HeightCacheValue.GetAdditive(additive + DrawersHelper.SpaceHeight * 2);
            }

            return HeightCacheValue.GetAdditive(0);
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