using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Utility;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Preview;
using Better.Commons.EditorAddons.Drawers.Attributes;
using Better.Commons.EditorAddons.Drawers.Base;
using Better.Commons.EditorAddons.Drawers.Caching;
using Better.Commons.EditorAddons.Enums;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.EditorAddons.Utility;
using Better.Commons.Runtime.Drawers.Attributes;
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
                ExtendedGUIUtility.NotSupportedAttribute(position, property, label, fieldType, attributeType);
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
                ExtendedGUIUtility.HelpBoxFromRect(position, property, label, Message, IconType.WarningMessage);
                return true;
            }

            label.image = IconType.View.GetIcon();
            var copy = ExtendedGUIUtility.GetClickRect(position, label);
            copy.height = EditorGUIUtility.singleLineHeight;

            Collection.PreDraw(copy, property, _previewSize, _objectChanged);

            return true;
        }

        protected override HeightCacheValue GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Collection.ValidateObject(property))
            {
                var additive = ExtendedGUIUtility.GetHelpBoxHeight(EditorGUIUtility.currentViewWidth, Message, IconType.WarningMessage);
                return HeightCacheValue.GetAdditive(additive + ExtendedGUIUtility.SpaceHeight * 2);
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