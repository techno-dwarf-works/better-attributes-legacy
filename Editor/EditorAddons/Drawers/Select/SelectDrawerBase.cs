﻿using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Select.Wrappers;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.Runtime.Select;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using Better.EditorTools.Helpers.DropDown;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    public abstract class SelectDrawerBase<TAttribute, TWrapper> : MultiFieldDrawer<TWrapper>
        where TAttribute : SelectAttributeBase where TWrapper : BaseSelectWrapper
    {
        private bool _needUpdate;
        private bool _isSetUp;
        private DisplayName _displayName;
        private DisplayGrouping _displayGrouping;

        private protected const string NotSupported = "Not supported";

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        protected override Type GetFieldType()
        {
            return (attribute as SelectAttributeBase)?.GetFieldType() ?? base.GetFieldType();
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                var att = (TAttribute)attribute;
                if (!CheckSupported(property))
                {
                    DrawersHelper.NotSupportedAttribute(property.displayName, fieldInfo.FieldType, attribute.GetType());
                    return false;
                }

                if (!ValidateCachedProperties(property, SelectUtility.Instance))
                {
                    _wrappers[property].Wrapper.SetProperty(property);
                }

                var popupPosition = GetPopupPosition(position);
                if (!_isSetUp)
                {
                    Setup(property, att);
                    _displayName = att.DisplayName;
                    _displayGrouping = att.DisplayGrouping;
                    SetReady();
                }

                var referenceValue = GetCurrentValue(property);
                if (DrawButton(popupPosition, referenceValue))
                {
                    ShowDropDown(property, popupPosition, referenceValue);
                }

                if (_needUpdate)
                {
                    UpdateValue(property);
                    _needUpdate = false;
                    AfterValueUpdated(property);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return true;
        }

        private Rect GetPopupPosition(Rect currentPosition)
        {
            var popupPosition = new Rect(currentPosition);
            popupPosition.width -= EditorGUIUtility.labelWidth;
            popupPosition.x += EditorGUIUtility.labelWidth;
            popupPosition.height = EditorGUIUtility.singleLineHeight;
            return popupPosition;
        }

        protected override void Deconstruct()
        {
            DropdownWindow.CloseInstance();
            _wrappers?.Deconstruct();
        }

        private bool DrawButton(Rect buttonPosition, object currentValue)
        {
            var content = DrawersHelper.GetIconGUIContent(IconType.GrayDropdown);

            content.text = GetButtonName(currentValue);
            return GUI.Button(buttonPosition, content, Styles.Button);
        }

        private void ShowDropDown(SerializedProperty serializedProperty, Rect popupPosition, object currentValue)
        {
            var copy = popupPosition;
            copy.y += EditorGUIUtility.singleLineHeight;
            var popup = DropdownWindow.ShowWindow(GUIUtility.GUIToScreenRect(copy), GenerateHeader());
            var items = GenerateItemsTree(serializedProperty, currentValue);

            popup.SetItems(items);
        }

        private protected virtual DropdownCollection GenerateItemsTree(SerializedProperty serializedProperty,
            object currentValue)
        {
            var items = new DropdownCollection(new DropdownSubTree(new GUIContent("Root")));
            var collection = GetSelectCollection();
            if (_displayGrouping == DisplayGrouping.None)
            {
                foreach (var type in collection)
                {
                    var guiContent = ResolveName(type, _displayName);
                    if (guiContent.image == null && ResolveState(currentValue, type))
                    {
                        guiContent.image = DrawersHelper.GetIcon(IconType.Checkmark);
                    }

                    var item = new DropdownItem(guiContent, OnSelectItem,
                        new SelectedItem<object>(serializedProperty, type));
                    items.AddChild(item);
                }
            }
            else
            {
                foreach (var type in collection)
                {
                    var resolveGroupedName = ResolveGroupedName(type, _displayGrouping);
                    items.AddItem(resolveGroupedName, ResolveState(currentValue, type), OnSelectItem,
                        new SelectedItem<object>(serializedProperty, type));
                }
            }

            return items;
        }

        private protected abstract GUIContent GenerateHeader();
        private protected abstract object GetCurrentValue(SerializedProperty property);
        private protected abstract bool CheckSupported(SerializedProperty property);
        private protected abstract string GetButtonName(object currentValue);
        private protected abstract void Setup(SerializedProperty property, TAttribute currentAttribute);
        private protected abstract List<object> GetSelectCollection();
        private protected abstract GUIContent ResolveName(object value, DisplayName displayName);
        private protected abstract GUIContent[] ResolveGroupedName(object value, DisplayGrouping grouping);
        private protected abstract bool ResolveState(object currentValue, object iteratedValue);
        private protected abstract void OnSelectItem(object obj);
        private protected abstract void UpdateValue(SerializedProperty property);
        private protected abstract void AfterValueUpdated(SerializedProperty property);

        private protected void SetNeedUpdate()
        {
            _needUpdate = true;
        }

        private void SetReady()
        {
            _isSetUp = true;
        }

        protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
        }
    }
}