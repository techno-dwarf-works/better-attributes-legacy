using System;
using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Helpers;
using BetterAttributes.Runtime.Attributes.Select;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    public abstract class SelectDrawerBase<T> : FieldDrawer where T : SelectAttributeBase
    {
        private bool _needUpdate;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
        
        private Rect GetPopupPosition(Rect currentPosition)
        {
            var popupPosition = new Rect(currentPosition);
            popupPosition.width -= EditorGUIUtility.labelWidth;
            popupPosition.x += EditorGUIUtility.labelWidth;
            popupPosition.height = EditorGUIUtility.singleLineHeight;
            return popupPosition;
        }


        private protected override void Deconstruct()
        {
            DropDownPopup.CloseInstance();
        }

        private protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                if (property.propertyType != SerializedPropertyType.ManagedReference) return false;
                var att = (T)attribute;
                var popupPosition = GetPopupPosition(position);
                Setup(property, att);
                var referenceValue = GetValue(property);
                if (DrawEnumButton(popupPosition, referenceValue))
                {
                    ShowDropDown(popupPosition, att.DisplayName, att.DisplayGrouping, referenceValue);
                }

                if (_needUpdate)
                {
                    UpdateValue(property);
                    _needUpdate = false;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return true;
        }

        

        private object GetValue(SerializedProperty property)
        {
            return property.managedReferenceValue;
        }

        private bool DrawEnumButton(Rect buttonPosition, object currentValue)
        {
            var content = DrawersHelper.GetIconGUIContent(IconType.GrayDropdown);

            content.text = currentValue == null ? "null" : currentValue.GetType().Name;
            return GUI.Button(buttonPosition, content, Styles.Button);
        }
 
        private void ShowDropDown(Rect popupPosition, DisplayName displayName, DisplayGrouping displayGrouping,
            object currentValue)
        {
            var copy = popupPosition;
            copy.y += EditorGUIUtility.singleLineHeight;
            var popup = DropDownPopup.ShowWindow(GUIUtility.GUIToScreenRect(copy));
            var items = GenerateItemsTree(displayName, displayGrouping, currentValue);

            popup.SetItems(items);
        }

        private protected virtual DropDownCollection GenerateItemsTree(DisplayName displayName, DisplayGrouping displayGrouping, object currentValue)
        {
            var items = new DropDownCollection(new DropDownSubTree(new GUIContent("Root")));
            var collection = GetSelectCollection();
            if (displayGrouping == DisplayGrouping.None)
            {
                foreach (var type in collection)
                {
                    var guiContent = new GUIContent(ResolveName(type, displayName));
                    var item = new DropDownItem(guiContent, ResolveState(currentValue, type), OnSelectItem, type);
                    items.AddChild(item);
                }
            }
            else
            {
                foreach (var type in collection)
                {
                    var resolveGroupedName = ResolveGroupedName(type, displayGrouping);
                    items.AddItem(resolveGroupedName, ResolveState(currentValue, type), OnSelectItem,
                        type);
                }
            }

            return items;
        }
        
        private protected abstract void Setup(SerializedProperty property, T currentAttribute);

        private protected abstract void UpdateValue(SerializedProperty property);

        private protected abstract List<object> GetSelectCollection();

        private protected abstract string ResolveName(object value, DisplayName displayName);

        private protected abstract string[] ResolveGroupedName(object value, DisplayGrouping grouping);

        private protected abstract bool ResolveState(object currentValue, object iteratedValue);

        private protected abstract void OnSelectItem(object obj);

        private protected void SetNeedUpdate()
        {
            _needUpdate = true;
        }

        private protected override Rect PreparePropertyRect(Rect original)
        {
            return original;
        }

        private protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
        }
    }
}