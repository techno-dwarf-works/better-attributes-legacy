using System;
using System.Collections.Generic;
using System.Linq;
using BetterAttributes.Runtime.SelectAttributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BetterAttributes.EditorAddons.Drawers.SelectDrawers
{
    public abstract class SelectDrawerBase<T> : PropertyDrawer where T : SelectAttributeBase
    {
        private protected bool _initializeFold;

        private protected List<Type> _reflectionType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                if (property.propertyType != SerializedPropertyType.ManagedReference) return;
                var att = (T)attribute;
                var t = property.serializedObject.targetObject.GetType();
                LazyGetAllInheritedType(att.GetFieldType() ?? fieldInfo.FieldType, t, att.FindTypesRecursively);
                var popupPosition = GetPopupPosition(position);

                var typePopupNameArray =
                    _reflectionType.Select(type => type == null ? "null" : $"{type.Name} : {type}").ToArray();

                var typeFullNameArray = _reflectionType
                    .Select(type => type == null
                        ? ""
                        : $"{type.Assembly.ToString().Split(',')[0]} {type.FullName}")
                    .ToArray();

                //Get the type of serialized object
                var currentTypeIndex = Array.IndexOf(typeFullNameArray, property.managedReferenceFullTypename);

                if (currentTypeIndex <= -1 || currentTypeIndex >= typeFullNameArray.Length)
                {
                    currentTypeIndex = 0;
                }

                var currentObjectType = _reflectionType[currentTypeIndex];
                var selectedTypeIndex = EditorGUI.Popup(popupPosition, currentTypeIndex, typePopupNameArray);
                ValidateType(property, selectedTypeIndex, currentObjectType);

                if (!_initializeFold)
                {
                    property.isExpanded = currentTypeIndex != 0;
                    _initializeFold = true;
                }

                EditorGUI.PropertyField(position, property, label, true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private protected virtual string GetManagedReferenceFullTypename(SerializedProperty property)
        {
            return property.managedReferenceFullTypename;
        }

        private protected abstract void ValidateType(SerializedProperty property, int selectedTypeIndex,
            Type currentObjectType);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private protected void LazyGetAllInheritedType(Type baseType, Type currentObjectType, bool findTypesRecursively)
        {
            if (_reflectionType != null) return;

            _reflectionType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p != currentObjectType && CheckType(baseType, p, findTypesRecursively) && (p.IsClass || p.IsValueType) &&
                            !p.IsAbstract && !p.IsSubclassOf(typeof(Object)))
                .ToList();
            _reflectionType.Insert(0, null);
        }

        private bool CheckType(Type baseType, Type p, bool findTypesRecursively)
        {
            return baseType.IsAssignableFrom(p); //TODO: temporary fix bug with serialize reference
            return findTypesRecursively ? baseType.IsAssignableFrom(p) : GetDirectInterfaces(p).Contains(baseType);
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
    }
}