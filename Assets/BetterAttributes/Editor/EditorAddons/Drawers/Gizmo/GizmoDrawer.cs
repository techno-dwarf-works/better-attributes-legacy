using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Gizmo;
using Better.EditorTools;
using Better.EditorTools.Attributes;
using Better.EditorTools.Drawers.Base;
using Better.EditorTools.Helpers;
using Better.EditorTools.Utilities;
using Better.Tools.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

#if UNITY_2022_1_OR_NEWER
using GizmoUtility = Better.Attributes.EditorAddons.Drawers.Utilities.GizmoUtility;
#else
using Better.Attributes.EditorAddons.Drawers.Utilities;
#endif

namespace Better.Attributes.EditorAddons.Drawers.Gizmo
{
    [MultiCustomPropertyDrawer(typeof(GizmoAttribute))]
    [MultiCustomPropertyDrawer(typeof(GizmoLocalAttribute))]
    public class GizmoDrawer : MultiFieldDrawer<GizmoWrapper>
    {
        public GizmoDrawer(FieldInfo fieldInfo, MultiPropertyAttribute attribute) : base(fieldInfo, attribute)
        {
        }

        public override void Initialize(FieldDrawer drawer)
        {
            base.Initialize(drawer);
            EditorApplication.delayCall += DelayCall;
        }

        private void DelayCall()
        {
            EditorApplication.delayCall -= DelayCall;
            SceneView.duringSceneGui += OnSceneGUIDelegate;
            SceneView.RepaintAll();
        }

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
            var attributeType = _attribute.GetType();

            EditorGUI.BeginChangeCheck();
            if (!GizmoUtility.Instance.IsSupported(fieldType))
            {
                var rect = new Rect(position);
                DrawersHelper.NotSupportedAttribute(rect, property, label, fieldType, attributeType);
                return true;
            }

            var cache = ValidateCachedProperties(property, GizmoUtility.Instance);
            if (!cache.IsValid)
            {
                Collection.SetProperty(property, fieldType);
            }

            position = PreparePropertyRect(position);

            return true;
        }

        protected override void DrawField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        protected override void PostDraw(Rect position, SerializedProperty property, GUIContent label)
        {
            if (EditorGUI.EndChangeCheck())
            {
                Collection.SetProperty(property, _fieldInfo.FieldType);
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
            copy.width *= 0.9f;
            return copy;
        }

        private Rect PrepareButtonRect(Rect original)
        {
            var copy = original;
            copy.x += copy.width + DrawersHelper.SpaceHeight;
            copy.width *= 0.1f;
            copy.height = EditorGUIUtility.singleLineHeight;
            return copy;
        }

        protected override HeightCache GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var fieldType = GetFieldOrElementType();
            if (!GizmoUtility.Instance.IsSupported(fieldType))
            {
                var message = DrawersHelper.NotSupportedMessage(property.name, fieldType, _attribute.GetType());
                var additive = DrawersHelper.GetHelpBoxHeight(EditorGUIUtility.currentViewWidth, message, IconType.WarningMessage);
                return HeightCache.GetAdditive(additive + DrawersHelper.SpaceHeight * 2);
            }

            return HeightCache.GetAdditive(0);
        }
    }
}