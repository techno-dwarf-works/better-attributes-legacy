using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Plugin.Editor.Drawers.GizmoDrawers.Wrappers
{
    public class Vector2Wrapper : GizmoWrapper
    {
        private Vector2 _vector2;

        public override void Apply(SceneView sceneView)
        {
            if(!ShowInSceneView) return;
            if (!ValidateSerializedObject()) return;
            DrawLabel($"{_serializedProperty.name}:\n{_vector2}", _vector2, _defaultRotation, sceneView);
            _vector2 = Handles.PositionHandle(_vector2, _defaultRotation);

            SetValueAndApply(_vector2);
        }

        public override void SetProperty(SerializedProperty property)
        {
            _vector2 = property.vector2Value;
            base.SetProperty(property);
        }
    }
}