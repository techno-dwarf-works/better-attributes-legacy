using System;
using System.Collections.Generic;
using System.Reflection;
using Better.Attributes.Runtime;
using Better.Attributes.Runtime.Select;
using Better.Commons.EditorAddons.Drawers;
using Better.Commons.EditorAddons.Drawers.Container;
using Better.Commons.EditorAddons.Drawers.Handlers;
using Better.Commons.EditorAddons.DropDown;
using Better.Commons.EditorAddons.Enums;
using Better.Commons.EditorAddons.Extensions;
using Better.Commons.EditorAddons.Utility;
using Better.Commons.Runtime.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace Better.Attributes.EditorAddons.Drawers.Select
{
    public abstract class BaseSelectHandler : SerializedPropertyHandler
    {
        protected FieldInfo _fieldInfo;
        protected BaseSelectAttribute _attribute;
        protected object _propertyContainer;
        protected object _currentValue;
        protected ElementsContainer _container;
        
        public bool IsValid => _container != null && _fieldInfo != null && _attribute != null;

        public void Setup(ElementsContainer container, FieldInfo fieldInfo, BaseSelectAttribute attribute)
        {
            _container = container;
            _fieldInfo = fieldInfo;
            _attribute = attribute;
            _propertyContainer = _container.SerializedProperty.GetLastNonCollectionContainer();
            _currentValue = GetCurrentValue();
            OnSetup();
        }

        protected abstract void OnSetup();

        public abstract object GetCurrentValue();

        protected abstract List<object> GetObjects();

        protected abstract GUIContent GetResolvedName(object value, DisplayName displayName);
        protected abstract IEnumerable<GUIContent> GetResolvedGroupedName(object value, DisplayGrouping grouping);
        public abstract string GetButtonText();
        protected abstract bool ResolveState(object iteratedValue);
        public abstract bool ValidateSelected(object item);
        public abstract bool CheckSupported();
        public abstract GUIContent GenerateHeader();
        public abstract bool IsSkippingFieldDraw();
        
        public DropdownCollection GenerateItemsTree()
        {
            var items = new DropdownCollection(new DropdownSubTree(new GUIContent(LabelDefines.Root)));
            var displayGrouping = _attribute.DisplayGrouping;
            var displayName = _attribute.DisplayName;
            var selectionObjects = GetObjects();
            if (displayGrouping == DisplayGrouping.None)
            {
                foreach (var value in selectionObjects)
                {
                    var guiContent = GetResolvedName(value, displayName);
                    if (guiContent == null)
                    {
                        continue;
                    }

                    if (guiContent.image == null && ResolveState(value))
                    {
                        guiContent.image = IconType.Checkmark.GetIcon();
                    }

                    var item = new DropdownItem(guiContent, OnSelectItem, value);
                    items.AddChild(item);
                }
            }
            else
            {
                foreach (var type in selectionObjects)
                {
                    var resolveGroupedName = GetResolvedGroupedName(type, displayGrouping);
                    items.AddItem(resolveGroupedName, ResolveState(type), OnSelectItem, type);
                }
            }

            return items;
        }
        
        private void OnSelectItem(object obj)
        {
            if (!ValidateSelected(obj))
            {
                return;
            }

            if (obj == null)
            {
                Update(null);
                return;
            }

            if (_currentValue != default)
            {
                if (_currentValue.Equals(obj))
                {
                    return;
                }
            }

            Update(obj);
        }

        public virtual void Update(object value)
        {
            _currentValue = value;
            
            var property = _container.SerializedProperty;
            property.serializedObject.ApplyModifiedProperties();
        }

        public virtual Type GetFieldOrElementType()
        {
            var t = _attribute.GetFieldType();
            if (t != null)
            {
                return t;
            }

            var fieldType = _fieldInfo.FieldType;
            if (fieldType.IsArrayOrList())
                return fieldType.GetCollectionElementType();
            return fieldType;
        }

        public override void Deconstruct()
        {
        }

        public virtual void OnPopulateContainer()
        {
            if (IsSkippingFieldDraw())
            {
                var label = VisualElementUtility.CreateLabelFor(_container.SerializedProperty);
                _container.RootElement.Insert(0, label);
                label.style.Width(StyleDefinition.LabelWidthStyle);
                label.SendToBack();
                label.AddToClassList("field-decoration");
            }
        }
    }
}