using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Select;
using Better.EditorTools.Drawers.Base;
using UnityEditor;
using UnityEngine;

#pragma warning disable CS0618

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    [CustomPropertyDrawer(typeof(SelectAttribute))]
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectDrawer : SelectDrawerBase<SelectAttributeBase>
    {
        protected override bool CheckSupported(SerializedProperty property)
        {
            return SelectUtility.Instance.CheckSupportedType(property.propertyType);
        }

        protected override WrapperCollection<BaseSelectWrapper> GenerateCollection()
        {
            return new SelectWrappers();
        }
        
        protected override void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_wrappers.TryGetValue(property, out var value) && value.Wrapper.SkipFieldDraw())
            {
                var rect = PreparePropertyRect(position);
                rect.height = value.Wrapper.GetHeight();
                EditorGUI.LabelField(rect, label);
                return;
            }

            base.DrawField(position, property, label);
        }
    }
}