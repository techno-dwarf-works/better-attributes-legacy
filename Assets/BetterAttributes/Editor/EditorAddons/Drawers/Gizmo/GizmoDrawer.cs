using Better.Attributes.EditorAddons.Drawers.Base;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Gizmo;
using Better.EditorTools;
using Better.EditorTools.Helpers;
using UnityEditor;
using UnityEngine;
#if UNITY_2022_1_OR_NEWER
using GizmoUtility = Better.Attributes.EditorAddons.Drawers.Utilities.GizmoUtility;
#else
using Better.Attributes.EditorAddons.Drawers.Utilities;
#endif

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    [CustomPropertyDrawer(typeof(GizmoAttribute))]
    [CustomPropertyDrawer(typeof(GizmoLocalAttribute))]
    public class GizmoDrawer : MultiFieldDrawer<GizmoWrapper>
    {
        private GizmoWrappers Collection
        {
            get
            {
                _wrappers ??= GenerateCollection();
                return _wrappers as GizmoWrappers;
            }
        }

        private HideTransformButtonUtility _hideTransformDrawer;

        public GizmoDrawer() : base()
        {
            SceneView.duringSceneGui += OnSceneGUIDelegate;
        }

        private void OnSceneGUIDelegate(SceneView sceneView)
        {
            if (sceneView.drawGizmos)
            {
                GizmoUtility.Instance.ValidateCachedProperties(_wrappers);
                Collection?.Apply(sceneView);
            }
        }

        private protected override void Deconstruct()
        {
            SceneView.duringSceneGui -= OnSceneGUIDelegate;
            _wrappers?.Deconstruct();
        }

        private protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = fieldInfo.FieldType;
            var attributeType = attribute.GetType();

            if (_hideTransformDrawer == null && property.IsTargetComponent(out _))
            {
                _hideTransformDrawer = new HideTransformButtonUtility(property, GizmoUtility.Instance);
            }

            if (!GizmoUtility.Instance.IsSupported(fieldType))
            {
                EditorGUI.BeginChangeCheck();
                DrawField(position, property, label);
                DrawersHelper.NotSupportedAttribute(label.text, fieldInfo.FieldType, attributeType, false);
                return false;
            }

            if (!ValidateCachedProperties(property, GizmoUtility.Instance))
            {
                Collection.SetProperty(property, fieldType);
            }

            EditorGUI.BeginChangeCheck();

            if (_hideTransformDrawer != null)
            {
                _hideTransformDrawer.DrawHideTransformButton();
            }

            return true;
        }

        private protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            if (EditorGUI.EndChangeCheck())
            {
                Collection.SetProperty(property, fieldInfo.FieldType);
            }

            if (GUI.Button(PrepareButtonRect(position), Collection.ShowInSceneView(property) ? "Hide" : "Show"))
            {
                Collection.SwitchShowMode(property);
                SceneView.RepaintAll();
            }
        }

        private protected override WrapperCollection<GizmoWrapper> GenerateCollection()
        {
            return new GizmoWrappers();
        }

        private protected override Rect PreparePropertyRect(Rect original)
        {
            var copy = original;
            copy.width *= 0.89f;
            return copy;
        }

        private Rect PrepareButtonRect(Rect original)
        {
            var copy = original;
            copy.x += copy.width * 0.9f;
            copy.width *= 0.1f;
            copy.height = EditorGUIUtility.singleLineHeight;
            return copy;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}