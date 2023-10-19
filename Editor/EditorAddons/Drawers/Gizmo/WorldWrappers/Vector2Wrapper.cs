using System;
using Better.Extensions.Runtime.MathfExtensions;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class Vector2Wrapper : GizmoWrapper
    {
        private Vector2 _vector2;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            DrawLabel($"{GetName()}:\n{_vector2}", _vector2, _defaultRotation, sceneView);
            var buffer = Handles.PositionHandle(_vector2, _defaultRotation);
            
            if (!Vector3Math.Approximately(_vector2, (Vector2)buffer))
            {
                SetValueAndApply(_vector2);
            }
        }

        public override void SetProperty(SerializedProperty property, Type fieldType)
        {
            _vector2 = property.vector2Value;
            base.SetProperty(property, fieldType);
        }
    }
}