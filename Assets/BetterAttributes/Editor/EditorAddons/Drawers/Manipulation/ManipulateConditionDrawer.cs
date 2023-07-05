using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Manipulation.Wrappers;
using Better.Attributes.Runtime.Manipulation;
using Better.EditorTools.Attributes;
using Better.EditorTools.Drawers.Base;
using Better.Extensions.Runtime;
using Better.Tools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Manipulation
{

    [MultiCustomPropertyDrawer(typeof(ManipulateConditionAttribute))]
    public class ManipulateConditionDrawer : MultiFieldDrawer<ManipulateConditionWrapper>
    {
        public ManipulateConditionDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            _wrappers ??= GenerateCollection();
            var wrapper = GetWrapper(property);
            
            return wrapper.PreDraw();
        }

        private ManipulateConditionWrapper GetWrapper(SerializedProperty property)
        {
            ManipulateConditionWrapper wrapper = null;
            if (!_wrappers.TryGetValue(property, out var value))
            {
                wrapper = new ManipulateConditionWrapper();
                wrapper.SetProperty(property, (ManipulateConditionAttribute)attribute);
                value = new WrapperCollectionValue<ManipulateConditionWrapper>(wrapper, fieldInfo.GetFieldOrElementType());
                _wrappers.Add(property, value);
            }

            wrapper = value.Wrapper;
            return wrapper;
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override HeightCache GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var wrapper = GetWrapper(property);
            return wrapper.GetHeight();
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            var wrapper = GetWrapper(property);
            wrapper.PostDraw();
        }

        protected override WrapperCollection<ManipulateConditionWrapper> GenerateCollection()
        {
            return new WrapperCollection<ManipulateConditionWrapper>();
        }

    }
}