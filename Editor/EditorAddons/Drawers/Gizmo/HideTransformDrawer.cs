using BetterAttributes.EditorAddons.Drawers.Utilities;
using BetterExtensions.EditorAddons;
using UnityEditor;
using UnityEngine;
#if UNITY_2022_1_OR_NEWER
using GizmoUtility = BetterAttributes.EditorAddons.Drawers.Utilities.GizmoUtility;
#endif

namespace BetterAttributes.EditorAddons.Drawers.Gizmo
{
    public class HideTransformButtonUtility
    {
        private bool _isChecked = false;
        private bool _isButtonDrawn;
        private readonly SerializedProperty _serializedProperty;
        private readonly GizmoUtility _gizmoDrawerUtility;

        public HideTransformButtonUtility(SerializedProperty property, GizmoUtility gizmoDrawerUtility)
        {
            _serializedProperty = property;
            _gizmoDrawerUtility = gizmoDrawerUtility;
        }

        public void DrawHideTransformButton()
        {
            if (!_serializedProperty.IsTargetComponent(out var component)) return;
            var type = component.GetType();
            if (!_isChecked)
            {
                _isButtonDrawn = _gizmoDrawerUtility.IsButtonDrawn(type);
                _isChecked = true;
            }

            if (_isButtonDrawn) return;
            var text = Tools.hidden ? "Show" : "Hide";
            if (GUILayout.Button($"{text} Transform handles"))
            {
                Tools.hidden = !Tools.hidden;
                SceneView.RepaintAll();
            }
        }
    }
}