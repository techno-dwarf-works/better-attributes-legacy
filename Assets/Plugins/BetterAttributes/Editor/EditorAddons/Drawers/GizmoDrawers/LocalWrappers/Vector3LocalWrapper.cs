using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers.LocalWrappers
{
    public class Vector3LocalWrapper : GizmoWrapper
    {
        private Vector3 _vector3;

        public override void Apply(SceneView sceneView)
        {
            if(!ShowInSceneView) return;
            if (!ValidateSerializedObject()) return;
            if (_serializedProperty.serializedObject.targetObject is MonoBehaviour monoBehaviour)
            {
                var transform = monoBehaviour.transform;
                var worldPosition = transform.TransformPoint(_vector3);
                DrawLabel($"Local {_serializedProperty.name}:\n{_vector3}", worldPosition,_defaultRotation, sceneView);
                _vector3 = transform.InverseTransformPoint(Handles.PositionHandle(worldPosition, _defaultRotation));
                SetValueAndApply(_vector3);
            }
        }
        
        public override void SetProperty(SerializedProperty property)
        {
            _vector3 = property.vector3Value;
            base.SetProperty(property);
        }
    }
}