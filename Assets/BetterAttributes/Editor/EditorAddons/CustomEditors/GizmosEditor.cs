using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Gizmo;
using Better.Attributes.Runtime.Gizmo;
using Better.EditorTools;
using Better.EditorTools.CustomEditors;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.CustomEditors
{
    [MultiEditor(typeof(Object), true, Order = -999)]
    public class GizmosEditor : EditorExtension
    {
        private HideTransformButtonUtility _hideTransformDrawer;

        public GizmosEditor(Object target, SerializedObject serializedObject) : base(target, serializedObject)
        {
        }

        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {
            CheckAttribute();
        }

        private void CheckAttribute()
        {
            var attributeFound = AttributeFound();

            if (attributeFound && !(_serializedObject.targetObject is ScriptableObject))
            {
                _hideTransformDrawer = new HideTransformButtonUtility();
            }
        }

        private bool AttributeFound()
        {
            var iterator = _serializedObject.GetIterator().Copy();
            var attributeFound = false;
            while (iterator.Next(true))
            {
                var data = iterator.GetFieldInfoAndStaticTypeFromProperty();
                if (data == null)
                {
                    continue;
                }

                if (data.FieldInfo.GetCustomAttribute<GizmoLocalAttribute>() == null && data.FieldInfo.GetCustomAttribute<GizmoAttribute>() == null) continue;
                attributeFound = true;
                break;
            }

            return attributeFound;
        }

        public override void OnInspectorGUI()
        {
            if (_hideTransformDrawer != null)
            {
                _hideTransformDrawer.DrawHideTransformButton();
            }
        }

        public override void OnChanged()
        {
            CheckAttribute();
        }
    }
}