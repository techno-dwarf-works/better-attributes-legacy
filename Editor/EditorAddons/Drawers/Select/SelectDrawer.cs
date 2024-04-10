using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Select;
using Better.Commons.EditorAddons.Drawers.Attributes;
using Better.Commons.EditorAddons.Drawers.Base;
using Better.Commons.Runtime.Drawers.Attributes;
using UnityEditor;
using UnityEngine;

#pragma warning disable CS0618

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    [MultiCustomPropertyDrawer(typeof(SelectAttribute))]
    [MultiCustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    [MultiCustomPropertyDrawer(typeof(DropdownAttribute))]
    public class SelectDrawer : SelectDrawerBase<SelectAttributeBase>
    {
        protected override WrapperCollection<BaseSelectWrapper> GenerateCollection()
        {
            return new SelectWrappers();
        }
        
        protected override void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_wrappers.TryGetValue(property, out var value) && value.Wrapper.SkipFieldDraw())
            {
                var rect = PreparePropertyRect(position);
                // rect.height = value.Wrapper.GetHeight().Value;
                EditorGUI.LabelField(rect, label);
                return;
            }

            base.DrawField(position, property, label);
        }

        public SelectDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }
    }
}