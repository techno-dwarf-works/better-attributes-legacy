using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers.LocalWrappers
{
    public class QuaternionLocalWrapper : GizmoWrapper
    {
        private Quaternion _quaternion;

        public override void Apply(SceneView sceneView)
        {
            if (ValidateSerializedObject()) return;
            if (_serializedProperty.serializedObject.targetObject is MonoBehaviour monoBehaviour)
            {
                var transform = monoBehaviour.transform;
                var rotation = transform.rotation;
                var worldRotation = rotation * _quaternion;
                DrawLabel($"Local {_serializedProperty.name}:\n{_quaternion.eulerAngles}", Vector3.zero, sceneView);
                _quaternion = Quaternion.Inverse(rotation) * Handles.RotationHandle(worldRotation, Vector3.zero);
                SetValueAndApply(_quaternion);
            }
        }

        public override void SetProperty(SerializedProperty property)
        {
            _quaternion = property.quaternionValue;
            base.SetProperty(property);
        }

        private protected override Vector3 GetPosition(Vector3 position, SceneView sceneView)
        {
            return _quaternion * (position + Vector3.up * HandleUtility.GetHandleSize(position) +
                                  sceneView.camera.transform.right * 0.2f * HandleUtility.GetHandleSize(position));
        }
    }
}