using System;
using BetterAttributes.EditorAddons.Drawers.Base;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers
{
    public abstract class GizmoWrapper : UtilityWrapper
    {
        private protected SerializedProperty _serializedProperty;
        private protected Type _objectType;

        private protected readonly Quaternion _defaultRotation = Quaternion.identity;
        private protected readonly Vector3 _defaultPosition = Vector3.zero;
        private bool _showInSceneView = true;
        private Type _fieldType;

        public bool ShowInSceneView => _showInSceneView;

        public virtual void SetProperty(SerializedProperty property, Type fieldType)
        {
            _fieldType = fieldType;
            _serializedProperty = property;
            _objectType = _serializedProperty.serializedObject.targetObject.GetType();
        }

        public override void Deconstruct()
        {
            GizmoDrawerUtility.Instance.RemoveButtonDrawn(_objectType);
        }

        public void SwitchShowMode()
        {
            _showInSceneView = !ShowInSceneView;
        }

        public abstract void Apply(SceneView sceneView);

        private protected virtual bool ValidateSerializedObject()
        {
            var serializedObject = _serializedProperty?.serializedObject;

            if (serializedObject == null) return false;
            try
            {
                if (!GizmoDrawerUtility.Instance.IsSupported(_fieldType)) return false;
                return serializedObject.targetObject != null;
            }
            catch
            {
                return false;
            }
        }

        private protected void SetValueAndApply(object value)
        {
            if (_fieldType.IsEquivalentTo(typeof(Vector2)))
                _serializedProperty.vector2Value = (Vector2)value;
            else if (_fieldType.IsEquivalentTo(typeof(Vector3)))
                _serializedProperty.vector3Value = (Vector3)value;
            else if (_fieldType.IsEquivalentTo(typeof(Bounds)))
                _serializedProperty.boundsValue = (Bounds)value;
            else if (_fieldType.IsEquivalentTo(typeof(Quaternion)))
                _serializedProperty.quaternionValue = (Quaternion)value;
            else
                throw new ArgumentOutOfRangeException();

            _serializedProperty.serializedObject.ApplyModifiedProperties();
        }

        private protected virtual void DrawLabel(string value, Vector3 position, Quaternion rotation,
            SceneView sceneView)
        {
            var style = new GUIStyle
            {
                normal =
                {
                    textColor = Color.green
                }
            };


            var vector3 = GetPosition(position, rotation, sceneView);

            Handles.Label(vector3, value, style);
        }

        private protected virtual Vector3 GetPosition(Vector3 position, Quaternion rotation, SceneView sceneView)
        {
            return rotation * (position + Vector3.up * HandleUtility.GetHandleSize(position) +
                               sceneView.camera.transform.right * 0.2f * HandleUtility.GetHandleSize(position));
        }
    }
}