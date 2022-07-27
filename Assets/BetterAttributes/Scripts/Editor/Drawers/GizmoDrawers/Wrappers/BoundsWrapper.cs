using BetterAttributes.Drawers.GizmoDrawers.BaseWrappers;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers.Wrappers
{
    public class BoundsWrapper : BoundsBaseWrapper
    {
        public override void Apply(SceneView sceneView)
        {
            if (ValidateSerializedObject()) return;
            DrawLabel($"{_serializedProperty.name}:\nCenter: {_bounds.center}\nSize: {_bounds.size}", _bounds.center, sceneView);
            _bounds.center = Handles.PositionHandle(_bounds.center, Quaternion.identity);
            DrawAndSetSize(_bounds.center);
            ValidateSize();
            Handles.DrawWireCube(_bounds.center, _bounds.size);
            
            SetValueAndApply(_bounds);
        }
    }
}