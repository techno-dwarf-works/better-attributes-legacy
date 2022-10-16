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
                gizmo.Wrapper.Apply(sceneView);
            }
        }

        public void SetProperty(SerializedProperty property, Type fieldType)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                gizmoWrapper.Wrapper.SetProperty(property, fieldType);
            }
        }

        public bool ShowInSceneView(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                return gizmoWrapper.Wrapper.ShowInSceneView;
            }

            return false;
        }

        public void SwitchShowMode(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                gizmoWrapper.Wrapper.SwitchShowMode();
            }
        }
    }
}