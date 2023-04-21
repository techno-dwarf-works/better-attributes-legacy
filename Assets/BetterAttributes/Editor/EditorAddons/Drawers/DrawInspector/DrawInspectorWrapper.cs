using System;
using Better.EditorTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.DrawInspector
{
    [Serializable]
    public class DrawInspectorWrapper : UtilityWrapper
    {
        private SerializedProperty _property;
        private bool _isOpen = false;
        private SerializedObject _serializedObject;

        public void OnGUI(Rect rect)
        {
            if (!_isOpen) return;
            if (_property == null) return;
            if (_serializedObject == null && _property.objectReferenceValue != null)
            {
                _serializedObject = new SerializedObject(_property.objectReferenceValue);
            }

            if (_serializedObject == null)
            {
                return;
            }

            var iterator = _serializedObject.GetIterator();
            var x = rect.position.x + 15f;
            var y = rect.position.y + EditorGUI.GetPropertyHeight(_property);
            rect.position = new Vector2(x, y);
            rect.width -= 15f;

            using (var change = new EditorGUI.ChangeCheckScope())
            {
                for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
                {
                    using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                        EditorGUI.PropertyField(rect, iterator, true);
                    rect.position = new Vector2(rect.position.x, rect.position.y + EditorGUI.GetPropertyHeight(iterator, true));
                }

                if (change.changed)
                {
                    _serializedObject.ApplyModifiedProperties();
                }
            }
        }

        public void SetOpen(bool value)
        {
            _isOpen = value;
            ReorderableListHelpers.RepaintAllInspectors(_property);
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public void SetProperty(SerializedProperty property)
        {
            if (property.objectReferenceValue != null)
            {
                _serializedObject = new SerializedObject(property.objectReferenceValue);
            }

            _property = property;
        }

        public override void Deconstruct()
        {
            _property = null;
            _serializedObject = null;
        }

        private float CalculateHeight(SerializedObject serializedObject)
        {
            var height = 0f;
            var iterator = serializedObject.GetIterator();
            for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                height += EditorGUI.GetPropertyHeight(iterator, true);
            }

            return height;
        }

        public float GetHeight()
        {
            return _serializedObject != null ? CalculateHeight(_serializedObject) : 0f;
        }
    }
}