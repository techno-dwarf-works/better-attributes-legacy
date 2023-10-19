using System;
using Better.EditorTools;
using Better.Extensions.Runtime.MathfExtensions;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class QuaternionLocalWrapper : GizmoWrapper
    {
        private const float Size = 1.1f;
        private Quaternion _quaternion;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            if (_serializedProperty.IsTargetComponent(out var component))
            {
                var transform = component.transform;
                var rotation = transform.rotation;
                var position = transform.position;
                var worldRotation = Vector3Math.Validate(rotation * _quaternion);
                DrawLabel($"Local {GetName()}:\n{_quaternion.eulerAngles}", position, worldRotation, sceneView);
                var buffer = Quaternion.Inverse(rotation) * Handles.RotationHandle(worldRotation, position);

                Handles.ArrowHandleCap(GUIUtility.GetControlID(FocusType.Passive), position, worldRotation, Size * HandleUtility.GetHandleSize(position),
                    EventType.Repaint);

                if (!Vector3Math.Approximately(_quaternion, buffer))
                {
                    _quaternion = buffer;
                    SetValueAndApply(_quaternion);
                }
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
            base.SetProperty(property, fieldType);
        }

        private protected override Vector3 GetPosition(Vector3 position, Quaternion rotation, SceneView sceneView)
        {
            return (position + (rotation * Vector3.up) * HandleUtility.GetHandleSize(position) +
                    sceneView.camera.transform.right * 0.2f * HandleUtility.GetHandleSize(position));
        }
    }
}