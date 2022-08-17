using BetterAttributes.EditorAddons.Drawers.GizmoDrawers.BaseWrappers;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers.LocalWrappers
{
    public class QuaternionLocalWrapper : GizmoWrapper
    {
        private Quaternion _quaternion;

        public override void Apply(SceneView sceneView)
        {
            if(!ShowInSceneView) return;
            if (!ValidateSerializedObject()) return;
            if (_serializedProperty.serializedObject.targetObject is MonoBehaviour monoBehaviour)
            {
                var transform = monoBehaviour.transform;
                var rotation = transform.rotation;
                var position = transform.position;
                var worldRotation = rotation * _quaternion;
                DrawLabel($"Local {_serializedProperty.name}:\n{_quaternion.eulerAngles}", position, worldRotation,
                    sceneView);
                _quaternion = Quaternion.Inverse(rotation) * Handles.RotationHandle(worldRotation, position);
                SetValueAndApply(_quaternion);
            }
        }

        public override void SetProperty(SerializedProperty property)
        {
            _quaternion = property.quaternionValue;
            base.SetProperty(property);
        }

        private protected override Vector3 GetPosition(Vector3 position, Quaternion rotation, SceneView sceneView)
        {
            return (position + (rotation * Vector3.up) * HandleUtility.GetHandleSize(position) +
                    sceneView.camera.transform.right * 0.2f * HandleUtility.GetHandleSize(position));
        }
    }
}