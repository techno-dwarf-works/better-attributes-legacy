using BetterAttributes.EditorAddons.Drawers.Utilities;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public abstract class MultiFieldDrawer<T> : FieldDrawer where T : UtilityWrapper
    {
        private protected WrapperCollection<T> _wrappers;

        private protected abstract WrapperCollection<T> GenerateCollection();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _wrappers ??= GenerateCollection();
            base.OnGUI(position, property, label);
        }

        private protected bool ValidateCachedProperties<THandler>(SerializedProperty property, BaseUtility<THandler> handler) where THandler : new()
        {
            var fieldType = fieldInfo.FieldType;
            var contains = _wrappers.ContainsKey(property);
            if (contains)
            {
                handler.ValidateCachedProperties(_wrappers);
            }
            else
            {
                var gizmoWrapper = handler.GetUtilityWrapper<T>(fieldType, attribute.GetType());
                _wrappers.Add(property, (gizmoWrapper, fieldType));
            }

            return contains;
        }
    }
}