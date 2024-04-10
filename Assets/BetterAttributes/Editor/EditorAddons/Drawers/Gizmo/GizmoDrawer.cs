using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.WrapperCollections;
using Better.Attributes.Runtime.Gizmo;
using Better.Commons.EditorAddons.Drawers.Attributes;
using Better.Commons.EditorAddons.Drawers.Base;
using Better.Commons.EditorAddons.Drawers.Caching;
using Better.Commons.EditorAddons.Enums;
using Better.Commons.EditorAddons.Utility;
using Better.Commons.Runtime.Drawers.Attributes;
using UnityEditor;
using UnityEngine;

#if UNITY_2022_1_OR_NEWER
using GizmoUtility = Better.Attributes.EditorAddons.Drawers.Utility.GizmoUtility;
#else
using Better.Attributes.EditorAddons.Drawers.Utility;
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
                ExtendedGUIUtility.NotSupportedAttribute(rect, property, label, fieldType, attributeType);
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
            if (!Collection.IsValid(property))
            {
                var cache = ValidateCachedProperties(property, GizmoUtility.Instance);

                if (cache.Value != null)
                {
                    var fieldType = GetFieldOrElementType();
                    cache.Value.Wrapper.SetProperty(property, fieldType);
                }
            }

            Collection.DrawField(position, property, label);
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
            copy.x += copy.width + ExtendedGUIUtility.SpaceHeight;
            copy.width *= 0.1f;
            copy.height = EditorGUIUtility.singleLineHeight;
            return copy;
        }

        protected override HeightCacheValue GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var fieldType = GetFieldOrElementType();
            if (!GizmoUtility.Instance.IsSupported(fieldType))
            {
                var message = ExtendedGUIUtility.NotSupportedMessage(property.name, fieldType, _attribute.GetType());
                var additive = ExtendedGUIUtility.GetHelpBoxHeight(EditorGUIUtility.currentViewWidth, message, IconType.WarningMessage);
                return HeightCacheValue.GetAdditive(additive + ExtendedGUIUtility.SpaceHeight * 2);
            }

            return Collection.GetHeight(property, label);
        }
    }
}