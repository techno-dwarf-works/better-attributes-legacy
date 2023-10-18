using System;
using Better.Extensions.Runtime.MathfExtensions;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class QuaternionWrapper : GizmoWrapper
    {
        private Quaternion _quaternion;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            DrawLabel($"{GetName()}:\n{_quaternion.eulerAngles}", _defaultPosition, _quaternion,
                sceneView);
            _quaternion.Validate();
            _quaternion = Handles.RotationHandle(_quaternion, _defaultPosition);

            Handles.ArrowHandleCap(GUIUtility.GetControlID(FocusType.Passive), _defaultPosition, _quaternion, 1.1f, EventType.Repaint);
            _serializedProperty.quaternionValue = _quaternion;
            SetValueAndApply(_quaternion);
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