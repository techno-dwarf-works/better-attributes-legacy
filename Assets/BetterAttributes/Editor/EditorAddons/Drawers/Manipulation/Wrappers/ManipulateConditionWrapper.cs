using System;
using Better.Attributes.Runtime.Manipulation;
using Better.EditorTools;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Utilities;
using Better.Tools.Runtime;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Manipulation.Wrappers
{
    public class FadeGroup : GUI.Scope
    {
        private readonly Color _color;

        public FadeGroup(bool satisfied)
        {
            _color = GUI.color;
            if (satisfied)
                GUI.color = Color.clear;
        }

        protected override void CloseScope()
        {
            GUI.color = _color;
        }
    }

    public class ManipulateConditionWrapper : UtilityWrapper
    {
        private SerializedProperty _property;
        private ManipulateConditionAttribute _attribute;

        private GUI.Scope _scope;

        public override void Deconstruct()
        {
            _scope?.Dispose();
        }

        private bool IsConditionSatisfied()
        {
            var container = _property.GetPropertyContainer();
            var type = container.GetType();
            var field = type.GetField(_attribute.MemberName, BetterEditorDefines.FieldsFlags);
            var memberValue = _attribute.MemberValue;
            if (field != null)
            {
                return Equals(memberValue, field.GetValue(container));
            }

            var method = type.GetMethod(_attribute.MemberName, BetterEditorDefines.MethodFlags);
            if (method != null)
            {
                return Equals(memberValue, method.Invoke(container, Array.Empty<object>()));
            }

            return false;
        }

        public bool PreDraw()
        {
            var satisfied = IsConditionSatisfied();

            switch (_attribute.ModeType)
            {
                case ManipulationMode.Show:
                    _scope = new FadeGroup(!satisfied);
                    break;
                case ManipulationMode.Hide:
                    _scope = new FadeGroup(satisfied);
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

        public HeightCache GetHeight()
        {
            var satisfied = IsConditionSatisfied();

            switch (_attribute.ModeType)
            {
                case ManipulationMode.Show:
                    var heightCache = new HeightCache(satisfied, 0);
                    return satisfied ? heightCache : heightCache.Force();
                case ManipulationMode.Hide:
                    var height = new HeightCache(!satisfied, 0);
                    return !satisfied ? height.Force() : height;
                case ManipulationMode.Disable:
                case ManipulationMode.Enable:
                    return HeightCache.GetAdditive(0f);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void PostDraw()
        {
            _scope?.Dispose();
        }

        public void SetProperty(SerializedProperty property, ManipulateConditionAttribute attribute)
        {
            _property = property;
            _attribute = attribute;
        }
    }
}