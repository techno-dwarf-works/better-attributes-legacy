using System;
using Better.Extensions.Runtime.MathfExtensions;
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
            _quaternion = Vector3Math.Validate(_quaternion);
            var buffer = Handles.RotationHandle(_quaternion, _defaultPosition);

            Handles.ArrowHandleCap(GUIUtility.GetControlID(FocusType.Passive), _defaultPosition, buffer, Size * HandleUtility.GetHandleSize(_defaultPosition),
                EventType.Repaint);

            if (!Vector3Math.Approximately(_quaternion, buffer))
            {
                SetValueAndApply(_quaternion);
            }
        }

        public override void DrawField(Rect position, GUIContent label)
        {
            using (var change = new EditorGUI.ChangeCheckScope())
            {
                var eulerRotation = EditorGUI.Vector3Field(position, label, _quaternion.eulerAngles);
                _quaternion = Quaternion.Euler(eulerRotation);
                if (change.changed)
                {
                    SetValueAndApply(_quaternion);
                }
            }
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