using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Manipulation.Wrappers;
using Better.Attributes.EditorAddons.Drawers.Misc.Wrappers;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.Runtime.Manipulation;
using Better.Attributes.Runtime.Misc;
using Better.EditorTools.Attributes;
using Better.EditorTools.Drawers.Base;
using Better.Tools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Misc
{
    [MultiCustomPropertyDrawer(typeof(MiscAttribute))]
    public class MiscDrawer : MultiFieldDrawer<MiscWrapper>
    {
        private MiscDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }

        private MiscWrapper GetWrapper(SerializedProperty property)
        {
            var cache = ValidateCachedProperties(property, MiscUtility.Instance);
            if (!cache.IsValid)
            {
                cache.Value.Wrapper.SetProperty(property, _fieldInfo, (MiscAttribute)_attribute);
            }

            return cache.Value.Wrapper;
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            _wrappers ??= GenerateCollection();
            var wrapper = GetWrapper(property); 
            wrapper.PreDraw(ref position, label);
            
            return true;
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            var wrapper = GetWrapper(property);
            wrapper.DrawField(position, label);
        }

        protected override HeightCache GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var wrapper = GetWrapper(property);
            return wrapper.GetHeight(label);
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            var wrapper = GetWrapper(property);
            wrapper.PostDraw();
        }

        protected override WrapperCollection<MiscWrapper> GenerateCollection()
        {
            return new WrapperCollection<MiscWrapper>();
        }
    }
}