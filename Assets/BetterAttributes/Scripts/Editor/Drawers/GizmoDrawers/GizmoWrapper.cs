using System;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers
{
    public abstract class GizmoWrapper
    {
        private protected SerializedProperty _serializedProperty;

        public virtual void SetProperty(SerializedProperty property)
        {
            _serializedProperty = property;
        }

        public abstract void Apply(SceneView sceneView);

        private protected virtual bool ValidateSerializedObject()
        {
            var serializedObject = _serializedProperty.serializedObject;
            return serializedObject == null || serializedObject.targetObject.Equals(null);
        }

        private protected void SetValueAndApply(object value)
        {
            switch (_serializedProperty.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    _serializedProperty.vector2Value = (Vector2)value;
                    break;
                case SerializedPropertyType.Vector3:
                    _serializedProperty.vector3Value = (Vector3)value;
                    break;
                case SerializedPropertyType.Bounds:
                    _serializedProperty.boundsValue = (Bounds)value;
                    break;
                case SerializedPropertyType.Quaternion:
                    _serializedProperty.quaternionValue = (Quaternion)value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private protected virtual void DrawLabel(string value, Vector3 position, SceneView sceneView)
        {
            var style = new GUIStyle
            {
                normal =
                {
                    textColor = Color.green
                }
            };

            
            var vector3 = GetPosition(position, sceneView);

            Handles.Label(vector3, value, style);
        }

        private protected virtual Vector3 GetPosition(Vector3 position, SceneView sceneView)
        {
            return position + Vector3.up * HandleUtility.GetHandleSize(position) +
                   sceneView.camera.transform.right * 0.2f * HandleUtility.GetHandleSize(position);
        }
    }
}