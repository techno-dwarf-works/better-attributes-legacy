using System.Collections.Generic;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers.BaseWrappers
{
    public class GizmoWrapperCollection : Dictionary<SerializedProperty, GizmoWrapper>
    {
        private class GizmoComparer : IEqualityComparer<SerializedProperty>
        {
            public bool Equals(SerializedProperty x, SerializedProperty y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.propertyPath == y.propertyPath;
            }

            public int GetHashCode(SerializedProperty obj)
            {
                return (obj.propertyPath != null ? obj.propertyPath.GetHashCode() : 0);
            }
        }

        public GizmoWrapperCollection() : base(new GizmoComparer())
        {
        }

        public void Apply(SceneView sceneView)
        {
            
            foreach (var gizmo in Values)
            {
                gizmo.Apply(sceneView);
            }
        }

        public void Deconstruct()
        {
            foreach (var gizmo in Values)
            {
                gizmo.Deconstruct();
            }
        }

        public void SetProperty(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                gizmoWrapper.SetProperty(property);
            }
        }

        public bool ShowInSceneView(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                return gizmoWrapper.ShowInSceneView;
            }

            return false;
        }

        public void SwitchShowMode(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                gizmoWrapper.SwitchShowMode();
            }
        }
    }
}