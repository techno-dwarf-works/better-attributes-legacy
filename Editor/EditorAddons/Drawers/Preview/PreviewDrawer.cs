using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Preview;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Preview
{
    [CustomPropertyDrawer(typeof(PreviewAttribute))]
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
            label.image = DrawersHelper.GetIcon(IconType.View);
            var copy = DrawersHelper.GetClickRect(position, label);
            copy.height = EditorGUIUtility.singleLineHeight;
            Collection.OnGUI(copy, property, _previewSize, _objectChanged);

            return true;
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            _objectChanged = EditorGUI.EndChangeCheck();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        protected override WrapperCollection<BasePreviewWrapper> GenerateCollection()
        {
            return new PreviewWrappers();
        }
    }
}