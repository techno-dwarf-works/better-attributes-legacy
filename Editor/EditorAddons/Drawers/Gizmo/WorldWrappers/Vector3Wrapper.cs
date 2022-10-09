using System;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Gizmo
{
    public class Vector3Wrapper : GizmoWrapper
    {
        private Vector3 _vector3;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            if (!ValidateSerializedObject()) return;
            DrawLabel($"{_serializedProperty.name}:\n{_vector3}", _vector3, _defaultRotation, sceneView);
            _vector3 = Handles.PositionHandle(_vector3, _defaultRotation);
            _serializedProperty.vector3Value = _vector3;
            SetValueAndApply(_vector3);
        }

        public override void SetProperty(SerializedProperty property, Type fieldType)
        {
            _vector3 = property.vector3Value;
            base.SetProperty(property, fieldType);
        }
    }
}