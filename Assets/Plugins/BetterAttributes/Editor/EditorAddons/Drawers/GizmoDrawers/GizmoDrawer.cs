using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Helpers;
using BetterAttributes.Runtime.GizmoAttributes;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers
{
    [CustomPropertyDrawer(typeof(GizmoAttribute))]
    [CustomPropertyDrawer(typeof(GizmoLocalAttribute))]
    public class GizmoDrawer : BaseMultiFieldDrawer<GizmoWrapper>
    {
        private GizmoWrapperCollection Collection
        {
            get
            {
                _wrappers ??= GenerateCollection();
                return _wrappers as GizmoWrapperCollection;
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
                GizmoDrawerUtility.Instance.ValidateCachedProperties(_wrappers);
                Collection?.Apply(sceneView);
            }
        }

        private protected override void Deconstruct()
        {
            SceneView.duringSceneGui -= OnSceneGUIDelegate;
            _wrappers?.Deconstruct();
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
            return new GizmoWrapperCollection();
        }

        private protected override bool PreDraw(ref Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = fieldInfo.FieldType;
            var attributeType = attribute.GetType();

            if (_hideTransformDrawer == null && property.serializedObject.targetObject is MonoBehaviour)
            {
                _hideTransformDrawer = new HideTransformButtonUtility(property, GizmoDrawerUtility.Instance);
            }
            
            if (!GizmoDrawerUtility.Instance.IsSupported(fieldType))
            {
                _hideTransformDrawer?.UpdatePosition(ref position);
                EditorGUI.BeginChangeCheck();
                DrawField(position, property, label);
                DrawersHelper.NotSupportedAttribute(label.text, fieldInfo.FieldType, attributeType);
                return false;
            }

            if (!Collection.ContainsKey(property))
            {
                var gizmoWrapper =
                    GizmoDrawerUtility.Instance.GetUtilityWrapper<GizmoWrapper>(fieldType, attributeType);
                Collection.Add(property, (gizmoWrapper, fieldType));

                Collection.SetProperty(property, fieldType);
            }
            else
            {
                GizmoDrawerUtility.Instance.ValidateCachedProperties(Collection);
            }

            EditorGUI.BeginChangeCheck();

            _hideTransformDrawer?.DrawHideTransformButton(position);
            _hideTransformDrawer?.UpdatePosition(ref position);

            return true;
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