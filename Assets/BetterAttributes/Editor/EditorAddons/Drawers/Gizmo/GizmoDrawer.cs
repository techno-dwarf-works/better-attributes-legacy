using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Gizmo;
using Better.EditorTools;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using Better.EditorTools.Utilities;
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
                if (_wrappers == null)
                {
                    _wrappers = GenerateCollection();
                }

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
                if (_wrappers == null)
                {
                    _wrappers = GenerateCollection();
                }
                GizmoUtility.Instance.ValidateCachedProperties(_wrappers);
                Collection?.Apply(sceneView);
            }
        }

        protected override void Deconstruct()
        {
            SceneView.duringSceneGui -= OnSceneGUIDelegate;
            _wrappers?.Deconstruct();
        }

        protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = GetFieldOrElementType();
            var attributeType = attribute.GetType();

            if (!GizmoUtility.Instance.IsSupported(fieldType))
            {
                EditorGUI.BeginChangeCheck();
                DrawField(position, property, label);
                DrawersHelper.NotSupportedAttribute(label.text, fieldInfo.FieldType, attributeType, false);
                return false;
            }

            var cache = ValidateCachedProperties(property, GizmoUtility.Instance);
            if (!cache.IsValid)
            {
                Collection.SetProperty(property, fieldType);
            }

            EditorGUI.BeginChangeCheck();

            return true;
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
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

        protected override WrapperCollection<GizmoWrapper> GenerateCollection()
        {
            return new GizmoWrappers();
        }

        protected override Rect PreparePropertyRect(Rect original)
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