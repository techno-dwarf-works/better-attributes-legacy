using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers.Wrappers
{
    public class QuaternionWrapper : GizmoWrapper
    {
        private Quaternion _quaternion;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            if (!ValidateSerializedObject()) return;
            DrawLabel($"{_serializedProperty.name}:\n{_quaternion.eulerAngles}", _defaultPosition, _quaternion,
                sceneView);
            _quaternion = Handles.RotationHandle(_quaternion, _defaultPosition);
            _serializedProperty.quaternionValue = _quaternion;
            SetValueAndApply(_quaternion);
        }

        public override void SetProperty(SerializedProperty property)
        {
            _quaternion = property.quaternionValue;
            
            var num = Mathf.Sqrt(Quaternion.Dot(_quaternion, _quaternion));
            if (num < (double)Mathf.Epsilon)
            {
                _quaternion = _defaultRotation;
            }
            
            base.SetProperty(property);
        }
    }
}