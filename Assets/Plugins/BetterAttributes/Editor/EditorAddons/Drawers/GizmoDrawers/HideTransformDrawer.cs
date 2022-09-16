using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers
{
    public class HideTransformButtonUtility
    {
        private bool _isChecked = false;
        private bool _isButtonDrawn;
        private readonly SerializedProperty _serializedProperty;
        private readonly GizmoDrawerUtility _gizmoDrawerUtility;

        public HideTransformButtonUtility(SerializedProperty property, GizmoDrawerUtility gizmoDrawerUtility)
        {
            _serializedProperty = property;
            _gizmoDrawerUtility = gizmoDrawerUtility;
        }

        public void DrawHideTransformButton(Rect position)
        {
            if (!(_serializedProperty.serializedObject.targetObject is MonoBehaviour monoBehaviour)) return;
            var type = monoBehaviour.GetType();
            if (!_isChecked)
            {
                _isButtonDrawn = _gizmoDrawerUtility.IsButtonDrawn(type);
                _isChecked = true;
            }

            if (_isButtonDrawn) return;
            var text = Tools.hidden ? "Show" : "Hide";
            if (GUI.Button(PrepareHideTransformRect(position), $"{text} Transform handles"))
            {
                Tools.hidden = !Tools.hidden;
                SceneView.RepaintAll();
            }
        }

        private Rect PrepareHideTransformRect(Rect original)
        {
            var copy = original;
            copy.height = EditorGUIUtility.singleLineHeight;
            return copy;
        }

        public void UpdatePosition(ref Rect position)
        {
            position.y += EditorGUIUtility.singleLineHeight;
        }
    }
}