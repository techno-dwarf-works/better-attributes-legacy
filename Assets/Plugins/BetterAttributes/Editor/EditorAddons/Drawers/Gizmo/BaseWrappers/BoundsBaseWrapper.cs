using System;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Gizmo
{
    public abstract class BoundsBaseWrapper : GizmoWrapper
    {
        private protected Bounds _bounds;

        private protected void DrawAndSetSize(Vector3 position)
        {
            _bounds.size = Handles.ScaleHandle(_bounds.size, position, Quaternion.identity,
                HandleUtility.GetHandleSize(position) * 0.7f);
        }

        private protected void ValidateSize()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_bounds.size[i] <= 0)
                {
                    var boundsSize = _bounds.size;
                    boundsSize[i] = 0.01f;
                    _bounds.size = boundsSize;
                }
            }
        }

        public override void SetProperty(SerializedProperty property, Type fieldType)
        {
            _bounds = property.boundsValue;
            base.SetProperty(property, fieldType);
            
            if (_bounds.size == Vector3.zero)
            {
                _bounds.size = Vector3.one / 10f;
                SetValueAndApply(_bounds);
            }
        }
    }
}