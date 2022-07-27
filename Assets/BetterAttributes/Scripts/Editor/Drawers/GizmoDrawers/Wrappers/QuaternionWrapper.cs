using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers.Wrappers
{
    public class QuaternionWrapper : GizmoWrapper
    {
        private Quaternion _quaternion;
        
        public override void Apply(SceneView sceneView)
        {
            if (ValidateSerializedObject()) return;
            DrawLabel($"{_serializedProperty.name}:\n{_quaternion.eulerAngles}", Vector3.zero, sceneView);
            _quaternion = Handles.RotationHandle(_quaternion, Vector3.zero);
            _serializedProperty.quaternionValue = _quaternion;
            SetValueAndApply(_quaternion);
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