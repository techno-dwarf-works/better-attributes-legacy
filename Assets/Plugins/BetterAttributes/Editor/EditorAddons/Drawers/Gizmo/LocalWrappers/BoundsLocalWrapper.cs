using BetterAttributes.EditorAddons.Helpers;
using BetterExtensions.EditorAddons;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Gizmo
{
    public class BoundsLocalWrapper : BoundsBaseWrapper
    {
        public override void Apply(SceneView sceneView)
        {
            if(!ShowInSceneView) return;
            if (!ValidateSerializedObject()) return;
            if (_serializedProperty.IsTargetComponent(out var component))
            {
                var transform = component.transform;
                var worldPosition = transform.TransformPoint(_bounds.center);
                DrawLabel($"{_serializedProperty.name}:\nLocal Center: {_bounds.center}\nSize: {_bounds.size}",
                    worldPosition, _defaultRotation, sceneView);
                _bounds.center =
                    transform.InverseTransformPoint(Handles.PositionHandle(worldPosition, Quaternion.identity));
                DrawAndSetSize(worldPosition);
                ValidateSize();
                Handles.DrawWireCube(worldPosition, _bounds.size);

                SetValueAndApply(_bounds);
            }
        }
    }
}