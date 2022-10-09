using System;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Gizmo;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.WrapperCollections
{
    public class GizmoWrappers : WrapperCollection<GizmoWrapper>
    {
        public void Apply(SceneView sceneView)
        {
            foreach (var gizmo in Values)
            {
                gizmo.Item1.Apply(sceneView);
            }
        }

        public void SetProperty(SerializedProperty property, Type fieldType)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                gizmoWrapper.Item1.SetProperty(property, fieldType);
            }
        }

        public bool ShowInSceneView(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                return gizmoWrapper.Item1.ShowInSceneView;
            }

            return false;
        }

        public void SwitchShowMode(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                gizmoWrapper.Item1.SwitchShowMode();
            }
        }
    }
}