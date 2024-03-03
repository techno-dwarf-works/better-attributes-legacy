using System;
using Better.EditorTools.EditorAddons.Drawers.Base;
using Better.Extensions.Runtime;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class QuaternionWrapper : GizmoWrapper
    {
        private const float Size = 1.1f;
        private Quaternion _quaternion;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            DrawLabel($"{GetName()}:\n{_quaternion.eulerAngles}", _defaultPosition, _quaternion, sceneView);
            if (!_quaternion.IsNormalized())
            {
                _quaternion = Quaternion.identity;
            }

            var buffer = Handles.RotationHandle(_quaternion, _defaultPosition);

            Handles.ArrowHandleCap(GUIUtility.GetControlID(FocusType.Passive), _defaultPosition, buffer, Size * HandleUtility.GetHandleSize(_defaultPosition),
                EventType.Repaint);

            if (!_quaternion.Approximately(buffer))
            {
                _quaternion = buffer;
                SetValueAndApply(_quaternion);
            }
        }

        public override void DrawField(Rect position, GUIContent label)
        {
            using (var change = new EditorGUI.ChangeCheckScope())
            {
                var eulerRotation = EditorGUI.Vector3Field(position, label, _quaternion.eulerAngles);
                if (change.changed)
                {
                    _quaternion = Quaternion.Euler(eulerRotation);
                    SetValueAndApply(_quaternion);
                }
            }
        }

        public override HeightCacheValue GetHeight(GUIContent label)
        {
            return HeightCacheValue.GetFull(EditorGUIUtility.singleLineHeight);
        }

        public override void SetProperty(SerializedProperty property, Type fieldType)
        {
            _quaternion = property.quaternionValue;

            var num = Mathf.Sqrt(Quaternion.Dot(_quaternion, _quaternion));
            if (num < (double)Mathf.Epsilon)
            {
                _quaternion = _defaultRotation;
            }

            base.SetProperty(property, fieldType);
        }
    }
}