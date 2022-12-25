using System;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Better.Attributes.EditorAddons.Drawers.DrawInspector
{
    [Serializable]
    public class DrawInspectorWrapper : UtilityWrapper
    {
        private Editor _cachedEditor;
        private bool _isOpen = false;

        public void OnGUI()
        {
            if (!_isOpen) return;
            if (_cachedEditor == null) return;
            EditorGUI.indentLevel++;
            EditorGUI.BeginChangeCheck();

            _cachedEditor.OnInspectorGUI();

            if (EditorGUI.EndChangeCheck())
            {
                _cachedEditor.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.indentLevel--;
        }

        public void SetOpen(bool value)
        {
            _isOpen = value;
        }
        
        public bool IsOpen()
        {
            return _isOpen;
        }

        public void CreateEditor(Object targetObject)
        {
            _cachedEditor = Editor.CreateEditor(targetObject);
        }

        public override void Deconstruct()
        {
            if (_cachedEditor == null) return;
            Object.DestroyImmediate(_cachedEditor);
            _cachedEditor = null;
        }
    }
}