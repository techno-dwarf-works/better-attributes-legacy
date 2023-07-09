﻿using System;
using Better.Attributes.Runtime.Manipulation;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using Better.EditorTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Manipulation.Wrappers
{
    public abstract class ManipulateWrapper : UtilityWrapper
    {
        protected SerializedProperty _property;
        protected ManipulateAttribute _attribute;
        private GUI.Scope _scope;

        public override void Deconstruct()
        {
            _scope?.Dispose();
        }

        protected abstract bool IsConditionSatisfied();

        public virtual bool PreDraw()
        {
            var satisfied = IsConditionSatisfied();

            switch (_attribute.ModeType)
            {
                case ManipulationMode.Show:
                    _scope = new HideGroup(!satisfied);
                    break;
                case ManipulationMode.Hide:
                    _scope = new HideGroup(satisfied);
                    break;
                case ManipulationMode.Disable:
                    _scope = new EditorGUI.DisabledGroupScope(satisfied);
                    break;
                case ManipulationMode.Enable:
                    _scope = new EditorGUI.DisabledGroupScope(!satisfied);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        public virtual HeightCache GetHeight()
        {
            var satisfied = IsConditionSatisfied();

            switch (_attribute.ModeType)
            {
                case ManipulationMode.Show:
                    var heightCache = new HeightCache(satisfied, 0);
                    return satisfied ? heightCache : heightCache.Force();
                case ManipulationMode.Hide:
                    var height = new HeightCache(!satisfied, 0);
                    return !satisfied ? height : height.Force();
                case ManipulationMode.Disable:
                case ManipulationMode.Enable:
                    return HeightCache.GetAdditive(0f);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual void SetProperty(SerializedProperty property, ManipulateAttribute attribute)
        {
            _property = property;
            _attribute = attribute;
        }

        public virtual void PostDraw()
        {
            _scope?.Dispose();
        }
    }
}