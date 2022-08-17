using BetterAttributes.EditorAddons.Drawers.GizmoDrawers.BaseWrappers;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers.LocalWrappers
{
    public class Vector2LocalWrapper : GizmoWrapper
    {
        private Vector2 _vector2;
        
        public override void Apply(SceneView sceneView)
        {
            if(!ShowInSceneView) return;
            if (!ValidateSerializedObject()) return;
            if (_serializedProperty.serializedObject.targetObject is MonoBehaviour monoBehaviour)
            {
                var transform = monoBehaviour.transform;
                var worldPosition = transform.TransformPoint(_vector2);
                DrawLabel($"Local {_serializedProperty.name}:\n{_vector2}", _vector2, _defaultRotation, sceneView);
                _vector2 = transform.InverseTransformPoint(Handles.PositionHandle(worldPosition, _defaultRotation));
                SetValueAndApply(_vector2);
            }
        }
        
        public override void SetProperty(SerializedProperty property)
        {
            _vector2 = property.vector2Value;
            base.SetProperty(property);
        }
    }
}