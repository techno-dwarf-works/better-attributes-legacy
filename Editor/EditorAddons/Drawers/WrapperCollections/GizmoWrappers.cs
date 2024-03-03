using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Gizmo;
using Better.EditorTools.EditorAddons.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.WrapperCollections
{
    public class GizmoWrappers : WrapperCollection<GizmoWrapper>
    {
        public void Apply(SceneView sceneView)
        {
            List<SerializedProperty> keysToRemove = null;
            foreach (var gizmo in this)
            {
                var valueWrapper = gizmo.Value.Wrapper;
                if (valueWrapper.Validate())
                {
                    valueWrapper.Apply(sceneView);
                }
                else
                {
                    if (keysToRemove == null)
                    {
                        keysToRemove = new List<SerializedProperty>();
                    }

                    keysToRemove.Add(gizmo.Key);
                }
            }

            if (keysToRemove != null)
            {
                foreach (var property in keysToRemove)
                {
                    Remove(property);
                }
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

        public void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                gizmoWrapper.Wrapper.DrawField(position, label);
            }
        }

        public bool IsValid(SerializedProperty property)
        {
            if (TryGetValue(property, out var gizmoWrapper))
            {
                return gizmoWrapper.Wrapper.Validate();
            }

            return false;
        }

        public HeightCacheValue GetHeight(SerializedProperty property, GUIContent label)
        {
            if (TryGetValue(property, out var wrapper))
            {
                return wrapper.Wrapper.GetHeight(label);
            }

            return HeightCacheValue.GetAdditive(0);
        }
    }
}