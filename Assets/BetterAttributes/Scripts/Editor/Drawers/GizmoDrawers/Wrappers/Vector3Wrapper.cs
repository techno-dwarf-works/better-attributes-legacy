using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers.Wrappers
{
    public class Vector3Wrapper : GizmoWrapper
    {
        private Vector3 _vector3;
        
        public override void Apply(SceneView sceneView)
        {
            if (ValidateSerializedObject()) return;
            DrawLabel($"{_serializedProperty.name}:\n{_vector3}", _vector3, sceneView);
            _vector3 = Handles.PositionHandle(_vector3, Quaternion.identity);
            _serializedProperty.vector3Value = _vector3;
            SetValueAndApply(_vector3);
        }
        
        public override void SetProperty(SerializedProperty property)
        {
            _vector3 = property.vector3Value;
            base.SetProperty(property);
        }
    }
}