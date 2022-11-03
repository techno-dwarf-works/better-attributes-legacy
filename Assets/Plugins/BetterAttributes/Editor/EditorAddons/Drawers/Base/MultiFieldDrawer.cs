using System;
using BetterAttributes.EditorAddons.Drawers.Utilities;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public abstract class MultiFieldDrawer<T> : FieldDrawer where T : UtilityWrapper
    {
        private protected WrapperCollection<T> _wrappers;

        /// <summary>
        /// Method generates explicit typed collection inherited from <see cref="BetterAttributes.EditorAddons.Drawers.Base.WrapperCollection"/> 
        /// </summary>
        /// <returns></returns>
        private protected abstract WrapperCollection<T> GenerateCollection();

        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _wrappers ??= GenerateCollection();
            base.OnGUI(position, property, label);
        }

        private protected virtual Type GetFieldType()
        {
            return fieldInfo.FieldType;
        }
        
        /// <summary>
        /// Validates if <see cref="_wrappers"/> contains property by <see cref="BetterAttributes.EditorAddons.Drawers.Comparers.SerializedPropertyComparer"/>
        /// </summary>
        /// <param name="property">SerializedProperty what will be stored into <see cref="_wrappers"/></param>
        /// <param name="handler"><see cref="BetterAttributes.EditorAddons.Drawers.Utilities.BaseUtility"/> used to validate current stored wrappers and gets instance for recently added property</param>
        /// <typeparam name="THandler"></typeparam>
        /// <returns>Returns true if wrapper for <paramref name="property"/> was already stored into <see cref="_wrappers"/></returns>
        private protected bool ValidateCachedProperties<THandler>(SerializedProperty property, BaseUtility<THandler> handler) where THandler : new()
        {
            var fieldType = GetFieldType();
            var contains = _wrappers.ContainsKey(property);
            if (contains)
            {
                handler.ValidateCachedProperties(_wrappers);
            }
            else
            {
                var gizmoWrapper = handler.GetUtilityWrapper<T>(fieldType, attribute.GetType());
                _wrappers.Add(property, new WrapperCollectionValue<T>(gizmoWrapper, fieldType));
            }

            return contains;
        }
    }
}