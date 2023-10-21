using System;
using Better.Extensions.Runtime.MathfExtensions;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class Vector3Wrapper : GizmoWrapper
    {
        private Vector3 _vector3;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            DrawLabel($"{GetName()}:\n{_vector3}", _vector3, _defaultRotation, sceneView);
            var buffer = Handles.PositionHandle(_vector3, _defaultRotation);
            
            if (!Vector3Math.Approximately(_vector3, buffer))
            {
                _vector3 = buffer;
                SetValueAndApply(_vector3);
            }
        }

        public override void SetProperty(SerializedProperty property, Type fieldType)
        {
            _vector3 = property.vector3Value;
            base.SetProperty(property, fieldType);
        }
    }
}