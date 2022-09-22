using System;
using System.Collections.Generic;
using System.Linq;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Helpers;
using BetterAttributes.Runtime.Attributes.Select;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BetterAttributes.EditorAddons.Drawers.Select
{
    public class SelectDrawerBase<T> : FieldDrawer where T : SelectAttributeBase
    {
        private protected List<Type> _reflectionType;
        private Type _type;
        private bool _needUpdate;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private protected void LazyGetAllInheritedType(Type baseType, Type currentObjectType)
        {
            if (_reflectionType != null) return;

            _reflectionType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p != currentObjectType && CheckType(baseType, p) &&
                            (p.IsClass || p.IsValueType) &&
                            !p.IsAbstract && !p.IsSubclassOf(typeof(Object)))
                .ToList();
            _reflectionType.Insert(0, null);
        }

        private bool CheckType(Type baseType, Type p)
        {
            return baseType.IsAssignableFrom(p);
        }

        private IEnumerable<Type> GetDirectInterfaces(Type type)
        {
            var allInterfaces = new List<Type>();
            var childInterfaces = new List<Type>();

            foreach (var i in type.GetInterfaces())
            {
                allInterfaces.Add(i);
                childInterfaces.AddRange(i.GetInterfaces());
            }

            var directInterfaces = allInterfaces.Except(childInterfaces);

            return directInterfaces;
        }

        private protected Rect GetPopupPosition(Rect currentPosition)
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
                var t = property.serializedObject.targetObject.GetType();
                LazyGetAllInheritedType(att.GetFieldType() ?? fieldInfo.FieldType, t);
                var popupPosition = GetPopupPosition(position);
                
                var referenceValue = property.managedReferenceValue;
                DrawEnumButton(popupPosition, att.DisplayName, referenceValue);

                if (_needUpdate)
                {
                    property.managedReferenceValue = _type == null ? null : Activator.CreateInstance(_type);
                    _needUpdate = false;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return true;
        }

        private void DrawEnumButton(Rect popupPosition, DisplayName displayName, object referenceValue)
        {
            var style = DrawersHelper.GetButtonStyle();
            var content = DrawersHelper.GetIconGUIContent(IconType.GrayDropdown);
            content.text = referenceValue == null ? "null" : referenceValue.GetType().Name;
            if (GUI.Button(popupPosition, content, style))
            {
                var copy = popupPosition;
                copy.y += EditorGUIUtility.singleLineHeight;
                var popup = DropDownPopup.ShowWindow(GUIUtility.GUIToScreenRect(copy));
                foreach (var type in _reflectionType)
                {
                    var guiContent = new GUIContent(ResolveName(type, displayName));
                    popup.AddItem(guiContent, ResolveState(referenceValue, type), OnSelectItem, type);
                }
            }
        }

        private string ResolveName(Type type, DisplayName displayName)
        {
            switch (displayName)
            {
                case DisplayName.Short:
                    return type == null ? "null" : $"{type.Name}";
                case DisplayName.Full:
                    return type == null ? "null" : $"{type.FullName}";
                case DisplayName.Extended:
                    return type == null ? "null" : $"{type.Name} : {type}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(displayName), displayName, null);
            }
        }

        private void OnSelectItem(object obj)
        {
            if (obj == null)
            {
                _type = null;
                _needUpdate = true;
                return;
            }

            var type = (Type)obj;
            if (_type != null)
            {
                if (_type == type)
                {
                    return;
                }
            }

            _type = type;
            _needUpdate = true;
        }

        private bool ResolveState(object value, Type type)
        {
            if (type == null && value == null) return true;
            return value?.GetType() == type;
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