using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Helpers;
using BetterAttributes.Runtime.PreviewAttributes;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
{
    [CustomPropertyDrawer(typeof(PreviewAttribute))]
    [CustomPropertyDrawer(typeof(PopupPreviewAttribute))]
    public class PreviewDrawer : BaseMultiFieldDrawer<BasePreviewWrapper>
    {
        public PreviewWrapperCollection Collection => _wrappers as PreviewWrapperCollection;
        
        private protected override void Deconstruct()
        {
            EditorPopup.CloseInstance();
            _wrappers.Deconstruct();
        }

        private protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = fieldInfo.FieldType;
            var attributeType = attribute.GetType();
            if (!PreviewUtility.Instance.IsSupported(fieldType))
            {
                DrawersHelper.NotSupportedAttribute(label.text, fieldType, attributeType);
                return false;
            }

            if (!_wrappers.ContainsKey(property))
            {
                var wrapper =
                    PreviewUtility.Instance.GetUtilityWrapper<BasePreviewWrapper>(fieldType, attributeType);
                _wrappers.Add(property, (wrapper, fieldType));
            }

            Collection.OnGUI(position, property, ((PreviewAttribute)attribute).PreviewSize);

            return true;
        }

        private protected override Rect PreparePropertyRect(Rect position)
        {
            return position;
        }

        private protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private protected override WrapperCollection<BasePreviewWrapper> GenerateCollection()
        {
            return new PreviewWrapperCollection();
        }
    }
}