﻿using System;
using Better.EditorTools;
using Better.EditorTools.Drawers.Base;
using UnityEditor;

namespace Better.Attributes.EditorAddons.Drawers.Select.Wrappers
{
    public class SelectTypeWrapper : BaseSelectWrapper
    {
        public override bool SkipFieldDraw()
        {
            return false;
        }

        public override HeightCache GetHeight()
        {
            return HeightCache.GetFull(EditorGUI.GetPropertyHeight(_property, true));
        }

        public override void Update(object value)
        {
            if (!_property.Verify()) return;
            var typeValue = (Type)value;
            _property.managedReferenceValue = typeValue == null ? null : Activator.CreateInstance(typeValue);
        }

        public override object GetCurrentValue()
        {
            return _property.GetManagedType();
        }
    }
}