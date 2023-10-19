﻿using System;
using Better.EditorTools;
using Better.Extensions.Runtime.MathfExtensions;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public class Vector3LocalWrapper : GizmoWrapper
    {
        private Vector3 _vector3;

        public override void Apply(SceneView sceneView)
        {
            if (!ShowInSceneView) return;
            if (_serializedProperty.IsTargetComponent(out var component))
            {
                var transform = component.transform;
                var worldPosition = transform.TransformPoint(_vector3);
                DrawLabel($"Local {GetName()}:\n{_vector3}", worldPosition, _defaultRotation, sceneView);
                var buffer = transform.InverseTransformPoint(Handles.PositionHandle(worldPosition, _defaultRotation));
                
                if (!Vector3Math.Approximately(_vector3, buffer))
                {
                    SetValueAndApply(_vector3);
                }
            }
        }

        public override void SetProperty(SerializedProperty property, Type fieldType)
        {
            _vector3 = property.vector3Value;
            base.SetProperty(property, fieldType);
        }
    }
}