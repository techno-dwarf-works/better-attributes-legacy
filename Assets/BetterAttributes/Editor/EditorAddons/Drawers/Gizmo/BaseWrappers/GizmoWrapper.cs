using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.EditorTools;
using Better.EditorTools.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_2022_1_OR_NEWER
using GizmoUtility = Better.Attributes.EditorAddons.Drawers.Utilities.GizmoUtility;
#endif

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    public abstract class GizmoWrapper : UtilityWrapper
    {
        private protected SerializedProperty _serializedProperty;

        private protected readonly Quaternion _defaultRotation = Quaternion.identity;
        private protected readonly Vector3 _defaultPosition = Vector3.zero;
        private bool _showInSceneView = true;
        private Type _fieldType;
        
        private string _compiledName;

        public bool ShowInSceneView => _showInSceneView;

        public virtual void SetProperty(SerializedProperty property, Type fieldType)
        {
            _serializedProperty = property;
            _fieldType = fieldType;
            _compiledName = GetCompiledName();
        }

        private string GetCompiledName()
        {
            if (Validate())
            {
                if (_serializedProperty.IsArrayElement())
                {
                    return $"{ObjectNames.NicifyVariableName(_serializedProperty.GetArrayPath())}";
                }

                return _serializedProperty.displayName;
            }

            return string.Empty;
        }

        public void SwitchShowMode()
        {
            _showInSceneView = !ShowInSceneView;
        }

        public abstract void Apply(SceneView sceneView);

        private protected virtual string GetName()
        {
            return _compiledName;
        }

        public override void Deconstruct()
        {
            _serializedProperty = null;
        }

        public virtual bool Validate()
        {
            try
            {
                if (_serializedProperty.IsDisposed())
                {
                    return false;
                }

                if (!GizmoUtility.Instance.IsSupported(_fieldType)) return false;
                return _serializedProperty.serializedObject.targetObject != null;
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

        private protected virtual void DrawLabel(string value, Vector3 position, Quaternion rotation, SceneView sceneView)
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